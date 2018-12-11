using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    //public GameObject player;
    private Vector3 offset;
    //private float positionZ;
    // background color
    //private Color32 lightColor;
    //private Color32 darkColor;

    public Camera mainCamera;

    private float initialZAngleofCamera;
    //public ParticleSystem warpMode;
    private float t;
    private bool isChanged;

    public Material normalModeBackground;
    public Material bulletProofModeBackground;

    private Color32 fogColorNormal;
    private Color32 fogColorBulletProofMode;

    // private float restrictedAreaTimer;


    void Start()
    {
        offset = transform.position - Player.instance.transform.position;
        initialZAngleofCamera = transform.eulerAngles.z;
        //lightColor = new Color32(70, 255, 255, 255);
        //darkColor = new Color32(50, 50, 50, 0);

        //mainCamera.backgroundColor = lightColor;
        mainCamera.fieldOfView = 60;
        mainCamera.farClipPlane = 450f;
        RenderSettings.skybox = normalModeBackground;

        fogColorNormal = new Color32(180, 205, 200, 255);
        fogColorBulletProofMode = new Color32(61, 61, 61, 255);
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 100;
        RenderSettings.fogEndDistance = 250;
        RenderSettings.fogColor = fogColorNormal;

        isChanged = false;
        t = 0;
        // restrictedAreaTimer = .2f;
    }

    /* void Update()
    {
        //exitRestrictedAreaTimer -= Time.deltaTime / 20;
        restrictedAreaTimer -= Time.deltaTime / 50;
    }*/

    void LateUpdate()
    {
        transform.position = Player.instance.transform.position + offset;

        if (Player.instance.mode == (int)Player.Mode.bulletProof && !isChanged)
        {
            t = Mathf.MoveTowards(t, 1f, 0.05f);
            //Debug.Log(t);
            //mainCamera.backgroundColor = Color.Lerp(lightColor, darkColor, t);
            RenderSettings.skybox = bulletProofModeBackground;
            RenderSettings.fogColor = fogColorBulletProofMode;
            if (t >= 1f)
            {
                t = 0;
                isChanged = true;
            }
        }
        else if (Player.instance.mode == (int)Player.Mode.normal && isChanged)
        {
            t = Mathf.MoveTowards(t, 1f, 0.05f);
            //mainCamera.backgroundColor = Color.Lerp(darkColor, lightColor, t);
            RenderSettings.skybox = normalModeBackground;
            RenderSettings.fogColor = fogColorNormal;
            if (t >= 1f)
            {
                t = 0;
                isChanged = false;
            }
        }

        if (RenderSettings.skybox != normalModeBackground && Player.instance.mode == (int)Player.Mode.normal && !isChanged)
        {
            RenderSettings.skybox = normalModeBackground;
        }
    }

    public void changeCameraAngle()
    {
        if (transform.eulerAngles.z == initialZAngleofCamera)
        {
            //transform.eulerAngles = new Vector3(initialXAngleofCamera, initialYAngleofCamera, 178); 
            StartCoroutine(cameraReverse());
            //transform.Translate(0f, 6f, 0f);
        }
        else
        {
            //transform.eulerAngles = new Vector3(initialXAngleofCamera, initialYAngleofCamera, initialZAngleofCamera);
            StartCoroutine(cameraReverse());
            //transform.Translate(0f, -6f, 0f);
        }
    }

    IEnumerator cameraReverse()
    {
        Vector3 rotateValue = new Vector3(0, 0, 10);
        for (int i = 0; i < 18; i++)
        {
            transform.eulerAngles = transform.eulerAngles - rotateValue;
            yield return new WaitForSeconds(.0005f);
        }
        StopCoroutine(cameraReverse());
    }
}

