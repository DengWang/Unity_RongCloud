package com.volvapps.rongcloudplugin;

import java.util.Arrays;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.volvapps.message.*;

import android.util.Log;
import io.rong.imlib.RongIMClient;
import io.rong.imlib.RongIMClient.BlacklistStatus;
import io.rong.imlib.RongIMClient.ErrorCode;
import io.rong.imlib.RongIMClient.OperationCallback;
import io.rong.imlib.model.Conversation;
import io.rong.imlib.model.Conversation.ConversationNotificationStatus;
import io.rong.imlib.model.Message;
import io.rong.imlib.model.MessageContent;
import io.rong.message.CommandNotificationMessage;
import io.rong.message.TextMessage;

public class RongCloudPlugin extends RongCloudPluginBase {

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

	public void _addToBlacklist(String userId) {
		RongIMClient.getInstance().addToBlacklist(userId,
				new OperationCallback() {

					@Override
					public void onSuccess() {
						UnitySendMessage("onAddToBlacklistSuccess", "");
					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onAddToBlacklistFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _clearMessages(int conversationType, String targetId) {
		RongIMClient.getInstance().clearMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						UnitySendMessage("onClearMessagesSuccess",
								String.valueOf(paramT));

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onClearMessagesFailed",
								String.valueOf(paramErrorCode.getValue()));

					}

				});

	}

	public void _clearMessagesUnreadStatus(int conversationType, String targetId) {
		RongIMClient.getInstance().clearMessagesUnreadStatus(
				Conversation.ConversationType.setValue(conversationType),
				targetId, new RongIMClient.ResultCallback<Boolean>() {
					@Override
					public void onSuccess(Boolean paramT) {
						UnitySendMessage("onClearMessagesUnreadStatusSuccess",
								String.valueOf(paramT));
					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onClearMessagesUnreadStatusFailed",
								String.valueOf(paramErrorCode.getValue()));
					}

				});
	}

	public void _clearNotifications() {
		RongIMClient.clearNotifications();
	}

	public void _deleteMessages(String messageIdsJson) {

		try {
			JSONArray jsonArray = new JSONArray(messageIdsJson);
			int[] messageIds = new int[jsonArray.length()];
			for (int i = 0; i < jsonArray.length(); ++i) {
				messageIds[i] = jsonArray.getInt(i);
			}
			RongIMClient.getInstance().deleteMessages(messageIds,
					new RongIMClient.ResultCallback<Boolean>() {
						@Override
						public void onSuccess(Boolean paramT) {
							UnitySendMessage("onDeleteMessagesSuccess",
									String.valueOf(paramT));
						}

						@Override
						public void onError(ErrorCode paramErrorCode) {
							UnitySendMessage("onDeleteMessagesFailed",
									String.valueOf(paramErrorCode.getValue()));
						}
					});
		} catch (JSONException e) {
			Log.i(App.TAG, "failed to parse MessageIdJSON: " + e.getMessage());

		}
	}

	public void _disconnect() {
		RongIMClient.getInstance().disconnect();
	}

	public void _logout() {
		RongIMClient.getInstance().logout();
	}

	public void _getBlacklist() {
		RongIMClient.getInstance().getBlacklist(
				new RongIMClient.GetBlacklistCallback() {

					@Override
					public void onSuccess(String[] paramT) {
						UnitySendMessage("onGetBlacklistSuccess",
								new JSONArray(Arrays.asList(paramT)).toString());

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onGetBlacklistFailed",
								String.valueOf(paramErrorCode.getValue()));

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
								UnitySendMessage("onGetBlacklistStatusSuccess",
										String.valueOf(paramT.getValue()));

							}

							@Override
							public void onError(ErrorCode paramErrorCode) {
								UnitySendMessage("onGetBlacklistStatusFailed",
										String.valueOf(paramErrorCode
												.getValue()));

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
								UnitySendMessage(
										"onGetConversationNotificationStatusSuccess",
										String.valueOf(paramT.getValue()));

							}

							@Override
							public void onError(ErrorCode paramErrorCode) {
								UnitySendMessage(
										"onGetConversationNotificationStatusFailed",
										String.valueOf(paramErrorCode
												.getValue()));

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

	public void _getLatestMessages(int conversationType, String targetId,
			int count) {

		RongIMClient.getInstance().getLatestMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, count,
				new RongIMClient.ResultCallback<List<Message>>() {

					@Override
					public void onSuccess(List<Message> messages) {
						UnitySendMessage("onGetLatestMessagesSuccess",
								JsonHelper.MessagesToJSON(messages));

					}
					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onGetLatestMessagesFailed",
								String.valueOf(paramErrorCode.getValue()));
					}

				});

	}

	public void _getNotificationQuietHours() {
		RongIMClient.getInstance().getNotificationQuietHours(
				new RongIMClient.GetNotificationQuietHoursCallback() {

					@Override
					public void onSuccess(String paramString, int paramInt) {
						HashMap<String, Object> map = new HashMap<String, Object>();
						map.put("startTime", paramString);
						map.put("spanMinutes", paramInt);
						UnitySendMessage("onGetNotificationQuietHoursSuccess",
								new JSONObject(map).toString());
					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onGetNotificationQuietHoursFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _getRemoteHistoryMessages(int conversationType,
			String targetId, long dataTime, int count) {
		RongIMClient.getInstance().getRemoteHistoryMessages(
				Conversation.ConversationType.setValue(conversationType),
				targetId, dataTime, count,
				new RongIMClient.ResultCallback<List<Message>>() {

					@Override
					public void onSuccess(List<Message> messages) {
						UnitySendMessage("onGetRemoteHistoryMessagesSuccess",
								JsonHelper.MessagesToJSON(messages));

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onGetRemoteHistoryMessagesFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _joinChatRoom(String chatRoomId, int count) {
		RongIMClient.getInstance().joinChatRoom(chatRoomId, count,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						UnitySendMessage("onJoinChatRoomSuccess", "");

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onJoinChatRoomFailed",
								String.valueOf(paramErrorCode.getValue()));

					}

				});
	}

	public void _quitChatRoom(String chatRoomId) {
		RongIMClient.getInstance().quitChatRoom(chatRoomId,
				new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						UnitySendMessage("onQuitChatRoomSuccess", "");

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onQuitChatRoomFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _reconnect() {
		RongIMClient.getInstance().reconnect(
				new RongIMClient.ConnectCallback() {
					@Override
					public void onSuccess(String userId) {
						UnitySendMessage("onReConnectSuccess", userId);

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onReConnectFailed",
								String.valueOf(paramErrorCode.getValue()));

					}

					@Override
					public void onTokenIncorrect() {
						UnitySendMessage("onReTokenIncorrect", "");

					}
				});
	}

	public void _removeFromBlacklist(String userId) {
		RongIMClient.getInstance().removeFromBlacklist(userId,
				new OperationCallback() {
					@Override
					public void onSuccess() {
						UnitySendMessage("onRemoveFromBlacklistSuccess", "");

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onRemoveFromBlacklistFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _removeNotificationQuietHours() {
		RongIMClient.getInstance().removeNotificationQuietHours(
				new RongIMClient.OperationCallback() {
					@Override
					public void onSuccess() {
						UnitySendMessage(
								"onRemoveNotificationQuietHoursSuccess", "");
					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage(
								"onRemoveNotificationQuietHoursFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}

	public void _sendTextMessage(int conversationType, String targetId,
			String content, String extra, String pushContent, String pushData) {
		TextMessage textMessage = TextMessage.obtain(content);
		textMessage.setExtra(extra);
		Send(conversationType, targetId, textMessage, pushContent, pushData);

	}

	public void _sendCmdMessage(int conversationType, String targetId,
			String cmdName, String data, String pushContent, String pushData) {
		CommandNotificationMessage cmdMessage = CommandNotificationMessage
				.obtain(cmdName, data);
		Send(conversationType, targetId, cmdMessage, pushContent, pushData);
	}

	public void _sendOperationMessage(int conversationType, String targetId,
			String operatorUserId, String operation, String data,
			String message, String extra, String pushContent, String pushData) {
		GroupOperationMessage groupOperationMessage = GroupOperationMessage
				.obtain(operatorUserId, operation, data, message);
		groupOperationMessage.setExtra(extra);
		Send(conversationType, targetId, groupOperationMessage, pushContent,
				pushData);
	}

	public void _sendRequestMessage(int conversationType, String targetId,
			String operatorUserId, String operatorUserAlias, String data,
			String message, String extra, String pushContent, String pushData) {
		GroupRequestMessage groupRequestMessage = GroupRequestMessage.obtain(
				operatorUserId, operatorUserAlias, data, message);
		groupRequestMessage.setExtra(extra);
		Send(conversationType, targetId, groupRequestMessage, pushContent,
				pushData);
	}

	private void Send(int conversationType, String targetId,
			MessageContent content, String pushContent, String pushData) {
		RongIMClient.getInstance().sendMessage(
				Conversation.ConversationType.setValue(conversationType),
				targetId, content, pushContent, pushData,
				new RongIMClient.SendMessageCallback() {
					@Override
					public void onError(Integer messageId,
							RongIMClient.ErrorCode e) {
						HashMap<String, Object> map = new HashMap<String, Object>();
						map.put("messageId", messageId);
						map.put("errorCode", e.getValue());
						JSONObject jsonObject = new JSONObject(map);
						UnitySendMessage("onSendMessageFailed",
								jsonObject.toString());
					}

					@Override
					public void onSuccess(Integer integer) {
						UnitySendMessage("onSendMessageSuccess",
								integer.toString());

					}
				}, new RongIMClient.ResultCallback<Message>() {
					@Override
					public void onError(ErrorCode e) {
						// UnitySendMessage("onSendTextMessageResultFailed",
						// String.valueOf(e.getValue()));
					}

					@Override
					public void onSuccess(Message message) {
						UnitySendMessage("onSendMessageResult",
								JsonHelper.MessagetoJSON(message));
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
							public void onError(ErrorCode e) {
								UnitySendMessage(
										"onSetConversationNotificationStatusFailed",
										String.valueOf(e.getValue()));

							}

							@Override
							public void onSuccess(
									ConversationNotificationStatus status) {
								UnitySendMessage(
										"onSetConversationNotificationStatusSuccess",
										String.valueOf(status.getValue()));

							}
						});
	}

	public void _setMessageReceivedStatus(int messageId, int receivedStatus) {
		RongIMClient.getInstance().setMessageReceivedStatus(messageId,
				new Message.ReceivedStatus(receivedStatus),
				new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onError(ErrorCode e) {
						UnitySendMessage("onSetMessageReceivedStatusFailed",
								String.valueOf(e.getValue()));

					}

					@Override
					public void onSuccess(Boolean status) {
						UnitySendMessage("onSetMessageReceivedStatusSuccess",
								String.valueOf(status));

					}

				});
	}

	public void _setMessageSentStatus(int messageId, int sentStatus) {
		RongIMClient.getInstance().setMessageSentStatus(messageId,
				Message.SentStatus.setValue(sentStatus),
				new RongIMClient.ResultCallback<Boolean>() {

					@Override
					public void onSuccess(Boolean paramT) {
						UnitySendMessage("onSetMessageSentStatusSuccess",
								String.valueOf(paramT));

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onSetMessageSentStatusFailed",
								String.valueOf(paramErrorCode.getValue()));

					}

				});
	}

	public void _setNotificationQuietHours(String startTime, int spanMinutes) {
		RongIMClient.getInstance().setNotificationQuietHours(startTime,
				spanMinutes, new RongIMClient.OperationCallback() {

					@Override
					public void onSuccess() {
						UnitySendMessage("onSetNotificationQuietHoursSuccess",
								"");

					}

					@Override
					public void onError(ErrorCode paramErrorCode) {
						UnitySendMessage("onSetNotificationQuietHoursFailed",
								String.valueOf(paramErrorCode.getValue()));

					}
				});
	}
}
