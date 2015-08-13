using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RongCloud
{
	public class RongCloudManager : MonoBehaviour
	{

		public string currentUserId {
			get;
			set;
		}


		void Awake ()
		{
			DontDestroyOnLoad (this);
			gameObject.name = this.GetType ().Name;
		}


		public static event Action<string> onConnectSuccessEvent;

		public void onConnectSuccess (string userId)
		{
			currentUserId = userId;
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

		public static event Action<long> onSendTextMessageSuccessEvent;
		public static event Action<RCMessage> onSentEvent;
		public static event Action<long,RCErrorCode> onSendTextMessageFailedEvent;


		public void onSendTextMessageSuccess (string messageIdStr)
		{
			Debug.Log ("onSendTextMessageSuccess : " + messageIdStr);
			long messageId = long.Parse (messageIdStr);
			if (MessagesPool.MessageSendPool.ContainsKey (messageId)) {
				MessagesPool.MessageSendPool [messageId].sentStatus = RCSentStatus.SentStatus_SENT;
				if (onSentEvent != null) {
					onSentEvent (MessagesPool.MessageSendPool [messageId]);
				}
			}
			if (onSendTextMessageSuccessEvent != null) {
				onSendTextMessageSuccessEvent (messageId);
			}
		}


		public void onSendTextMessageFailed (string json)
		{
			
			Debug.Log ("onSendTextMessageFailed : " + json);

			Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
			long messageId = long.Parse (dict ["messageId"].ToString ());
			RCErrorCode errorCode = (RCErrorCode)int.Parse (dict ["errorCode"].ToString ());
			if (MessagesPool.MessageSendPool.ContainsKey (messageId)) {
				MessagesPool.MessageSendPool [messageId].sentStatus = RCSentStatus.SentStatus_FAILED;
			}
			if (onSendTextMessageFailedEvent != null) {
				onSendTextMessageFailedEvent (messageId, errorCode);
			}
		}

		//		public static event Action<RCErrorCode> onSendTextMessageResultFailedEvent;

		//		public void onSendTextMessageResultFailed (string status)
		//		{
		//			var val = (RCErrorCode)int.Parse (status);
		//			Debug.Log ("onSendTextMessageResultFailed : " + val);
		//			if (onSendTextMessageResultFailedEvent != null) {
		//				onSendTextMessageResultFailedEvent (val);
		//			}
		//		}
		//
		//

		//发送消息的回调，不管消息有没有成功
		public static event Action<RCMessage> onSendTextMessageResultEvent;

		public void onSendTextMessageResult (string json)
		{
			Debug.Log ("onSendTextMessageResult : " + json);
			RCMessage message = RCMessage.DecodeFromJson (json);
			MessagesPool.MessageSendPool.Add (message.messageId, message);
			if (onSendTextMessageResultEvent != null) {
				onSendTextMessageResultEvent (message);
			}
		}


		public static event Action<RCMessage> onReceivedEvent;

		public void onReceived (string messageJson)
		{
			Debug.Log ("onReceived : " + messageJson);
			if (onReceivedEvent != null) {
				onReceivedEvent (RCMessage.DecodeFromJson (messageJson));
			}
		}



		public static event Action<RCUserInfo> onGetUserInfoSuccessEvent;

		public void onGetUserInfoSuccess (string json)
		{
			Debug.Log ("onGetUserInfoSuccess : " + json);
			if (onGetUserInfoSuccessEvent != null) {
				onGetUserInfoSuccessEvent (RCUserInfo.DecodeFromJson (json));
			}
		}


		public static event Action<RCErrorCode> onGetUserInfoFailedEvent;

		public void onGetUserInfoFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetUserInfoFailed : " + val);
			if (onGetUserInfoFailedEvent != null) {
				onGetUserInfoFailedEvent (val);
			}
		}


		public static event Action<RCConversationNotificationStatus> onGetConversationNotificationStatusSuccessEvent;
		public static event Action<RCErrorCode> onGetConversationNotificationStatusFailedEvent;

		public void onGetConversationNotificationStatusSuccess (string status)
		{
			var val = (RCConversationNotificationStatus)int.Parse (status);
			Debug.Log ("onGetConversationNotificationStatusSuccess : " + val);
			if (onGetConversationNotificationStatusSuccessEvent != null) {
				onGetConversationNotificationStatusSuccessEvent (val);
			}
		}

		public void onGetConversationNotificationStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetConversationNotificationStatusFailed : " + val);
			if (onGetConversationNotificationStatusFailedEvent != null) {
				onGetConversationNotificationStatusFailedEvent (val);
			}
		}



		public static event Action<RCConversationNotificationStatus> onSetConversationNotificationStatusSuccessEvent;
		public static event Action<RCErrorCode> onSetConversationNotificationStatusFailedEvent;

		public void onSetConversationNotificationStatusSuccess (string status)
		{
			var val = (RCConversationNotificationStatus)int.Parse (status);
			Debug.Log ("onSetConversationNotificationStatusSuccess : " + val);
			if (onSetConversationNotificationStatusSuccessEvent != null) {
				onSetConversationNotificationStatusSuccessEvent (val);
			}
		}

		public void onSetConversationNotificationStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onSetConversationNotificationStatusFailed : " + val);
			if (onSetConversationNotificationStatusFailedEvent != null) {
				onSetConversationNotificationStatusFailedEvent (val);
			}
		}

		public static event Action onSyncGroupsSuccessEvent;
		public static event Action<RCErrorCode> onSyncGroupsFailedEvent;


		public void onSyncGroupsSuccess (string empty)
		{
			Debug.Log ("onSyncGroupsSuccess");
			if (onSyncGroupsSuccessEvent != null) {
				onSyncGroupsSuccessEvent ();
			}
		}

		public void onSyncGroupsFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onSyncGroupsFailedEvent : " + val);
			if (onSyncGroupsFailedEvent != null) {
				onSyncGroupsFailedEvent (val);
			}
		}


		public static event Action onJoinGroupSuccessEvent;

		public static event Action<RCErrorCode> onJoinGroupFailedEvent;

		public void onJoinGroupSuccess (string empty)
		{
			Debug.Log ("onJoinGroupSuccess");
			if (onJoinGroupSuccessEvent != null) {
				onJoinGroupSuccessEvent ();
			}
		}


		public void onJoinGroupFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onJoinGroupFailed : " + val);
			if (onJoinGroupFailedEvent != null) {
				onJoinGroupFailedEvent (val);
			}
		}


		public static event Action onQuitGroupSuccessEvent;

		public static event Action<RCErrorCode> onQuitGroupFailedEvent;

		public void onQuitGroupSuccess (string empty)
		{
			Debug.Log ("onQuitGroupSuccess");
			if (onQuitGroupSuccessEvent != null) {
				onQuitGroupSuccessEvent ();
			}
		}


		public void onQuitGroupFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onQuitGroupFailed : " + val);
			if (onQuitGroupFailedEvent != null) {
				onQuitGroupFailedEvent (val);
			}
		}



		public static event Action onAddToBlacklistSuccessEvent;

		public static event Action<RCErrorCode> onAddToBlacklistFailedEvent;

		public void onAddToBlacklistSuccess (string empty)
		{
			Debug.Log ("onAddToBlacklistSuccess");
			if (onAddToBlacklistSuccessEvent != null) {
				onAddToBlacklistSuccessEvent ();
			}
		}


		public void onAddToBlacklistFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onAddToBlacklistFailed : " + val);
			if (onAddToBlacklistFailedEvent != null) {
				onAddToBlacklistFailedEvent (val);
			}
		}



		public static event Action onRemoveFromBlacklistSuccessEvent;

		public static event Action<RCErrorCode> onRemoveFromBlacklistFailedEvent;

		public void onRemoveFromBlacklistSuccess (string empty)
		{
			Debug.Log ("onRemoveFromBlacklistSuccess");
			if (onRemoveFromBlacklistSuccessEvent != null) {
				onRemoveFromBlacklistSuccessEvent ();
			}
		}


		public void onRemoveFromBlacklistFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onRemoveFromBlacklistFailed : " + val);
			if (onRemoveFromBlacklistFailedEvent != null) {
				onRemoveFromBlacklistFailedEvent (val);
			}
		}



		public static event Action<int> onGetBlacklistStatusSuccessEvent;

		public static event Action<RCErrorCode> onGetBlacklistStatusFailedEvent;

		public void onGetBlacklistStatusSuccess (string status)
		{
			Debug.Log ("onGetBlacklistStatusSuccess :" + status);
			if (onGetBlacklistStatusSuccessEvent != null) {
				onGetBlacklistStatusSuccessEvent (int.Parse (status));
			}
		}


		public void onGetBlacklistStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetBlacklistStatusFailed : " + val);
			if (onGetBlacklistStatusFailedEvent != null) {
				onGetBlacklistStatusFailedEvent (val);
			}
		}




		public static event Action<List<string>> onGetBlacklistSuccessEvent;

		public static event Action<RCErrorCode> onGetBlacklistFailedEvent;

		public void onGetBlacklistSuccess (string json)
		{
			Debug.Log ("onGetBlacklistSuccess :" + json);
			if (onGetBlacklistSuccessEvent != null) {
				List<object> tempList = MiniJSON.Json.Deserialize (json) as List<object>;
				List<string> userIdList = new List<string> (tempList.Count);
				foreach (var item in tempList) {
					userIdList.Add (item.ToString ());
				}
				onGetBlacklistSuccessEvent (userIdList);
			}
		}


		public void onGetBlacklistFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetBlacklistFailed : " + val);
			if (onGetBlacklistFailedEvent != null) {
				onGetBlacklistFailedEvent (val);
			}
		}

		public static event Action<List<RCMessage>> onGetRemoteHistoryMessagesSuccessEvent;

		public void onGetRemoteHistoryMessagesSuccess (string json)
		{
			Debug.Log ("onGetRemoteHistoryMessagesSuccess : " + json);
			if (onGetRemoteHistoryMessagesSuccessEvent != null) {
				List<RCMessage> messages = RCUtils.PraseRCMessages (json);
				onGetRemoteHistoryMessagesSuccessEvent (messages);
			}
		}


		public static event Action<RCErrorCode> onGetRemoteHistoryMessagesFailedEvent;

		public void onGetRemoteHistoryMessagesFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetRemoteHistoryMessagesFailed : " + val);
			if (onGetRemoteHistoryMessagesFailedEvent != null) {
				onGetRemoteHistoryMessagesFailedEvent (val);
			}
		
		}




		public static event Action onJoinChatRoomSuccessEvent;

		public static event Action<RCErrorCode> onJoinChatRoomFailedEvent;

		public void onJoinChatRoomSuccess (string empty)
		{
			Debug.Log ("onJoinChatRoomSuccess");
			if (onJoinChatRoomSuccessEvent != null) {
				onJoinChatRoomSuccessEvent ();
			}
		}

		public void onJoinChatRoomFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onJoinChatRoomFailed : " + val);
			if (onJoinChatRoomFailedEvent != null) {
				onJoinChatRoomFailedEvent (val);
			}
		}



		public static event Action onQuitChatRoomSuccessEvent;

		public static event Action<RCErrorCode> onQuitChatRoomFailedEvent;

		public void onQuitChatRoomSuccess (string empty)
		{
			Debug.Log ("onQuitChatRoomSuccess");
			if (onQuitChatRoomSuccessEvent != null) {
				onQuitChatRoomSuccessEvent ();
			}
		}

		public void onQuitChatRoomFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onQuitChatRoomFailed : " + val);
			if (onQuitChatRoomFailedEvent != null) {
				onQuitChatRoomFailedEvent (val);
			}
		}


		#region Android Add

		public static event Action onClearMessagesUnreadStatusSuccessEvent;
		public static event Action<RCErrorCode> onClearMessagesUnreadStatusFailedEvent;




		public void onClearMessagesUnreadStatusSuccess (string empty)
		{
			Debug.Log ("onClearMessagesUnreadStatusSuccess");
			if (onClearMessagesUnreadStatusSuccessEvent != null) {
				onClearMessagesUnreadStatusSuccessEvent ();
			}
		}

		public void onClearMessagesUnreadStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onClearMessagesUnreadStatusFailed : " + val);
			if (onClearMessagesUnreadStatusFailedEvent != null) {
				onClearMessagesUnreadStatusFailedEvent (val);
			}
		}

		public static event Action<bool> onDeleteMessagesSuccessEvent;
		public static event Action<RCErrorCode> onDeleteMessagesFailedEvent;



		public void onDeleteMessagesSuccess (string status)
		{
			Debug.Log ("onDeleteMessagesSuccess : " + status);
			if (onDeleteMessagesSuccessEvent != null) {
				onDeleteMessagesSuccessEvent (bool.Parse (status));
			}
		}

		public void onDeleteMessagesFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onDeleteMessagesFailed : " + val);
			if (onDeleteMessagesFailedEvent != null) {
				onDeleteMessagesFailedEvent (val);
			}
		}




		public static event Action<string,int> onGetNotificationQuietHoursSuccessEvent;
		public static event Action<RCErrorCode> onGetNotificationQuietHoursFailedEvent;



		public void onGetNotificationQuietHoursSuccess (string json)
		{
			Debug.Log ("onGetNotificationQuietHoursSuccess : " + json);
			if (onGetNotificationQuietHoursSuccessEvent != null) {
				Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
				onGetNotificationQuietHoursSuccessEvent (dict ["startTime"].ToString (), int.Parse (dict ["spanMinutes"].ToString ()));
			}
		}

		public void onGetNotificationQuietHoursFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onGetNotificationQuietHoursFailed : " + val);
			if (onGetNotificationQuietHoursFailedEvent != null) {
				onGetNotificationQuietHoursFailedEvent (val);
			}
		}


		public static event Action<string> onReConnectSuccessEvent;

		public void onReConnectSuccess (string userId)
		{
			Debug.Log ("onReConnectSuccess : " + userId);
			if (onReConnectSuccessEvent != null) {
				onReConnectSuccessEvent (userId);
			}
		}

		public static event Action<RCConnectErrorCode> onReConnectFailedEvent;

		public void onReConnectFailed (string status)
		{
			var errorCode = (RCConnectErrorCode)int.Parse (status);
			Debug.Log ("onReConnectFailed : " + errorCode);
			if (onReConnectFailedEvent != null) {
				onReConnectFailedEvent (errorCode);
			}
		}

		public static event Action onReTokenIncorrectEvent;

		public void onReTokenIncorrect (string empty)
		{
			Debug.Log ("onReTokenIncorrect");
			if (onReTokenIncorrectEvent != null) {
				onReTokenIncorrectEvent ();
			}
		}


		public static event Action onRemoveNotificationQuietHoursSuccessEvent;
		public static event Action<RCErrorCode> onRemoveNotificationQuietHoursFailedEvent;


		public void onRemoveNotificationQuietHoursSuccess (string empty)
		{
			Debug.Log ("onRemoveNotificationQuietHoursSuccess");
			if (onRemoveNotificationQuietHoursSuccessEvent != null) {
				onRemoveNotificationQuietHoursSuccessEvent ();
			}
		}


		public void onRemoveNotificationQuietHoursFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onRemoveNotificationQuietHoursFailed : " + val);
			if (onRemoveNotificationQuietHoursFailedEvent != null) {
				onRemoveNotificationQuietHoursFailedEvent (val);
			}
		}



		public static event Action<bool> onSetMessageReceivedStatusSuccessEvent;
		public static event Action<bool> onSetMessageSentStatusSuccessEvent;

		public static event Action<RCErrorCode> onSetMessageReceivedStatusFailedEvent;
		public static event Action<RCErrorCode> onSetMessageSentStatusFailedEvent;


		public void onSetMessageReceivedStatusSuccess (string status)
		{
			Debug.Log ("onSetMessageReceivedStatusSuccess : " + status);
			if (onSetMessageReceivedStatusSuccessEvent != null) {
				onSetMessageReceivedStatusSuccessEvent (bool.Parse (status));
			}
		}


		public void onSetMessageSentStatusSuccess (string status)
		{
			Debug.Log ("onSetMessageSentStatusSuccess : " + status);
			if (onSetMessageSentStatusSuccessEvent != null) {
				onSetMessageSentStatusSuccessEvent (bool.Parse (status));
			}
		}




		public void onSetMessageReceivedStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onSetMessageReceivedStatusFailed : " + val);
			if (onSetMessageReceivedStatusFailedEvent != null) {
				onSetMessageReceivedStatusFailedEvent (val);
			}
		}


		public void onSetMessageSentStatusFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onSetMessageSentStatusFailed : " + val);
			if (onSetMessageSentStatusFailedEvent != null) {
				onSetMessageSentStatusFailedEvent (val);
			}
		}

		public static event Action onSetNotificationQuietHoursSuccessEvent;
		public static event Action<RCErrorCode> onSetNotificationQuietHoursFailedEvent;




		public void onSetNotificationQuietHoursSuccess (string empty)
		{
			Debug.Log ("onSetNotificationQuietHoursSuccess");
			if (onSetNotificationQuietHoursSuccessEvent != null) {
				onSetNotificationQuietHoursSuccessEvent ();
			}
		}


		public void onSetNotificationQuietHoursFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onSetNotificationQuietHoursFailed : " + val);
			if (onSetNotificationQuietHoursFailedEvent != null) {
				onSetNotificationQuietHoursFailedEvent (val);
			}
		}


		public static event Action<bool> onClearMessagesSuccessEvent;
		public static event Action<RCErrorCode> onClearMessagesFailedEvent;

		public void onClearMessagesSuccess (string status)
		{
			Debug.Log ("onClearMessagesSuccess : " + status);
			if (onClearMessagesSuccessEvent != null) {
				onClearMessagesSuccessEvent (bool.Parse (status));
			}
		}

		public void onClearMessagesFailed (string errorCode)
		{
			var val = (RCErrorCode)int.Parse (errorCode);
			Debug.Log ("onClearMessagesFailed : " + val);
			if (onClearMessagesFailedEvent != null) {
				onClearMessagesFailedEvent (val);
			}
		}

		#endregion

	}
}