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


		public RCCommandNotificationMessage (string name, string data)
		{
			this.name = name;
			this.data = data;
		}
	}
}