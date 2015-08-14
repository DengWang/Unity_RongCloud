using UnityEngine;
using System.Collections;

namespace RongCloud
{
	public class RCTextMessage :  RCMessageContent
	{

		public string content {
			get;
			set;
		}

		public string extra {
			get;
			set;
		}


		public RCTextMessage(string content, string extra) {
			this.content = content;
			this.extra = extra;
		}
	}

}