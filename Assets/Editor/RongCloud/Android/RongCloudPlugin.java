package com.volvapps.rongcloudplugin;


import io.rong.imlib.RongIMClient;
import io.rong.imlib.model.Conversation;
import io.rong.message.TextMessage;

public class RongCloudPlugin extends RongCloudPluginBase {

	public void _connectWithToken(String token) {
		RongIMClient.connect(token, new RongIMClient.ConnectCallback() {
			@Override
			public void onSuccess(String userId) {
				/* 连接成功 */
				UnitySendMessage("onConnectSuccess",userId);
				RongCloudEvent.getInstance().setOtherListener();
			}

			@Override
			public void onError(RongIMClient.ErrorCode e) {
				/* 连接失败，注意并不需要您做重连 */
				UnitySendMessage("onConnectFailed", String.valueOf(e.getValue()));
			}
			@Override
			public void onTokenIncorrect() {
				/*
				 * Token 错误，在线上环境下主要是因为 Token 已经过期，您需要向 App Server 重新请求一个新的
				 * Token
				 */
				UnitySendMessage("onTokenIncorrect","");
			}
		});
	}
	
	
	public void _disconnect(){
		RongIMClient.getInstance().disconnect();
	}
	
	
	public void _logout(){
		RongIMClient.getInstance().logout();
	}
	
	public void _sendTextMessage(String targetId,String content, String extra, String pushContent ,String pushData){
		TextMessage textMessage = TextMessage.obtain(content);
		textMessage.setExtra(extra);
		RongIMClient.getInstance().sendMessage(Conversation.ConversationType.PRIVATE, targetId, textMessage, pushContent, pushData,new RongIMClient.SendMessageCallback() {
		    @Override
		    public void onError(Integer messageId, RongIMClient.ErrorCode e) {

		    }

		    @Override
		    public void onSuccess(Integer integer) {

		    }});
	
	}

}
