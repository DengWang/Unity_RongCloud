using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RCInformationNotificationMessage : RCMessageContent
	{

		public string message {
			get;
			set;
		}

		public string extra {
			get;
			set;
		}


		public RCInformationNotificationMessage(string message, string extra) {
			this.message = message;
			this.extra = extra;
		}
	}

}