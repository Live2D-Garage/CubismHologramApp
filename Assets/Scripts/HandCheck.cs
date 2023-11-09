#if !DO_NOT_USE_HAND_TRACK
using OpenCvSharp;
#endif
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandCheck : MonoBehaviour
{
#if !DO_NOT_USE_HAND_TRACK
    public RawImage _renderer;
    public Scalar SKIN_LOWER = new Scalar(0, 60, 80);
    public Scalar SKIN_UPPER = new Scalar(10, 160, 240);

    private int _width = 1920;
    private int _height = 1080;
    private int _fps = 30;
    private WebCamTexture _webcamTexture;

    public int DeviceID = 0;
    public int ScaleThreshold = 30000;

    public Animator Animator;
    private bool Interact = false;
    public bool Debug = false;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        _webcamTexture = new WebCamTexture(devices[DeviceID].name, this._width, this._height, this._fps);
        _webcamTexture.Play();
    }

    void OnDestroy()
    {
        if (_webcamTexture != null)
        {
            if (_webcamTexture.isPlaying) _webcamTexture.Stop();
            _webcamTexture = null;
        }
    }

    private void Update()
        => ExtractSkinColor(_webcamTexture);

    private void ExtractSkinColor(WebCamTexture tex)
    {
        int cnt = Time.frameCount;
        if (cnt % 10 == 0)
        {
            Mat mat = OpenCvSharp.Unity.TextureToMat(tex);
            Mat hsvMat = new Mat();
            Cv2.CvtColor(mat, hsvMat, ColorConversionCodes.BGR2HSV);
            Mat binary = hsvMat.InRange(SKIN_LOWER, SKIN_UPPER);

            var label = new MatOfInt();
            var stats = new MatOfInt();
            var centroids = new MatOfDouble();
            var nLabels = Cv2.ConnectedComponentsWithStats(binary, label, stats, centroids, PixelConnectivity.Connectivity8, MatType.CV_32SC1);
            var statsIndexer = stats.GetGenericIndexer<int>();
            var maxArea = 0;
            var maxIndex = 0;
            for (int i = 1; i < nLabels; i++)
            {
                var area = statsIndexer[i, 4];
                if (maxArea < area)
                {
                    maxArea = area;
                    maxIndex = i;
                }
            }

            if (maxArea >= ScaleThreshold)
            {
                if (Interact == false)
                {
                    Animator.SetTrigger("Play");
                    Interact = true;
                }
            }
            else
            {
                Interact = false;
            }


            if (Debug)
            {
                var handMat = label.InRange(maxIndex, maxIndex + 1);
                Cv2.CvtColor(handMat, handMat, ColorConversionCodes.GRAY2BGR);
                _renderer.texture = OpenCvSharp.Unity.MatToTexture(handMat);
            }
        }
    }
#endif
}