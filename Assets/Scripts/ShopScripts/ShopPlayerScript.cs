using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayerScript : MonoBehaviour {

	Vector3 strecthXAmount;
	Vector3 initialScale;
	//bool isVibration;
	//UIScript uIScript;
	//private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		//Application.targetFrameRate = 60;
		initialScale = transform.localScale;
		strecthXAmount = new Vector3(initialScale.x + 0.3f, initialScale.y - 0.3f , initialScale.z + 0.3f);
		//audioSource = GetComponent<AudioSource>(); 

		/*if (PlayerPrefs.GetInt("Vibration", 1) == 1)
		{
			isVibration = true;
		}
		else
		{
			isVibration = false;
		}*/
	}

	private void OnCollisionEnter(Collision collision)
	{
		StartCoroutine(ImpactEffectEnterScene());
		/*audioSource.Play();
		if (isVibration)
		{
			VibrationPop.vibrateforDuration(1);
		}*/
	}


	IEnumerator ImpactEffectEnterScene()
	{
		while (Vector3.Distance(initialScale,transform.localScale) < Vector3.Distance(initialScale, strecthXAmount))
		{
			transform.localScale += new Vector3(0.05f, -0.05f, 0.05f);
			yield return new WaitForSeconds(.001f);
		}
		yield return new WaitForSeconds(0.1f);
		while (Vector3.Distance(transform.localScale, initialScale) > 0f)
		{
			transform.localScale += new Vector3(-0.05f, 0.05f, -0.05f);
			yield return new WaitForSeconds(.001f);
		}

		StopCoroutine(ImpactEffectEnterScene());
	}
}
