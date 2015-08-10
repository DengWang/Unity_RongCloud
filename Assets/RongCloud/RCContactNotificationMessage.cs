using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RCContactNotificationMessage : RCMessageContent
	{

		public string operation {
			get;
			set;
		}

		public string sourceUserId {
			get;
			set;
		}

		public string targetUserId {
			get;
			set;
		}

		public string message {
			get;
			set;
		}


		public string extra {
			get;
			set;
		}
	}

}