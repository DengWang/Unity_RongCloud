using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace RongCloud
{

	#if UNITY_ANDROID

	public class RongCloudAndroidBinding
	{
		
		private static AndroidJavaObject _plugin;

		static RongCloudAndroidBinding ()
		{
			if (Application.platform != RuntimePlatform.Android) {
				return;
			}
			using (var pluginClass = new AndroidJavaClass ("com.volvapps.rongcloudplugin.RongCloudPlugin")) {
				_plugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			}
		}








		public static void Init (string appKey)
		{
//			if (Application.platform == RuntimePlatform.Android) {
//				_plugin.Call ("_init");
//			}

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


		public static void SendCmdMessage (RCConversationType conversationType, string targetId, string cmdName, string data) {
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_sendCmdMessage", (int)conversationType, targetId, cmdName, data, "", "");
		}



		public static void SendOperationMessage(RCConversationType conversationType, string targetId, string operatorUserId, string operation, string data, string message, string extra, string pushContent, string pushData){
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_sendOperationMessage", (int)conversationType, targetId, operatorUserId, operation,data,message,extra, pushContent, pushData);
		}

		public static void AddToBlacklist (string userId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_addToBlacklist", userId);
		}

		public static void ClearMessages (RCConversationType conversationType, string targetId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_clearMessages", (int)conversationType, targetId);
		}


		public static void ClearMessagesUnreadStatus (RCConversationType conversationType, string targetId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_clearMessagesUnreadStatus", (int)conversationType, targetId);
		}


		public static void ClearNotifications ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_clearNotifications");
		}



		public static void DeleteMessages (List<long> messageIds)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_deleteMessages", MiniJSON.Json.Serialize(messageIds));
		}



		public static void Disconnect ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_disconnect");
		}

		public static void GetBlacklist ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_getBlacklist");
		}


		public static void GetBlacklistStatus (string userId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_getBlacklistStatus", userId);
		}



		public static void GetConversationNotificationStatus (RCConversationType conversationType, string targetId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_getConversationNotificationStatus", (int)conversationType, targetId);
		}


		public static RCConnectionStatus GetConnectionStatus ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return RCConnectionStatus.ConnectionStatus_Unconnected;
			return  (RCConnectionStatus)_plugin.Call<int> ("_getCurrentConnectionStatus");
		}



		public static string GetCurrentUserId ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return null;
			return  _plugin.Call<string> ("_getCurrentUserId");
		}


		public static long GetDeltaTime ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return 0;
			return  _plugin.Call<long> ("_getDeltaTime");
		}


		public static List<RCMessage> GetHistoryMessages (RCConversationType conversationType, string targetId, int oldestMessageId, int count)
		{
			if (Application.platform != RuntimePlatform.Android)
				return null;
			return  RCUtils.PraseRCMessages (_plugin.Call<string> ("_getHistoryMessages", (int)conversationType, targetId, oldestMessageId, count));
		}


		public static List<RCMessage> GetLatestMessages (RCConversationType conversationType, string targetId, int count)
		{
			if (Application.platform != RuntimePlatform.Android)
				return null;
			return  RCUtils.PraseRCMessages (_plugin.Call<string> ("_getLatestMessages", (int)conversationType, targetId, count));
		}


		public static void GetNotificationQuietHours ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_getNotificationQuietHours");
		}


		public static void GetRemoteHistoryMessages (RCConversationType conversationType, string targetId, long dataTime, int count)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_getRemoteHistoryMessages", (int)conversationType, targetId, dataTime, count);
		}

		public static int GetTotalUnreadCount ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return 0;
			return _plugin.Call<int> ("_getTotalUnreadCount");
		}


		public static int GetUnreadCount (RCConversationType conversationType, string targetId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return 0;
			return _plugin.Call<int> ("_getUnreadCount", (int)conversationType, targetId);
		}

		public static void JoinChatRoom (string chatRoomId, int messageCount)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_joinChatRoom", chatRoomId, messageCount);
		}


		public static void JoinGroup (string groupId, string groupName)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_joinGroup", groupId, groupName);
		}

		public static void Logout ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_logout");
		}


		public static void QuitChatRoom (string chatRoomId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_quitChatRoom", chatRoomId);
		}


		public static void QuitGroup (string groupId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_quitGroup", groupId);
		}

		public static void Reconnect ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_reconnect");
		}


		public static void RemoveFromBlacklist (string userId)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_removeFromBlacklist", userId);
		}


		public static void RemoveNotificationQuietHours ()
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_removeNotificationQuietHours");
		}



		public static void SetConversationNotificationStatus (RCConversationType conversationType, string targetId, RCConversationNotificationStatus  conversationNotificationStatus)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_setConversationNotificationStatus", (int)conversationType, targetId, conversationNotificationStatus);
		}

		public static void SetMessageReceivedStatus (int messageId, RCReceivedStatus receivedStatus)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_setMessageReceivedStatus", messageId, (int)receivedStatus);
		}

		public static void SetMessageSentStatus (int messageId, RCSentStatus sentStatus)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_setMessageSentStatus", messageId, (int)sentStatus);
		}


		public static void  SetNotificationQuietHours (string startTime, int spanMinutes)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_setNotificationQuietHours", startTime, spanMinutes);
		}

		public static void SyncGroups (List<RCGroup> groups)
		{
			if (Application.platform != RuntimePlatform.Android)
				return;
			_plugin.Call ("_syncGroups", MiniJSON.Json.Serialize (groups));
		}
	}

	#endif

}