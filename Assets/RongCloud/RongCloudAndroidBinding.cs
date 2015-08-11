using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RongCloudAndroidBinding
	{

		private static AndroidJavaObject _plugin;


		static RongCloudAndroidBinding ()
		{
			if (Application.platform != RuntimePlatform.Android) {
				return;
			}
			// find the plugin instance
			using (var pluginClass = new AndroidJavaClass ("com.volvapps.rongcloudplugin.RongCloudPlugin")) {
				_plugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			}
		}


		public static void Init ()
		{
			if (Application.platform != RuntimePlatform.Android) {
				return;
			}
			_plugin.Call ("_init");
		}


		public static void  ConnectWithToken (string token)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_connectWithToken", token);
		}



		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content, string extra, string pushContent, string pushData)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_sendTextMessage", (int)conversationType, targetId, content, extra, pushData, pushData);
		}


	}

}