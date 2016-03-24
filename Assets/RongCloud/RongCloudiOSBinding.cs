using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RongCloud
{

	#if UNITY_IPHONE
	public class RongCloudiOSBinding
	{


		[DllImport ("__Internal")]
		private static extern string _getCurrentUserInfo ();

		public static RCUserInfo GetCurrentUserInfo ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return  RCUserInfo.DecodeFromJson (_getCurrentUserInfo ());
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
		private static extern void _disconnect ();

		public static void Disconnect ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_disconnect ();
		}


		[DllImport ("__Internal")]
		private static extern void _logout ();

		public static void Logout ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_logout ();
		}




		[DllImport ("__Internal")]
		private static extern void _sendTextMessage (int conversationType, string targetId, string content, string extra, string pushContent, string pushData);

		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content, string extra, string pushContent, string pushData)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendTextMessage ((int)conversationType, targetId, content, extra, pushContent, pushData);
		}



		[DllImport ("__Internal")]
		private static extern void _sendCmdMessage (int conversationType, string targetId, string cmdName, string data, string pushContent, string pushData);

		public static void SendCmdMessage (RCConversationType conversationType, string targetId, string cmdName, string data)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendCmdMessage ((int)conversationType, targetId, cmdName, data, "", "");
		}


		[DllImport ("__Internal")]
		private static extern void _sendOperationMessage (int conversationType, string targetId, string operatorUserId, string operation, string data, string message, string extra, string pushContent, string pushData);

		public static void SendOperationMessage (RCConversationType conversationType, string targetId, string operatorUserId, string operation, string data, string message, string extra, string pushContent, string pushData)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendOperationMessage ((int)conversationType, targetId, operatorUserId, operation, data, message, extra, pushContent, pushData);
		}


		[DllImport ("__Internal")]
		private static extern void _sendRequestMessage (int conversationType, string targetId, string operatorUserId, string operatorUserAlias, string data, string message, string extra, string pushContent, string pushData);

		public static void SendRequestMessage (RCConversationType conversationType, string targetId, string operatorUserId, string operatorUserAlias, string data, string message, string extra, string pushContent, string pushData)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				_sendRequestMessage ((int)conversationType, targetId, operatorUserId, operatorUserAlias, data, message, extra, pushContent, pushData);
		}


		[DllImport ("__Internal")]
		private static extern string _getLatestMessages (int conversationType, string targetId, int count);

		public static void GetLatestMessages (RCConversationType conversationType, string targetId, int count)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				string json = _getLatestMessages ((int)conversationType, targetId, count);
				RongCloudManager.inst.onGetLatestMessagesSuccess (json);

			}
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
		private static extern bool _setMessageReceivedStatus (long  messageId, int receivedStatus);

		public static bool SetMessageReceivedStatus (long messageId, RCReceivedStatus receivedStatus)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _setMessageReceivedStatus (messageId, (int)receivedStatus);
			}
			return false;
		}

		[DllImport ("__Internal")]
		private static extern bool _setMessageSentStatus (long  messageId, int receivedStatus);

		public static bool SetMessageSentStatus (long messageId, RCSentStatus sentStatus)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _setMessageSentStatus (messageId, (int)sentStatus);
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

		public static void SetConversationNotificationStatus (RCConversationType conversationType, string targetId, RCConversationNotificationStatus conversationNotificationStatus)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				bool isBlocked = (conversationNotificationStatus == RCConversationNotificationStatus.DO_NOT_DISTURB);
				_setConversationNotificationStatus ((int)conversationType, targetId, isBlocked);
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




		[DllImport ("__Internal")]
		private static extern void _joinChatRoom (string targetId, int messageCount);

		public static void JoinChatRoom (string targetId, int messageCount)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_joinChatRoom (targetId, messageCount);
			}
		}

		[DllImport ("__Internal")]
		private static extern void _quitChatRoom (string targetId);

		public static void QuitChatRoom (string targetId)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_quitChatRoom (targetId);
			}
		}



		[DllImport ("__Internal")]
		private static extern void _getNotificationQuietHours ();

		public static void GetNotificationQuietHours ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_getNotificationQuietHours ();
			}
		}



		[DllImport ("__Internal")]
		private static extern void _setNotificationQuietHours (string startTime, int spanMins);

		public static void SetNotificationQuietHours (string startTime, int spanMins)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setNotificationQuietHours (startTime, spanMins);
			}
		}


		[DllImport ("__Internal")]
		private static extern void _removeNotificationQuietHours ();

		public static void RemoveNotificationQuietHours ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_removeNotificationQuietHours ();
			}
		}
	}
	#endif
}