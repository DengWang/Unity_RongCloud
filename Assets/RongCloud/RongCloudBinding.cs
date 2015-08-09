using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RongCloud
{
	public class RongCloudBinding
	{


		[DllImport ("__Internal")]
		private static extern string _getCurrentUserInfo ();

		public static RCUserInfo GetCurrentUserInfo ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return	RCUserInfo.DecodeFromJson (_getCurrentUserInfo ());
			}
			return null;
		}



		[DllImport ("__Internal")]
		private static extern int _getSdkRunningMode ();

		public static RCSDKRunningMode GetSdkRunningMode ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return (RCSDKRunningMode)_getSdkRunningMode ();
			}
			return RCSDKRunningMode.RCSDKRunningMode_Foregroud;
		}



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
		private static extern void _connectWithToken (string token);

		public static void ConnectWithToken (string token)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_connectWithToken (token);
		}




		[DllImport ("__Internal")]
		private static extern void _disconnect (bool isReceivePush);

		public static void Disconnect (bool isReceivePush)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_disconnect (isReceivePush);
		}

		[DllImport ("__Internal")]
		private static extern void _disconnectWithoutParam ();

		public static void Disconnect ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_disconnectWithoutParam ();
		}


		[DllImport ("__Internal")]
		private static extern void _logout ();

		public static void Logout ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_logout ();
		}


		[DllImport ("__Internal")]
		private static extern void _sendTextMessage (int conversationType, string targetId, string content, string extra, string pushContent);

		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content, string extra, string pushContent)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendTextMessage ((int)conversationType, targetId, content, extra, pushContent);
		}

		[DllImport ("__Internal")]
		private static extern void _sendTextMessageWithPushData (int conversationType, string targetId, string content, string extra, string pushContent, string pushData);

		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content, string extra, string pushContent, string pushData)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendTextMessageWithPushData ((int)conversationType, targetId, content, extra, pushContent, pushData);
		}



		[DllImport ("__Internal")]
		private static extern int _getTotalUnreadCount ();

		public static int GetTotalUnreadCount ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _getTotalUnreadCount ();
			}
			return 0;
		}


		[DllImport ("__Internal")]
		private static extern int _getUnreadCount (int conversationType, string targetId);

		public static int GetUnreadCount (RCConversationType conversationType, string targetId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _getUnreadCount ((int)conversationType, targetId);
			}
			return 0;
		}

		[DllImport ("__Internal")]
		private static extern int _getUnreadCountWithoutTargetId (string  conversationTypeList);

		public static int GetUnreadCount (List<RCConversationType> conversationTypeList)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				List<int> tempConversationTypeList = new List<int> ();
				foreach (var item in conversationTypeList) {
					tempConversationTypeList.Add ((int)item);
				}
				return _getUnreadCountWithoutTargetId (MiniJSON.Json.Serialize (tempConversationTypeList));
			}
			return 0;
		}


		[DllImport ("__Internal")]
		private static extern string _getLatestMessages (int conversationType, string targetId, int count);

		public static string GetLatestMessages (RCConversationType conversationType, string targetId, int count)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _getLatestMessages ((int)conversationType, targetId, count);
			}
			return null;
		}

		[DllImport ("__Internal")]
		private static extern string _getHistoryMessages (int conversationType, string targetId, long messageId, int count);

		public static string GetHistoryMessages (RCConversationType conversationType, string targetId, long messageId, int count)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _getHistoryMessages ((int)conversationType, targetId, messageId, count);
			}
			return null;
		}


		[DllImport ("__Internal")]
		private static extern bool _deleteMessages (string messageIds);

		public static bool DeleteMessages (List<long> messageIds)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _deleteMessages (MiniJSON.Json.Serialize (messageIds));
			}
			return false;
		}


		[DllImport ("__Internal")]
		private static extern bool _clearMessages (int conversationType, string targetId);

		public static bool ClearMessages (RCConversationType conversationType, string targetId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _clearMessages ((int)conversationType, targetId);
			}
			return false;
		}


		[DllImport ("__Internal")]
		private static extern bool _clearMessagesUnreadStatus (int conversationType, string targetId);

		public static bool ClearMessagesUnreadStatus (RCConversationType conversationType, string targetId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _clearMessagesUnreadStatus ((int)conversationType, targetId);
			}
			return false;
		}


		[DllImport ("__Internal")]
		private static extern bool _setMessageReceivedStatus (long	messageId, int receivedStatus);

		public static bool SetMessageReceivedStatus (long messageId, RCReceivedStatus receivedStatus)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _setMessageReceivedStatus (messageId, (int)receivedStatus);
			}
			return false;
		}




		[DllImport ("__Internal")]
		private static extern void _getConversationNotificationStatus (int conversationType, string targetId);

		public static void GetConversationNotificationStatus (RCConversationType conversationType, string targetId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_getConversationNotificationStatus ((int)conversationType, targetId);
			}
		}



		[DllImport ("__Internal")]
		private static extern void _setConversationNotificationStatus (int conversationType, string targetId, bool isBlocked);

		public static void SetConversationNotificationStatus (RCConversationType conversationType, string targetId, bool isBlocked)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setConversationNotificationStatus ((int)conversationType, targetId, isBlocked);
			}
		}



		[DllImport ("__Internal")]
		private static extern void _syncGroups (string groupList);

		public static void SyncGroups (List<RCGroup> groupList)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_syncGroups (MiniJSON.Json.Serialize (groupList));
			}
		}


		[DllImport ("__Internal")]
		private static extern void _joinGroup (string groupId, string groupName);

		public static void JoinGroup (string groupId, string groupName)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_joinGroup (groupId, groupName);
			}
		}


		[DllImport ("__Internal")]
		private static extern void _quitGroup (string groupId);

		public static void QuitGroup (string groupId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_quitGroup (groupId);
			}
		}


		[DllImport ("__Internal")]
		private static extern int  _getCurrentNetworkStatus ();

		public static RCNetworkStatus GetCurrentNetworkStatus ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return (RCNetworkStatus)_getCurrentNetworkStatus ();
			}
			return RCNetworkStatus.RC_NotReachable;
		}


		[DllImport ("__Internal")]
		private static extern void _addToBlacklist (string userId);

		public static void AddToBlacklist (string userId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_addToBlacklist (userId);
			}
		}


		[DllImport ("__Internal")]
		private static extern void _removeFromBlacklist (string userId);

		public static void RemoveFromBlacklist (string userId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_removeFromBlacklist (userId);
			}
		}


		[DllImport ("__Internal")]
		private static extern void _getBlacklistStatus (string userId);

		public static void GetBlacklistStatus (string userId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_getBlacklistStatus (userId);
			}
		}


		[DllImport ("__Internal")]
		private static extern void _getBlacklist ();

		public static void GetBlacklist ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_getBlacklist ();
			}
		}

		[DllImport ("__Internal")]
		private static extern int  _getConnectionStatus ();

		public static RCConnectionStatus GetConnectionStatus ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return (RCConnectionStatus)_getConnectionStatus ();
			}
			return RCConnectionStatus.ConnectionStatus_Unconnected;
		}


		[DllImport ("__Internal")]
		private static extern void _getRemoteHistoryMessages (int conversationType, string targetId, long recordTime, int count);

		public static void GetRemoteHistoryMessages (RCConversationType conversationType, string targetId, long recordTime, int count)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_getRemoteHistoryMessages ((int)conversationType, targetId, recordTime, count);
			}
		}
	}
}