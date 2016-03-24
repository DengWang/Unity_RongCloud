using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RongCloud
{
	#if UNITY_IPHONE
	using Binding = RongCloudiOSBinding;

	
	#elif UNITY_ANDROID
	using Binding = RongCloudAndroidBinding;
	#endif

	public class RongCloudBinding
	{

		public static void Init (string appKey)
		{
			Binding.Init (appKey);
		}

		public static void  ConnectWithToken (string token)
		{
			Binding.ConnectWithToken (token);
		}

		public static void SendTextMessage (RCConversationType conversationType, string targetId, string content, string extra, string pushContent, string pushData)
		{
			Binding.SendTextMessage (conversationType, targetId, content, extra, pushContent, pushData);
		}

		public static void SendCmdMessage (RCConversationType conversationType, string targetId, string cmdName, string data)
		{
			Binding.SendCmdMessage (conversationType, targetId, cmdName, data);
		}


		public static void SendOperationMessage (RCConversationType conversationType, string targetId, string operatorUserId, string operation, string data, string message, string extra, string pushContent, string pushData)
		{
			Binding.SendOperationMessage (conversationType, targetId, operatorUserId, operation, data, message, extra, pushContent, pushData);
		}


		public static void SendRequestMessage (RCConversationType conversationType, string targetId, string operatorUserId, string operatorUserAlias, string data, string message, string extra, string pushContent, string pushData)
		{
			Binding.SendRequestMessage (conversationType, targetId, operatorUserId, operatorUserAlias, data, message, extra, pushContent, pushData);
		}


		public static void AddToBlacklist (string userId)
		{
			Binding.AddToBlacklist (userId);
		}

		public static void ClearMessages (RCConversationType conversationType, string targetId)
		{
			Binding.ClearMessages (conversationType, targetId);
		}

		public static void ClearMessagesUnreadStatus (RCConversationType conversationType, string targetId)
		{
			Binding.ClearMessagesUnreadStatus (conversationType, targetId);
		}

		#if UNITY_ANDROID
		public static void ClearNotifications ()
		{
			RongCloudAndroidBinding.ClearNotifications ();
		}
		#endif

		public static void DeleteMessages (List<long> messageIds)
		{
			Binding.DeleteMessages (messageIds);
		}


		public static void Disconnect ()
		{
			Binding.Disconnect ();
		}


		public static void Logout ()
		{
			Binding.Logout ();
		}


		public static void GetBlacklist ()
		{
			Binding.GetBlacklist ();
		}


		public static void GetBlacklistStatus (string userId)
		{
			Binding.GetBlacklistStatus (userId);
		}


		public static void GetConversationNotificationStatus (RCConversationType conversationType, string targetId)
		{
			Binding.GetConversationNotificationStatus (conversationType, targetId);
		}



		public static RCConnectionStatus GetConnectionStatus ()
		{
			return Binding.GetConnectionStatus ();
		}

		#if UNITY_ANDROID
		public static string GetCurrentUserId ()
		{

			return RongCloudAndroidBinding.GetCurrentUserId ();
		}
		#endif

		#if UNITY_IPHONE
		public static RCUserInfo GetCurrentUserInfo ()
		{
			return  RongCloudiOSBinding.GetCurrentUserInfo ();
		}
		#endif



		public static void GetLatestMessages (RCConversationType conversationType, string targetId, int count)
		{
			Binding.GetLatestMessages (conversationType, targetId, count);
		}

		public static void GetNotificationQuietHours ()
		{
			Binding.GetNotificationQuietHours ();
		}

		public static void GetRemoteHistoryMessages (RCConversationType conversationType, string targetId, long dataTime, int count)
		{
			Binding.GetRemoteHistoryMessages (conversationType, targetId, dataTime, count);
		}



		public static void joinChatRoom (string chatRoomId, int messageCount)
		{
			Binding.JoinChatRoom (chatRoomId, messageCount);
		}



		public static void QuitChatRoom (string chatRoomId)
		{
			Binding.QuitChatRoom (chatRoomId);
		}




		#if UNITY_ANDROID
		public static void Reconnect ()
		{
			RongCloudAndroidBinding.Reconnect ();
		}
		#endif



		public static void RemoveFromBlacklist (string userId)
		{
			Binding.RemoveFromBlacklist (userId);
		}

		public static void RemoveNotificationQuietHours ()
		{
			Binding.RemoveNotificationQuietHours ();
		}


		public static void SetConversationNotificationStatus (RCConversationType conversationType, string targetId, RCConversationNotificationStatus  conversationNotificationStatus)
		{
			Binding.SetConversationNotificationStatus (conversationType, targetId, conversationNotificationStatus);
		}


		public static void SetMessageReceivedStatus (int messageId, RCReceivedStatus receivedStatus)
		{
			Binding.SetMessageReceivedStatus (messageId, receivedStatus);
		}


		public static void SetMessageSentStatus (int messageId, RCSentStatus sentStatus)
		{
			Binding.SetMessageSentStatus (messageId, sentStatus);
		}



		public static void  SetNotificationQuietHours (string startTime, int spanMinutes)
		{
			Binding.SetNotificationQuietHours (startTime, spanMinutes);
		}



	}
}