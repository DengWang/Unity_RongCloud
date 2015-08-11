package com.volvapps.rongcloudplugin;

import android.util.Log;
import io.rong.imlib.RongIMClient;
import io.rong.imlib.model.Message;


public final class RongCloudEvent implements
		RongIMClient.OnReceiveMessageListener,
		RongIMClient.ConnectionStatusListener
		{

	private static final String TAG = "Unity";

	private static RongCloudEvent mRongCloudInstance;

	/**
	 * 初始化 RongCloud.
	 *
	 * @param context
	 *            上下文。
	 */
	public static void init() {

		if (mRongCloudInstance == null) {

			synchronized (RongCloudEvent.class) {

				if (mRongCloudInstance == null) {
					mRongCloudInstance = new RongCloudEvent();
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
	private RongCloudEvent() {
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
		Log.d(TAG, "onChanged:" + message);
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
