package com.volvapps.message;
import android.os.Parcel;
import android.text.TextUtils;

import io.rong.common.ParcelUtils;

import io.rong.imlib.MessageTag;
import io.rong.imlib.model.MessageContent;
import io.rong.common.RLog;

import java.io.UnsupportedEncodingException;

import org.json.JSONException;
import org.json.JSONObject;

@MessageTag(value = "VOLV:GroupReq", flag = MessageTag.ISCOUNTED
| MessageTag.ISPERSISTED)
public class GroupRequestMessage extends MessageContent {
	
	private String operatorUserId;
	private String operatorUserAlias;
	private String data;
	private String message;
	private String extra;
	
	
	/**
	 * 读取接口，目的是要从Parcel中构造一个实现了Parcelable的类的实例处理。
	 */
	public static final Creator<GroupRequestMessage> CREATOR = new Creator<GroupRequestMessage>() {

		@Override
		public GroupRequestMessage createFromParcel(Parcel source) {
			return new GroupRequestMessage(source);
		}

		@Override
		public GroupRequestMessage[] newArray(int size) {
			return new GroupRequestMessage[size];
		}
	};

	public int describeContents() {
		return 0;
	}
	
	
	public void writeToParcel(Parcel dest, int flags) {
		ParcelUtils.writeToParcel(dest, this.operatorUserId);
		ParcelUtils.writeToParcel(dest, this.operatorUserAlias);
		ParcelUtils.writeToParcel(dest, this.data);
		ParcelUtils.writeToParcel(dest, this.message);
		ParcelUtils.writeToParcel(dest, this.extra);
	}
	
	
	public String getOperatorUserId() {
		return this.operatorUserId;
	}

	public void setOperatorUserId(String operatorUserId) {
		this.operatorUserId = operatorUserId;
	}

	public String getOperatorUserAlias() {
		return this.operatorUserAlias;
	}

	public void setOperatorUserAlias(String operatorUserAlias) {
		this.operatorUserAlias = operatorUserAlias;
	}

	public String getData() {
		return this.data;
	}

	public void setData(String data) {
		this.data = data;
	}

	public String getMessage() {
		return this.message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public String getExtra() {
		return this.extra;
	}

	public void setExtra(String extra) {
		this.extra = extra;
	}
	
	
	
	public GroupRequestMessage(Parcel in) {
		this.operatorUserId = ParcelUtils.readFromParcel(in);
		this.operatorUserAlias = ParcelUtils.readFromParcel(in);
		this.data = ParcelUtils.readFromParcel(in);
		this.message = ParcelUtils.readFromParcel(in);
		this.extra = ParcelUtils.readFromParcel(in);
	}

	public static GroupRequestMessage obtain(String operatorUserId,
			String operatorUserAlias, String data, String message) {
		GroupRequestMessage obj = new GroupRequestMessage();
		obj.operatorUserId = operatorUserId;
		obj.operatorUserAlias = operatorUserAlias;
		obj.data = data;
		obj.message = message;
		return obj;
	}

	private GroupRequestMessage() {
	}

	public byte[] encode() {
		JSONObject jsonObj = new JSONObject();
		try {
			jsonObj.put("operatorUserId", this.operatorUserId);
			jsonObj.put("operatorUserAlias", this.operatorUserAlias);

			if (!(TextUtils.isEmpty(this.data))) {
				jsonObj.put("data", this.data);
			}
			if (!(TextUtils.isEmpty(this.message))) {
				jsonObj.put("message", this.message);
			}
			if (!(TextUtils.isEmpty(getExtra())))
				jsonObj.put("extra", getExtra());
		} catch (JSONException e) {
			RLog.e(this, "JSONException", e.getMessage());
		}
		try {
			return jsonObj.toString().getBytes("UTF-8");
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return null;
	}

	public GroupRequestMessage(byte[] data) {
		String jsonStr = null;
		try {
			jsonStr = new String(data, "UTF-8");
		} catch (UnsupportedEncodingException e1) {
		}
		try {
			JSONObject jsonObj = new JSONObject(jsonStr);
			setOperatorUserId(jsonObj.optString("operatorUserId"));
			setOperatorUserAlias(jsonObj.optString("operatorUserAlias"));
			setData(jsonObj.optString("data"));
			setMessage(jsonObj.optString("message"));
			setExtra(jsonObj.optString("extra"));
		} catch (JSONException e) {
			RLog.e(this, "JSONException", e.getMessage());
		}
	}
}












