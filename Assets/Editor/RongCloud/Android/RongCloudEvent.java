package com.volvapps.rongcloudplugin;

import android.content.Context;
import android.os.Handler;
import android.util.Log;
import io.rong.imlib.RongIMClient;
import io.rong.imlib.model.Message;
import io.rong.imlib.model.MessageContent;
import io.rong.message.ContactNotificationMessage;
import io.rong.message.ImageMessage;
import io.rong.message.InformationNotificationMessage;
import io.rong.message.RichContentMessage;
import io.rong.message.TextMessage;
import io.rong.message.VoiceMessage;

/**
 * Created by zhjchen on 1/29/15.
 */

/**
 * 融云SDK事件监听处理。 把事件统一处理，开发者可直接复制到自己的项目中去使用。
 * <p/>
 * 该类包含的监听事件有： 1、消息接收器：OnReceiveMessageListener。
 * 2、发出消息接收器：OnSendMessageListener。 3、用户信息提供者：GetUserInfoProvider。
 * 4、好友信息提供者：GetFriendsProvider。 5、群组信息提供者：GetGroupInfoProvider。 蓉c
 * 7、连接状态监听器，以获取连接相关状态：ConnectionStatusListener。 8、地理位置提供者：LocationProvider。
 * 9、自定义 push 通知： OnReceivePushMessageListener。
 * 10、会话列表界面操作的监听器：ConversationListBehaviorListener。
 */
public final class RongCloudEvent implements
		RongIMClient.OnReceiveMessageListener,
		RongIMClient.ConnectionStatusListener
		{

	private static final String TAG = "Unity";

	private static RongCloudEvent mRongCloudInstance;

	private Context mContext;

	/**
	 * 初始化 RongCloud.
	 *
	 * @param context
	 *            上下文。
	 */
	public static void init(Context context) {

		if (mRongCloudInstance == null) {

			synchronized (RongCloudEvent.class) {

				if (mRongCloudInstance == null) {
					mRongCloudInstance = new RongCloudEvent(context);
				}
			}
		}
	}

	/**
	 * 构造方法。
	 *
	 * @param context
	 *            上下文。
	 */
	private RongCloudEvent(Context context) {
		mContext = context;
		initDefaultListener();
	}

	/**
	 * RongIM.init(this) 后直接可注册的Listener。
	 */
	private void initDefaultListener() {

	}

	/*
	 * 连接成功注册。 <p/> 在RongIM-connect-onSuccess后调用。
	 */
	public void setOtherListener() {
		RongIMClient.setOnReceiveMessageListener(this);// 设置消息接收监听器。
		RongIMClient.setConnectionStatusListener(this);// 设置连接状态监听器。

	}

	/**
	 * 获取RongCloud 实例。
	 *
	 * @return RongCloud。
	 */
	public static RongCloudEvent getInstance() {
		return mRongCloudInstance;
	}

	/**
	 * 接收消息的监听器：OnReceiveMessageListener 的回调方法，接收到消息后执行。
	 *
	 * @param message
	 *            接收到的消息的实体信息。
	 * @param left
	 *            剩余未拉取消息数目。
	 */
	@Override
	public boolean onReceived(Message message, int left) {
//		MessageContent messageContent = message.getContent();
//		
//		if (messageContent instanceof TextMessage) {// 文本消息
//			TextMessage textMessage = (TextMessage) messageContent;
//			Log.d(TAG, "onReceived-TextMessage:" + textMessage.getContent());
//		} else if (messageContent instanceof ImageMessage) {// 图片消息
//			ImageMessage imageMessage = (ImageMessage) messageContent;
//			Log.d(TAG, "onReceived-ImageMessage:" + imageMessage.getRemoteUri());
//		} else if (messageContent instanceof VoiceMessage) {// 语音消息
//			VoiceMessage voiceMessage = (VoiceMessage) messageContent;
//			Log.d(TAG, "onReceived-voiceMessage:"
//					+ voiceMessage.getUri().toString());
//		} else if (messageContent instanceof RichContentMessage) {// 图文消息
//			RichContentMessage richContentMessage = (RichContentMessage) messageContent;
//			Log.d(TAG,
//					"onReceived-RichContentMessage:"
//							+ richContentMessage.getContent());
//		} else if (messageContent instanceof InformationNotificationMessage) {// 小灰条消息
//			InformationNotificationMessage informationNotificationMessage = (InformationNotificationMessage) messageContent;
//			Log.d(TAG, "onReceived-informationNotificationMessage:"
//					+ informationNotificationMessage.getMessage());
//			
//		}  else if (messageContent instanceof ContactNotificationMessage) {// 好友添加消息
//			ContactNotificationMessage contactContentMessage = (ContactNotificationMessage) messageContent;
//			Log.d(TAG, "onReceived-ContactNotificationMessage:getExtra;"
//					+ contactContentMessage.getExtra());
//			Log.d(TAG, "onReceived-ContactNotificationMessage:+getmessage:"
//					+ contactContentMessage.getMessage().toString());
//		} else {
//			Log.d(TAG, "onReceived-其他消息，自己来判断处理");
//		}
		RongCloudPlugin.instance().UnitySendMessage("onReceived", JsonHelper.MessagetoJSON(message));
		return false;

	}

	/**
	 * 连接状态监听器，以获取连接相关状态:ConnectionStatusListener 的回调方法，网络状态变化时执行。
	 *
	 * @param status
	 *            网络状态。
	 */
	@Override
	public void onChanged(ConnectionStatus status) {
		Log.d(TAG, "onChanged:" + status);
		RongCloudPlugin.instance().UnitySendMessage("onConnectionStatusChanged", String.valueOf(status.getValue()));
	}
}
