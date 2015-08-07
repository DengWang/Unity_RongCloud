using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RongCloud
{
	public class RongCloudBinding
	{

		[DllImport ("__Internal")]
		private static extern void _init (string appKey);

		public static void Init (string appKey)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_init (appKey);
		}


		[DllImport ("__Internal")]
		private static extern void _setDeviceToken (string deviceToken);

		public static void SetDeviceToken (string deviceToken)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_setDeviceToken (deviceToken);
		}


		[DllImport ("__Internal")]
		private static extern void _connectToRongCloudServer (string token);

		public static void ConnectToRongCloudServer (string token)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_connectToRongCloudServer (token);
		}

		[DllImport ("__Internal")]
		private static extern void _sendTextMessage (int conversationType, string targetId, string content,string extra);

		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content,string extra)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendTextMessage ((int)conversationType, targetId, content,extra);
		}

	}
}