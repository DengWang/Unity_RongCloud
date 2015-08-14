package com.volvapps.rongcloudplugin;

import io.rong.imlib.model.Group;
import io.rong.imlib.model.Message;
import io.rong.imlib.model.MessageContent;
import io.rong.message.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.volvapps.message.*;

import android.net.Uri;
import android.util.Log;

public class JsonHelper {

	public final static String TAG = "Unity";

	

	public static List<Group> GroupsFromJSON2(String json) {
		if (json == null || json.length() == 0) {
			return null;
		}

		JSONArray jsonArray;
		try {
			jsonArray = new JSONArray(json);
		} catch (JSONException e) {
			Log.i(App.TAG, "failed to parse GroupsJSON: " + e.getMessage());
			return null;
		}

		ArrayList<Group> groups = new ArrayList<Group>(jsonArray.length());

		for (int i = 0; i < jsonArray.length(); ++i) {
			try {
				JSONObject jsonObject = jsonArray.getJSONObject(i);
				Group group = new Group(jsonObject.getString("groupId"),
						jsonObject.getString("groupName"), Uri.parse(jsonObject
								.getString("portraitUri")));
				groups.add(group);
			} catch (JSONException e) {
				Log.i(App.TAG, "failed to parse GroupsJSON: " + e.getMessage());
				return null;
			}

		}
		return groups;
	}
	
	
	
	public static List<Group> GroupsFromJSON(String json) {
		if (json == null || json.length() == 0) {
			return null;
		}

		JSONArray jsonArray;
		try {
			jsonArray = new JSONArray(json);
		} catch (JSONException e) {
			Log.i(TAG, "failed to parse GroupsJSON: " + e.getMessage());
			return null;
		}

		ArrayList<Group> groups = new ArrayList<Group>(jsonArray.length());

		for (int i = 0; i < jsonArray.length(); ++i) {
			try {
				String groupString = jsonArray.getString(i);
				JSONObject jsonObject = new JSONObject(groupString);
				Group group = new Group(jsonObject.getString("groupId"),
						jsonObject.getString("groupName"), Uri.parse(jsonObject
								.getString("portraitUri")));
				groups.add(group);
			} catch (JSONException e) {
				Log.i(TAG, "failed to parse GroupsJSON: " + e.getMessage());
				return null;
			}

		}
		return groups;
	}
	

	public static String MessagetoJSON(Message message) {

		HashMap<String, Object> messageMap = new HashMap<String, Object>();
		messageMap.put("conversationType", message.getConversationType()
				.getValue());
		messageMap.put("targetId", message.getTargetId());
		messageMap.put("messageId", message.getMessageId());
		messageMap.put("messageDirection", message.getMessageDirection()
				.getValue());
		messageMap.put("senderUserId", message.getSenderUserId());
		messageMap.put("receivedStatus", message.getReceivedStatus().getFlag());
		messageMap.put("sentStatus", message.getSentStatus().getValue());

		messageMap.put("receivedTime", message.getReceivedTime());
		messageMap.put("sentTime", message.getSentTime());
		messageMap.put("objectName", message.getObjectName());

		MessageContent messageContent = message.getContent();
		HashMap<String, Object> contentMap = null;
		Log.i(App.TAG, message.getObjectName());
		if (messageContent instanceof TextMessage) {// 文本消息
			TextMessage textMessage = (TextMessage) messageContent;
			contentMap = new HashMap<String, Object>();
			contentMap.put("content", textMessage.getContent());
			contentMap.put("extra", textMessage.getExtra());
		}else if(messageContent instanceof InformationNotificationMessage){
			InformationNotificationMessage informationNotificationMessage = (InformationNotificationMessage)messageContent;
			contentMap = new HashMap<String, Object>();
			contentMap.put("message", informationNotificationMessage.getMessage());
			contentMap.put("extra", informationNotificationMessage.getExtra());
		}else if (messageContent instanceof CommandNotificationMessage){
			CommandNotificationMessage commandNotificationMessage = (CommandNotificationMessage)messageContent;
			contentMap = new HashMap<String, Object>();
			contentMap.put("name", commandNotificationMessage.getName());
			contentMap.put("data", commandNotificationMessage.getData());
		}else if (messageContent instanceof GroupOperationMessage){
			GroupOperationMessage groupOperationMessage = (GroupOperationMessage)messageContent;
			contentMap = new HashMap<String, Object>();
			contentMap.put("operatorUserId", groupOperationMessage.getOperatorUserId());
			contentMap.put("operation", groupOperationMessage.getOperation());
			contentMap.put("data", groupOperationMessage.getData());
			contentMap.put("message", groupOperationMessage.getMessage());
			contentMap.put("extra", groupOperationMessage.getExtra());
		}else if (messageContent instanceof GroupRequestMessage){
			GroupRequestMessage grouprequestMessage = (GroupRequestMessage)messageContent;
			contentMap = new HashMap<String, Object>();
			contentMap.put("operatorUserId", grouprequestMessage.getOperatorUserId());
			contentMap.put("operatorUserAlias", grouprequestMessage.getOperatorUserAlias());
			contentMap.put("data", grouprequestMessage.getData());
			contentMap.put("message", grouprequestMessage.getMessage());
			contentMap.put("extra", grouprequestMessage.getExtra());
		}
		messageMap.put("content", contentMap == null ? "{}" : contentMap);
		return new JSONObject(messageMap).toString();
	}

	public static String MessagesToJSON(List<Message> messages) {
		StringBuffer sb = new StringBuffer();
		sb.append("[");
		for (int i = 0; i < messages.size(); ++i) {
			if (i != 0) {
				sb.append(",");
			}
			sb.append(MessagetoJSON(messages.get(i)));
		}
		sb.append("]");
		return sb.toString();
	}

}
