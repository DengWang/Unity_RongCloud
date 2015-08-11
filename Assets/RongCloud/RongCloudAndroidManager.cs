using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RongCloud
{
	public class RongCloudAndroidManager : MonoBehaviour
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

		public static event Action<string> onSendTextMessageSuccessEvent;
		public static event Action<RCErrorCode> onSendTextMessageFailedEvent;

		public void onSendTextMessageSuccess (string messageId)
		{
			Debug.Log ("onSendTextMessageSuccess : " + messageId);
			if (onSendTextMessageSuccessEvent != null) {
				onSendTextMessageSuccessEvent (messageId);
			}
		}

		public void onSendTextMessageFailed (string status)
		{
			var val = (RCErrorCode)int.Parse (status);
			Debug.Log ("onSendTextMessageFailed : " + val);
			if (onSendTextMessageFailedEvent != null) {
				onSendTextMessageFailedEvent (val);
			}
		}



		public static event Action<RCErrorCode> onSendTextMessageResultFailedEvent;
		public static event Action<RCMessage> onSendTextMessageResultSuccessEvent;

		public void onSendTextMessageResultFailed (string status)
		{
			var val = (RCErrorCode)int.Parse (status);
			Debug.Log ("onSendTextMessageResultFailed : " + val);
			if (onSendTextMessageResultFailedEvent != null) {
				onSendTextMessageResultFailedEvent (val);
			}
		}


		public void onSendTextMessageResultSuccess (string json)
		{
			Debug.Log ("onSendTextMessageResultSuccess : " + json);
			if (onSendTextMessageResultSuccessEvent != null) {
				onSendTextMessageResultSuccessEvent (RCMessage.DecodeFromJson (json));
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

				List<RCMessage> messages = null;

				onGetRemoteHistoryMessagesSuccessEvent (messages);
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


	}
}