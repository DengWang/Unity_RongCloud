using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
}
