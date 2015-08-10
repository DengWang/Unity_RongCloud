using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RCDiscussionNotificationMessage : RCMessageContent
	{

		public RCDiscussionNotificationType type {
			get;
			set;
		}

		public string operatorId {
			get;
			set;
		}

		public string extension {
			get;
			set;
		}
	}
}