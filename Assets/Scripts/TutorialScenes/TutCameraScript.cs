using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCameraScript : MonoBehaviour
{

    //public GameObject player;
    private Vector3 offset;
    //private float positionZ;
    // background color
    //private Color32 lightColor;
    //private Color32 darkColor;

    public Camera mainCamera;

    //public ParticleSystem warpMode;

    public Material normalModeBackground;

    private Color32 fogColorNormal;

    // private float restrictedAreaTimer;


    void Start()
    {
        offset = transform.position - TutPlayer.instance.transform.position;
        //lightColor = new Color32(70, 255, 255, 255);
        //darkColor = new Color32(50, 50, 50, 0);

        //mainCamera.backgroundColor = lightColor;
        mainCamera.fieldOfView = 60;
        mainCamera.farClipPlane = 450f;
        RenderSettings.skybox = normalModeBackground;

        fogColorNormal = new Color32(180, 205, 200, 255);
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 100;
        RenderSettings.fogEndDistance = 250;
        RenderSettings.fogColor = fogColorNormal;
        // restrictedAreaTimer = .2f;
    }

    /* void Update()
    {
        //exitRestrictedAreaTimer -= Time.deltaTime / 20;
        restrictedAreaTimer -= Time.deltaTime / 50;
    }*/

    void LateUpdate()
    {
        transform.position = TutPlayer.instance.transform.position + offset;
    }
}

