using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RongCloud;
using System;

public class Demo : MonoBehaviour
{
	#if UNITY_IPHONE
	string token = "z5G1EInN8hTM2BAkCA9FHVuX0L/W2OKmeTvN4IB57Ld228wcoGbTmCVLyy5v6VIQAQ3E1/wg1L0o/CDsOtZj1w==";
	


#elif UNITY_ANDROID
	string token = "pOvqI7u+Rd33EyCrhSnY4y8DOtDiEGalhTK+bjuo4tNlOB5IUNcDRvhASOfkn377LkczdT7ZjCMGy3TYAX/KtZZBTxpUFMv4";
	#endif

	public string appKey = "c9kqb3rdk0ebj";


	public RongCloudManager manager;

	void Awake ()
	{
	
		RongCloudManager.onConnectSuccessEvent += onConnectSuccessEvent;
	}
	// Use this for initialization
	void Start ()
	{
		RongCloudBinding.Init (appKey);
		RongCloudBinding.ConnectWithToken (token);

		#if UNITY_IPHONE
		NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert);
		#endif

	}

	void onConnectSuccessEvent (string userId)
	{
		Debug.Log ("onConnectSuccessEvent : " + userId);
	}

	#if UNITY_IPHONE
	bool tokenSent = false;

	void Update ()
	{
		if (!tokenSent) {
			byte[] token = NotificationServices.deviceToken;
			if (token != null) {
				// send token to a provider
				string tokenString = System.BitConverter.ToString (token).Replace ("-", "");
				Debug.Log ("tokenString : " + tokenString);
			}
		}
	}
	#endif



	void OnGUI ()
	{



		if (GUI.Button (new Rect (50, 50, 150, 50), "SendMessageToUser")) {
			
			RongCloudBinding.SendTextMessage (RCConversationType.ConversationType_PRIVATE, "2", "nihao" + Time.realtimeSinceStartup, Time.realtimeSinceStartup.ToString (), "nihao" + Time.realtimeSinceStartup, "nihao" + Time.realtimeSinceStartup);	

		}

	}

}
