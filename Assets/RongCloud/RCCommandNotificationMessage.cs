using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RCCommandNotificationMessage : RCMessageContent
	{

		public string name {
			get;
			set;
		}

		public string data {
			get;
			set;
		}
	}
}