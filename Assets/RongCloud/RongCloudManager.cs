using UnityEngine;
using System.Collections;
using System;

public class RongCloudManager : MonoBehaviour
{


	public static event Action<string> connectSuccessEvent;
	public static event Action connectFailedEvent;
	public static event Action tokenIncorrectEvent;

	public void connectSuccess (string userId)
	{
		Debug.Log ("connectSuccess : " + userId);
		if (connectSuccessEvent != null) {
			connectSuccessEvent (userId);
		}
	}


	public void connectFailed (string empty)
	{
		Debug.Log ("connectFailed");
		if (connectFailedEvent != null) {
			connectFailedEvent ();
		}
	}


	public void tokenIncorrect (string empty)
	{
		Debug.Log ("tokenIncorrect");
		if (tokenIncorrectEvent != null) {
			tokenIncorrectEvent ();
		}
	}

}
