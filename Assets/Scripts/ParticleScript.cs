using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

	private GameObject mainCamera;
	private float offset;
	private float positionZ;

    public ParticleSystem warpMode;
	private ParticleSystem.ColorOverLifetimeModule colorModule;

	private Color yellowParticle;
	private Color purpleParticle;

	private Color whiteColor;
	private Color greenParticle;

	private bool isChanged;

	private Gradient gradientA;
	private GradientColorKey[] cGradKeyA;
	private GradientAlphaKey[] aGradKeyA;

	private Gradient gradientB;
	private GradientColorKey[] cGradKeyB;
	private GradientAlphaKey[] aGradKeyB;

	private void Start()
	{


        mainCamera = GameObject.FindWithTag("MainCamera");
		warpMode = GetComponent<ParticleSystem>();
		colorModule = warpMode.colorOverLifetime;
		colorModule.enabled = true;

		transform.position = new Vector3(0f, -10f, mainCamera.gameObject.transform.position.z + 150f);
		offset = transform.position.z - mainCamera.gameObject.transform.position.z;

		yellowParticle = new Color32(245, 245, 150, 255);
		purpleParticle = new Color32(165, 243, 255, 255);


		whiteColor = new Color(255, 240, 120, 255);
		greenParticle = new Color(255, 220, 0, 255);


		//****************************    GRADIENT PARTICLE SYSTEM ICIN     ************************
		gradientA = new Gradient();

		cGradKeyA = new GradientColorKey[2];
		cGradKeyA[0].color = greenParticle;
		cGradKeyA[0].time = 0f;
		cGradKeyA[1].color = whiteColor;
		cGradKeyA[1].time = .7f;

		aGradKeyA = new GradientAlphaKey[2];
		aGradKeyA[0].alpha = 1f;
		aGradKeyA[0].time = 0f;
		aGradKeyA[1].alpha = 1f;
		aGradKeyA[1].time = 0f;

		gradientA.SetKeys(cGradKeyA,aGradKeyA);

		gradientB = new Gradient();

		cGradKeyB = new GradientColorKey[2];
		cGradKeyB[0].color = purpleParticle;
		cGradKeyB[0].time = 0f;
		cGradKeyB[1].color = yellowParticle;
		cGradKeyB[1].time = 1f;

		aGradKeyB = new GradientAlphaKey[2];
		aGradKeyB[0].alpha = 1f;
		aGradKeyB[0].time = 0f;
		aGradKeyB[1].alpha = 1f;
		aGradKeyB[1].time = 1f;

		gradientB.SetKeys(cGradKeyB, aGradKeyB);

		var main = warpMode.main;

		main.startColor = new ParticleSystem.MinMaxGradient(gradientA, gradientA);
        main.startSpeed = new ParticleSystem.MinMaxCurve(Player.instance.speed.z * 2, Player.instance.speed.z * 3);
		main.maxParticles = 20;

		colorModule.color = gradientA;

		isChanged = false;

	}

	private void LateUpdate()
	{
		if (Player.instance.mode == (int)Player.Mode.bulletProof)
		{
			var main = warpMode.main;
            if (!isChanged)
            {
                main.startSpeed = new ParticleSystem.MinMaxCurve(Player.instance.speed.z * 3, Player.instance.speed.z * 4);
                main.startColor = new ParticleSystem.MinMaxGradient(gradientB, gradientB);
                colorModule.color = gradientB;
                isChanged = true;
            }
		}
		else if(Player.instance.mode == (int)Player.Mode.normal)
		{
            var main = warpMode.main;

            if(isChanged)
            {
                main.startSpeed = new ParticleSystem.MinMaxCurve(Player.instance.speed.z * 2, Player.instance.speed.z * 3);
                main.startColor = new ParticleSystem.MinMaxGradient(gradientA, gradientA);
                colorModule.color = gradientA;
                isChanged = false;
            }
		}

		positionZ = mainCamera.transform.position.z + offset;
		transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
	}
}
