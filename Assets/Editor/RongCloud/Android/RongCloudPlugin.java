package com.volvapps.rongcloudplugin;

import io.rong.imlib.RongIMClient;
import io.rong.imlib.RongIMClient.BlacklistStatus;
import io.rong.imlib.RongIMClient.ErrorCode;
import io.rong.imlib.model.Conversation;
import io.rong.imlib.model.Conversation.ConversationNotificationStatus;
import io.rong.imlib.model.Message;
import io.rong.imlib.model.UserData;
import io.rong.message.TextMessage;

public class RongCloudPlugin extends RongCloudPluginBase {

	
	
	public void _init(){
		RongIMClient.init(getActivity());
		RongCloudEvent.init(getActivity());
	}
	
	
	public void _connectWithToken(String token) {
		RongIMClient.connect(token, new RongIMClient.ConnectCallback() {
			@Override
			public void onSuccess(String userId) {
				/* 连接成功 */
				UnitySendMessage("onConnectSuccess", userId);
				RongCloudEvent.getInstance().setOtherListener();
			}

			@Override
			public void onError(RongIMClient.ErrorCode e) {
				/* 连接失败，注意并不需要您做重连 */
				UnitySendMessage("onConnectFailed",
						String.valueOf(e.getValue()));
			}

			@Override
			public void onTokenIncorrect() {
				/*
				 * Token 错误，在线上环境下主要是因为 Token 已经过期，您需要向 App Server 重新请求一个新的
				 * Token
				 */
				UnitySendMessage("onTokenIncorrect", "");
			}
		});
	}

	public void _disconnect() {
		RongIMClient.getInstance().disconnect();
	}

	public void _logout() {
		RongIMClient.getInstance().logout();
	}

	public void _sendTextMessage(int conversationType, String targetId,
			String content, String extra, String pushContent, String pushData) {
		TextMessage textMessage = TextMessage.obtain(content);
		textMessage.setExtra(extra);
		RongIMClient.getInstance().sendMessage(
				Conversation.ConversationType.setValue(conversationType),
				targetId, textMessage, pushContent, pushData,
				new RongIMClient.SendMessageCallback() {
					@Override
					public void onError(Integer messageId,
							RongIMClient.ErrorCode e) {
						UnitySendMessage("onSendTextMessageFailed", String.valueOf(e.getValue()));

					}

					@Override
					public void onSuccess(Integer integer) {
						UnitySendMessage("onSendTextMessageSuccess",integer.toString());

					}
				}, new RongIMClient.ResultCallback<Message>() {

					@Override
					public void onError(ErrorCode e) {
						// TODO Auto-generated method stub
						UnitySendMessage("onSendTextMessageResultFailed",String.valueOf(e.getValue()));

					}

					@Override
					public void onSuccess(Message message) {
						// TODO Auto-generated method stub
						UnitySendMessage("onSendTextMessageResultSuccess",JsonHelper.MessagetoJSON(message));
					}
				});

	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

	public int _getTotalUnreadCount() {
		return RongIMClient.getInstance().getTotalUnreadCount();
	}

	public int _getUnreadCount(int conversationType, String targetId) {
		return RongIMClient.getInstance().getUnreadCount(
				Conversation.ConversationType.setValue(conversationType),
				targetId);
	}

	public String _getHistoryMessages(int conversationType, String targetId,
			int oldestMessageId, int count) {
		RongIMClient.getInstance().getHistoryMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, oldestMessageId, count);
		return "";
	}

	public String _getLatestMessages(int conversationType, String targetId,
			int count) {
		RongIMClient.getInstance().getLatestMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, count);
		return "";
	}

	public void _deleteMessages(int[] messageIds) {
		RongIMClient.getInstance().deleteMessages(messageIds,
				new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _clearMessages(int conversationType, String targetId) {
		RongIMClient.getInstance().clearMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _clearMessagesUnreadStatus(int conversationType, String targetId) {
		RongIMClient.getInstance().clearMessagesUnreadStatus(
				Conversation.ConversationType.setValue(conversationType),
				targetId, new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _clearNotifications() {
		RongIMClient.clearNotifications();
	}

	public void _getBlacklist() {
		RongIMClient.getInstance().getBlacklist(
				new RongIMClient.GetBlacklistCallback() {

					@Override
					public void onSuccess(String[] paramT) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _getBlacklistStatus(String userId) {
		RongIMClient
				.getInstance()
				.getBlacklistStatus(
						userId,
						new RongIMClient.ResultCallback<RongIMClient.BlacklistStatus>() {

							@Override
							public void onSuccess(BlacklistStatus paramT) {
								// TODO Auto-generated method stub

							}

							@Override
							public void onError(ErrorCode paramErrorCode) {
								// TODO Auto-generated method stub

							}

						});
	}

	public void _getConversationNotificationStatus(int conversationType,
			String targetId) {
		RongIMClient
				.getInstance()
				.getConversationNotificationStatus(
						Conversation.ConversationType
								.setValue(conversationType),
						targetId,
						new RongIMClient.ResultCallback<Conversation.ConversationNotificationStatus>() {

							@Override
							public void onSuccess(
									ConversationNotificationStatus paramT) {
								// TODO Auto-generated method stub

							}

							@Override
							public void onError(ErrorCode paramErrorCode) {
								// TODO Auto-generated method stub

							}

						});
	}

	public int _getCurrentConnectionStatus() {
		return RongIMClient.getInstance().getCurrentConnectionStatus()
				.getValue();
	}

	public String _getCurrentUserId() {
		return RongIMClient.getInstance().getCurrentUserId();

	}

	public long _getDeltaTime() {
		return RongIMClient.getInstance().getDeltaTime();
	}

	public void _getNotificationQuietHours() {
		RongIMClient.getInstance().getNotificationQuietHours(
				new RongIMClient.GetNotificationQuietHoursCallback() {

					@Override
					public void onSuccess(String paramString, int paramInt) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _joinChatRoom(String chatRoomId, int count) {
		RongIMClient.getInstance().joinChatRoom(chatRoomId, count,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _joinGroup(String groupId, String groupName) {
		RongIMClient.getInstance().joinGroup(groupId, groupName,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _quitChatRoom(String chatRoomId) {
		RongIMClient.getInstance().quitChatRoom(chatRoomId,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _quitGroup(String groupId) {
		RongIMClient.getInstance().quitGroup(groupId,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _addToBlacklist(String userId) {
		RongIMClient.getInstance().addToBlacklist(userId,
				new RongIMClient.AddToBlackCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _removeFromBlacklist(String userId) {
		RongIMClient.getInstance().removeFromBlacklist(userId,
				new RongIMClient.RemoveFromBlacklistCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _removeNotificationQuietHours() {
		RongIMClient.getInstance().removeNotificationQuietHours(
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode arg0) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _setConversationNotificationStatus(int conversationType,
			String targetId, int notificationStatus) {
		RongIMClient
				.getInstance()
				.setConversationNotificationStatus(
						Conversation.ConversationType
								.setValue(conversationType),
						targetId,
						Conversation.ConversationNotificationStatus
								.setValue(notificationStatus),
						new RongIMClient.ResultCallback<Conversation.ConversationNotificationStatus>() {

							@Override
							public void onError(ErrorCode arg0) {
								// TODO Auto-generated method stub

							}

							@Override
							public void onSuccess(
									ConversationNotificationStatus arg0) {
								// TODO Auto-generated method stub

							}
						});
	}

	public void _setMessageReceivedStatus(int messageId, int receivedStatus) {
		RongIMClient.getInstance().setMessageReceivedStatus(messageId,
				new Message.ReceivedStatus(receivedStatus),
				new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onError(ErrorCode arg0) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onSuccess(Boolean arg0) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _setMessageSentStatus(int messageId, int sentStatus) {
		RongIMClient.getInstance().setMessageSentStatus(messageId,
				Message.SentStatus.setValue(sentStatus),
				new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}

				});
	}

	public void _setNotificationQuietHours(String startTime, int spanMinutes) {
		RongIMClient.getInstance().setNotificationQuietHours(startTime,
				spanMinutes, new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _syncGroup(String groupList) {
		RongIMClient.getInstance().syncGroup(null,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

	public void _syncUserData(UserData userData) {
		RongIMClient.getInstance().syncUserData(userData,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						// TODO Auto-generated method stub

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						// TODO Auto-generated method stub

					}
				});
	}

}
