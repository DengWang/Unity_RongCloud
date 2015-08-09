using UnityEngine;
using System.Collections;
using RongCloud;
public class Demo : MonoBehaviour
{


	public string token = "hWnrG2D1tj3dNRQizCXxiluX0L/W2OKmeTvN4IB57Ld228wcoGbTmBzyqLcR4Gc2xE+tVa8USSAo/CDsOtZj1w==";
	public string appKey = "kj7swf8o7cvl2";
	// Use this for initialization
	void Start ()
	{
		RongCloudBinding.Init (appKey);
		NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert);
		RongCloudBinding.ConnectWithToken (token);
	}


	bool tokenSent = false;
	void Update () {
		if (!tokenSent) {
			byte[]  token = NotificationServices.deviceToken;
			if (token != null) {
				// send token to a provider
				string tokenString =  System.BitConverter.ToString(token).Replace("-","");
				Debug.Log ("tokenString : " + tokenString);
			}
		}
	}
	



	void OnGUI() {
		if( GUI.Button(new Rect(50,50,100,50),"SendMessage")){
			RongCloudBinding.SendTextMessage (RCConversationType.ConversationType_PRIVATE, "1", "nihao" + Time.realtimeSinceStartup,Time.realtimeSinceStartup.ToString(),"");
		}
	}

}
