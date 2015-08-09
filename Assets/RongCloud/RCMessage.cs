using UnityEngine;
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


		public static RCMessage DecodeFromJson (string json)
		{
			Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
			if (dict != null) {
				RCMessage message = new RCMessage ();
//				userInfo.userId = dict ["userId"].ToString ();
//				userInfo.name = dict ["name"].ToString ();
//				userInfo.portraitUri = dict ["portraitUri"].ToString ();
				return message;
			} else {
				Debug.LogError ("Deserialize error " + json);
				return null;
			}
		}


	}

}