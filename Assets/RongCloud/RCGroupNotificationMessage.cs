using UnityEngine;
using System.Collections;

namespace RongCloud
{

	public class RCGroupNotificationMessage : RCMessageContent
	{

		public string operatorUserId {
			get;
			set;
		}

		public string operation {
			get;
			set;
		}

		public string data {
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