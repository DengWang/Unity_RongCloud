using UnityEngine;
using System.Collections;
using System;

namespace RongCloud
{
	public class RongCloudManager : MonoBehaviour
	{

		public static event Action<string> onConnectSuccessEvent;

		public void onConnectSuccess (string userId)
		{
			Debug.Log ("connectSuccess : " + userId);
			if (onConnectSuccessEvent != null) {
				onConnectSuccessEvent (userId);
			}
		}

		public static event Action<RCConnectErrorCode> onConnectFailedEvent;

		public void onConnectFailed (string status)
		{
			var errorCode = (RCConnectErrorCode)int.Parse (status);
			Debug.Log ("connectFailed : " + errorCode);
			if (onConnectFailedEvent != null) {
				onConnectFailedEvent (errorCode);
			}
		}

		public static event Action onTokenIncorrectEvent;

		public void onTokenIncorrect (string empty)
		{
			Debug.Log ("tokenIncorrect");
			if (onTokenIncorrectEvent != null) {
				onTokenIncorrectEvent ();
			}
		}

		public static event Action<RCConnectionStatus> onConnectionStatusChangedEvent;

		public void onConnectionStatusChanged (string status)
		{
			var val = (RCConnectionStatus)int.Parse (status);
			Debug.Log ("onConnectionStatusChanged : " + val);
			if (onConnectionStatusChangedEvent != null) {
				onConnectionStatusChangedEvent (val);
			}
		}

		public static event Action<string> onSendMessageSuccessEvent;

		public void onSendMessageSuccess (string messageId)
		{
			Debug.Log ("onSendMessageSuccess : " + messageId);
			if (onSendMessageSuccessEvent != null) {
				onSendMessageSuccessEvent (messageId);
			}
		}

		public static event Action<RCErrorCode> onSendMessageFailedEvent;

		public void onSendMessageFailed (string status)
		{
			var val = (RCErrorCode)int.Parse (status);
			Debug.Log ("sendMessageFailed : " + val);
			if (onSendMessageFailedEvent != null) {
				onSendMessageFailedEvent (val);
			}
		}

		public static event Action<string> onTextReceivedEvent;

		public void onTextReceived (string messageJson)
		{
			Debug.Log ("onTextReceived : " + messageJson);
			if (onTextReceivedEvent != null) {
				onTextReceivedEvent (messageJson);
			}
		}

	}
}