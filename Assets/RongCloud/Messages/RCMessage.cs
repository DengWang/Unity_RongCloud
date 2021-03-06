﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RongCloud
{
	public class RCMessage
	{

		public RCConversationType conversationType {
			get;
			set;
		}

		public string targetId {
			get;
			set;
		}

		public long messageId {
			get;
			set;
		}

		public RCMessageDirection messageDirection {
			get;
			set;
		}

		public string senderUserId {
			get;
			set;
		}


		public RCReceivedStatus receivedStatus {
			get;
			set;
		}

		public RCSentStatus sentStatus {
			get;
			set;
		}


		public long receivedTime {
			get;
			set;
		}

		public long sentTime {
			get;
			set;
		}

		public string objectName {
			get;
			set;
		}

		public RCMessageContent content {
			get;
			set;
		}

		public string extra {
			get;
			set;
		}

		public string   messageUId {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[RCMessage: conversationType={0}, targetId={1}, messageId={2}, messageDirection={3}, senderUserId={4}, receivedStatus={5}, sentStatus={6}, receivedTime={7}, sentTime={8}, objectName={9}, content={10}, extra={11}]", conversationType, targetId, messageId, messageDirection, senderUserId, receivedStatus, sentStatus, receivedTime, sentTime, objectName, content, extra);
		}

		public static RCMessage DecodeFromJson (string json)
		{
			Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
			return DecodeFromJson (dict);
		}

		public static RCMessage DecodeFromJson (Dictionary<string,object> dict)
		{
			if (dict != null) {
				RCMessage message = new RCMessage (
					                                dict ["conversationType"].ToString (),
					                                dict ["targetId"].ToString (),
					                                dict ["messageId"].ToString (),
					                                dict ["messageDirection"].ToString (),
					                                dict ["senderUserId"].ToString (),
					                                dict ["receivedStatus"].ToString (),
					                                dict ["sentStatus"].ToString (),
					                                dict ["receivedTime"].ToString (),
					                                dict ["sentTime"].ToString (),
					                                dict ["objectName"].ToString (),
					                                dict ["content"],
					                                dict ["messageUId"].ToString ()
				                                );
				return message;
			} else {
				Debug.LogError ("dict is null");
				return null;
			}
		}


		RCMessage (string conversationType, string targetId, string  messageId, string messageDirection, string senderUserId, string receivedStatus, string sentStatus, string receivedTime, string sentTime, string objectName, object content, string messageUId)
		{
			this.conversationType = (RCConversationType)int.Parse (conversationType);
			this.targetId = targetId;
			this.messageId = long.Parse (messageId);
			this.messageDirection = (RCMessageDirection)int.Parse (messageDirection);
			this.senderUserId = senderUserId;
			this.receivedStatus = (RCReceivedStatus)int.Parse (receivedStatus);
			this.sentStatus = (RCSentStatus)int.Parse (sentStatus);
			this.receivedTime = long.Parse (receivedTime);
			this.sentTime = long.Parse (sentTime);
			this.objectName = objectName;
			this.messageUId = messageUId;
			Dictionary<string,object> dict = content as Dictionary<string,object>;
			if (dict != null) {
				switch (this.objectName) {
				case "RC:TxtMsg":
					this.content = new RCTextMessage (dict ["content"].ToString (), dict ["extra"].ToString ());
					break;
				case "RC:InfoNtf":
					this.content = new RCInformationNotificationMessage (dict ["message"].ToString (), dict ["extra"].ToString ());
					break;
				case "RC:CmdNtf":
					this.content = new RCCommandNotificationMessage (dict ["name"].ToString (), dict ["data"].ToString ());
					break;
				case "VOLV:GroupOp":
					this.content = new GroupOperationMessage (dict ["operatorUserId"].ToString (), dict ["operation"].ToString (), dict ["data"].ToString (), dict ["message"].ToString (), dict ["extra"].ToString ());
					break;
				case "VOLV:GroupReq":
					this.content = new GroupRequestMessage (dict ["operatorUserId"].ToString (), dict ["operatorUserAlias"].ToString (), dict ["data"].ToString (), dict ["message"].ToString (), dict ["extra"].ToString ());
					break;
				default:
					this.content = null;
					break;
				}
			} else {
				this.content = null;
			}
		}
	}

}