package com.volvapps.message;

import android.os.Parcel;
import android.text.TextUtils;

import com.sea_monster.common.ParcelUtils;

import io.rong.imlib.MessageTag;
import io.rong.imlib.model.MessageContent;
import io.rong.message.utils.RLog;

import java.io.UnsupportedEncodingException;

import org.json.JSONException;
import org.json.JSONObject;

@MessageTag(value = "VOLV:CustomOp", flag = MessageTag.ISCOUNTED
		| MessageTag.ISPERSISTED)
public class CustomOperationMessage extends MessageContent {
	private String operatorUserId;
	private String operation;
	private String data;
	private String message;
	private String extra;

	/**
	 * 读取接口，目的是要从Parcel中构造一个实现了Parcelable的类的实例处理。
	 */
	public static final Creator<CustomOperationMessage> CREATOR = new Creator<CustomOperationMessage>() {

		@Override
		public CustomOperationMessage createFromParcel(Parcel source) {
			return new CustomOperationMessage(source);
		}

		@Override
		public CustomOperationMessage[] newArray(int size) {
			return new CustomOperationMessage[size];
		}
	};

	public int describeContents() {
		return 0;
	}

	public void writeToParcel(Parcel dest, int flags) {
		ParcelUtils.writeToParcel(dest, this.operatorUserId);
		ParcelUtils.writeToParcel(dest, this.operation);
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

	public String getOperation() {
		return this.operation;
	}

	public void setOperation(String operation) {
		this.operation = operation;
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

	public CustomOperationMessage(Parcel in) {
		this.operatorUserId = ParcelUtils.readFromParcel(in);
		this.operation = ParcelUtils.readFromParcel(in);
		this.data = ParcelUtils.readFromParcel(in);
		this.message = ParcelUtils.readFromParcel(in);
		this.extra = ParcelUtils.readFromParcel(in);
	}

	public static CustomOperationMessage obtain(String operatorUserId,
			String operation, String data, String message) {
		CustomOperationMessage obj = new CustomOperationMessage();
		obj.operatorUserId = operatorUserId;
		obj.operation = operation;
		obj.data = data;
		obj.message = message;
		return obj;
	}

	private CustomOperationMessage() {
	}

	public byte[] encode() {
		JSONObject jsonObj = new JSONObject();
		try {
			jsonObj.put("operatorUserId", this.operatorUserId);
			jsonObj.put("operation", this.operation);

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

	public CustomOperationMessage(byte[] data) {
		String jsonStr = null;
		try {
			jsonStr = new String(data, "UTF-8");
		} catch (UnsupportedEncodingException e1) {
		}
		try {
			JSONObject jsonObj = new JSONObject(jsonStr);
			setOperatorUserId(jsonObj.optString("operatorUserId"));
			setOperation(jsonObj.optString("operation"));
			setData(jsonObj.optString("data"));
			setMessage(jsonObj.optString("message"));
			setExtra(jsonObj.optString("extra"));
		} catch (JSONException e) {
			RLog.e(this, "JSONException", e.getMessage());
		}
	}

}
