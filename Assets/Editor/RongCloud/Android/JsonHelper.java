package com.volvapps.rongcloudplugin;

import io.rong.imlib.model.Message;
import io.rong.imlib.model.MessageContent;
import io.rong.message.TextMessage;
import java.util.HashMap;
import java.util.Iterator;

import org.json.JSONException;
import org.json.JSONObject;

import android.util.Log;

public class JsonHelper {

	public final static String TAG = "Unity";

	public static HashMap<String, Object> fromJSON(String json) {
		if (json == null || json.length() == 0) {
			return null;
		}
		HashMap<String, Object> map = new HashMap<String, Object>();
		JSONObject jsonObject;
		try {
			jsonObject = new JSONObject(json);
		} catch (JSONException e) {
			Log.i(TAG, "failed to parse userInfoJSON: " + e.getMessage());
			return null;
		}

		Iterator<String> iter = jsonObject.keys();
		while (iter.hasNext()) {
			String key = (String) iter.next();
			try {
				String value = jsonObject.getString(key);
				map.put(key, value);
			} catch (JSONException e) {
				Log.i(TAG,
						"failed to extract userInfo paramter for key: " + key
								+ ", error: " + e.getMessage());
			}
		}
		return map;
	}
	
	
	public static String toJSON(HashMap<String,Object> map){
		return new JSONObject(map).toString();
	}
	
	
	
	public static String MessagetoJSON(Message message) {
		
		HashMap<String,Object> messageMap = new HashMap<String,Object>();
		messageMap.put("conversationType", message.getConversationType().getValue());
		messageMap.put("targetId", message.getTargetId());
		messageMap.put("messageId", message.getMessageId());
		messageMap.put("messageDirection", message.getMessageDirection().getValue());
		messageMap.put("senderUserId", message.getSenderUserId());
		messageMap.put("receivedStatus", message.getReceivedStatus().getFlag());
		messageMap.put("sentStatus", message.getSentStatus().getValue());
		
		messageMap.put("receivedTime", message.getReceivedTime());
		messageMap.put("sentTime", message.getSentTime());
		messageMap.put("objectName", message.getObjectName());
				
		
		MessageContent messageContent = message.getContent();
		HashMap<String,Object> contentMap = null;
		
		
		if (messageContent instanceof TextMessage) {// 文本消息
			TextMessage textMessage = (TextMessage) messageContent;
			contentMap = new HashMap<String,Object>();
			contentMap.put("content", textMessage.getContent());
			contentMap.put("extra", textMessage.getExtra());
			
		} 
		messageMap.put("content", contentMap == null ? "{}":contentMap);
		return toJSON(messageMap);
	}

}
