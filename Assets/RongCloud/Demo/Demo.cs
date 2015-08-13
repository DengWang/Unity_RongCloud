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
	string token = "8tHBY+r7m5fozIIoYFjI8noqi9ukpNaQStcltO2Qyr79FUphMYcV16YeZJywhMn4hmMmQ+paVJ5ipNwf7WoZwA==";
	#endif

	public string appKey = "kj7swf8o7cvl2";


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
		Debug.Log ("JoinGroup");
		RCGroup group1 = new RCGroup ("1", "TestGroup", "");
		RCGroup group2 = new RCGroup ("2", "TestGroup2", "");
		RCGroup group3 = new RCGroup ("3", "TestGroup3", "");

		RongCloudBinding.SyncGroups (new List<RCGroup> (){ group1, group2, group3 });

		RongCloudBinding.GetConversationNotificationStatus (RCConversationType.ConversationType_GROUP, "1");
		RongCloudBinding.GetConversationNotificationStatus (RCConversationType.ConversationType_PRIVATE, "2");
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

		GUILayout.Label (manager.currentUserId);


		if (GUI.Button (new Rect (50, 50, 150, 50), "SendMessageToUser")) {

			if (manager.currentUserId == "1") {
				RongCloudBinding.SendTextMessage (RCConversationType.ConversationType_PRIVATE, "2", "nihao" + Time.realtimeSinceStartup, Time.realtimeSinceStartup.ToString (), "nihao" + Time.realtimeSinceStartup,"nihao" + Time.realtimeSinceStartup);	
			} else {
				RongCloudBinding.SendTextMessage (RCConversationType.ConversationType_PRIVATE, "1", "nihao" + Time.realtimeSinceStartup, Time.realtimeSinceStartup.ToString (), "nihao" + Time.realtimeSinceStartup,  "nihao" + Time.realtimeSinceStartup);
			}
		}

		if (GUI.Button (new Rect (350, 50, 150, 50), "SendMessageToGroup")) {
			RongCloudBinding.SendTextMessage (RCConversationType.ConversationType_GROUP, "1", "nihao" + Time.realtimeSinceStartup, Time.realtimeSinceStartup.ToString (), "", "");

		}

//		if (GUI.Button (new Rect (50, 150, 150, 50), "GetRemoteHistoryMessagesForUser")) {
//			long recodeTime = (long)(System.DateTime.Now - new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
//			RongCloudBinding.GetRemoteHistoryMessages (RCConversationType.ConversationType_PRIVATE, "1", recodeTime, 100);
//		}
//
//		if (GUI.Button (new Rect (350, 150, 150, 50), "GetRemoteHistoryMessagesForGroup")) {
//			long recodeTime = (long)(System.DateTime.Now - new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
//			RongCloudBinding.GetRemoteHistoryMessages (RCConversationType.ConversationType_GROUP, "1", recodeTime, 100);
//		}



		if (GUI.Button (new Rect (50, 250, 150, 50), "GetLastMessageForUser")) {
			Debug.Log (RongCloudBinding.GetLatestMessages (RCConversationType.ConversationType_PRIVATE, "1", 20));
		}



		if (GUI.Button (new Rect (350, 250, 150, 50), "GetLastMessageForGroup")) {
			Debug.Log (RongCloudBinding.GetLatestMessages (RCConversationType.ConversationType_GROUP, "1", 20));
		}

		if (GUI.Button (new Rect (50, 350, 150, 50), "JoinGroup")) {
			Debug.Log ("JoinGroup");
			RongCloudBinding.JoinGroup ("1", "TestGroup");

		}


		if (GUI.Button (new Rect (350, 350, 150, 50), "QuitGroup")) {
			RongCloudBinding.QuitGroup ("1");
		}


		if (GUI.Button (new Rect (50, 450, 150, 50), "GetUnreadCount")) {
			Debug.Log ("GetUnreadCount");
			Debug.Log (RongCloudBinding.GetUnreadCount (RCConversationType.ConversationType_GROUP, "1"));

		}



//		if (GUI.Button (new Rect (350, 450, 100, 50), "GetConversationList")) {
//			Debug.Log ("GetConversationList");
//			Debug.Log (RongCloudBinding.GetConversationList (new List<RCConversationType> () {
//				RCConversationType.ConversationType_GROUP,
//				RCConversationType.ConversationType_PRIVATE
//			}));
//
//		}


		if (GUI.Button (new Rect (50, 550, 150, 50), "SyncGroup")) {
			Debug.Log ("SyncGroup");
			RCGroup group1 = new RCGroup ("1", "TestGroup", "");
			RCGroup group2 = new RCGroup ("2", "TestGroup2", "");
			RCGroup group3 = new RCGroup ("3", "TestGroup3", "");

			RongCloudBinding.SyncGroups (new List<RCGroup> (){ group1, group2, group3 });

		}


	}

}
