using System;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_IOS
using System.Runtime.InteropServices;
#endif

public static class SetBrightness 
{
    #if !UNITY_EDITOR && UNITY_IOS
    [DllImport("__Internal")]
    private static extern void setBrightness(float brightness);
    #endif

    public static void DoAction(float maxbright) {
        #if UNITY_EDITOR
        #elif UNITY_IOS
        setBrightness(maxbright);
        #elif UNITY_ANDROID
        var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            var window = activity.Call<AndroidJavaObject>("getWindow");
            var lp = window.Call<AndroidJavaObject>("getAttributes");
            lp.Set("screenBrightness", maxbright);
            window.Call("setAttributes", lp);
        }));
        #endif
    }
}
