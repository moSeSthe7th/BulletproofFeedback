using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class InitializationScript : MonoBehaviour {


	void Awake(){
		if (!FB.IsInitialized) {
			FB.Init (() => {
				FB.ActivateApp();
			});
		} else {
			FB.ActivateApp ();
		}


	}


	// Use this for initialization
	void Start () {
		/* Mandatory - set your AppsFlyer’s Developer key. */
		AppsFlyer.setAppsFlyerKey("eYNubRbWMdHRGCzbRTk3TfeYNubRbWMdHRGCzbRTk3Tf");
		/* For detailed logging */
		/* AppsFlyer.setIsDebug (true); */
		#if UNITY_IOS
		/* Mandatory - set your apple app ID
		NOTE: You should enter the number only and not the "ID" prefix */
		AppsFlyer.setAppID ("1434179130");
		AppsFlyer.trackAppLaunch ();
		#elif UNITY_ANDROID
		/* Mandatory - set your Android package name */
		AppsFlyer.setAppID ("com.UtmostGames.Bulletproof");
		/* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
		AppsFlyer.init ("eYNubRbWMdHRGCzbRTk3Tf","AppsFlyerTrackerCallbacks");
		#endif
	}

}
