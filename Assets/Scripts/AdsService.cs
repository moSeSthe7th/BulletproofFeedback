using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsService : MonoBehaviour
{
    private UIScript uIScript;
    private void Awake()
    {
        if (!Advertisement.isInitialized)
        {
#if UNITY_IOS
            Advertisement.Initialize("2774229");  //// 1st parameter is String and 2nd is boolean
#elif UNITY_ANDROID
            Advertisement.Initialize("2774228");  //// 1st parameter is String and 2nd is boolean
#endif
        }
        uIScript = FindObjectOfType(typeof(UIScript)) as UIScript;
    }

    IEnumerator ShowSkipAd()
    {
        float timer = 0;
        while(!Advertisement.IsReady() && timer < 3){
            timer += .5f;
            yield return new WaitForSecondsRealtime(.5f);
        }
        if (Advertisement.IsReady())
        {
            //var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show();
            Debug.LogWarning("Regular add");
        }
        StopCoroutine(ShowSkipAd());

    }
    IEnumerator RewardAd()
    {
        float timer = 0;
        while (!Advertisement.IsReady("rewardedVideo") && timer < 3)
        {
            timer += .2f;
            yield return new WaitForSecondsRealtime(.2f);
        }
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else 
        {
            GameConst.instance.secondPushed = false;
            uIScript.GameOver();
        }
        StopCoroutine(RewardAd());
    }
	public void ShowAd()
	{
        StartCoroutine(ShowSkipAd());
	}

	public void ShowRewardedAd()
	{
		Debug.Log("tusa bastım");
		GameConst.instance.secondPushed = true;
        uIScript.DestroyAdCanvas();
        StartCoroutine(RewardAd());
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");

            Player.instance.StartAll();
            var main = GameObject.FindWithTag("WarpMode").GetComponent<ParticleScript>().warpMode.main;
            main.startSpeed = new ParticleSystem.MinMaxCurve(Player.instance.speed.z * 2, Player.instance.speed.z * 3);
            if (!Player.instance.gameObject.activeInHierarchy)
            {
                Player.instance.gameObject.SetActive(true);
            }
            
			StartCoroutine(GameConst.instance.CountDown());

			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}