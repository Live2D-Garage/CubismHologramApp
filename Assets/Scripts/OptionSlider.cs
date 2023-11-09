using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    public GameObject Model;
    public Slider ScaleSlider;
    public Slider OffsetXSlider;
    public Slider OffsetYSlider;
    public Toggle FlipToggle;
    private float FlipValue = 1;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScaleChanged()
    {

        UI.transform.localScale = new Vector3(FlipValue * ScaleSlider.value, FlipValue * ScaleSlider.value, 1);
    }

    public void OffsetXChanged()
    {
        UI.GetComponent<RectTransform>().localPosition = new Vector3(OffsetXSlider.value, OffsetYSlider.value, 0);

    }

    public void OffsetYChanged()
    {
        UI.GetComponent<RectTransform>().localPosition = new Vector3(OffsetXSlider.value, OffsetYSlider.value, 0);
    }

    public void ToggleChanged()
    {
        if (FlipToggle.isOn)
        {
            FlipValue = -1;
            UI.transform.localScale = new Vector3(FlipValue * ScaleSlider.value, FlipValue * ScaleSlider.value, 1);
        }
        else
        {
            FlipValue = 1;
            UI.transform.localScale = new Vector3(FlipValue * ScaleSlider.value, FlipValue * ScaleSlider.value, 1);
        }
    }
}
