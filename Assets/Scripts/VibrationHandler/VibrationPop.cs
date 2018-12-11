using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class VibrationPop
{
#if UNITY_IPHONE
    const string dll = "__Internal";
#elif UNITY_ANDROID
    const string dll = "VibrationDLL";
#elif UNITY_EDITOR
    const string dll = " ";
#endif

#if UNITY_IPHONE

    [DllImport(dll)]
    private static extern void Vibrate(int x);

    public static void vibrateforDuration(int x)
    {
        Vibrate(x);
    }


#elif UNITY_ANDROID
    public static void vibrateforDuration(int x)
    {
        Debug.Log ("Vibrated");
        if (x == 1)
        {
            Vibration.Vibrate (40);
            //vibrator.Call("vibrate", 300);
        }
        else if(x == 2)
        {
            Vibration.Vibrate (90);
            //vibrator.Call("vibrate", 600);
        }
    }
#elif UNITY_EDITOR
    public static void vibrateforDuration(int x)
    {
        //Debug.Log("Vibrated");
        /* if (x == 1)
         {
             Vibration.Vibrate(300);
             //vibrator.Call("vibrate", 300);
         }
         else if (x == 2)
         {
             Vibration.Vibrate(600);
             //vibrator.Call("vibrate", 600);
         }*/
    }
#endif

}

