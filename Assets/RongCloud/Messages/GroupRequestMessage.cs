using UnityEngine;
using System.Collections;

namespace RongCloud
{

	public class GroupRequestMessage : RCMessageContent
	{

		public string operatorUserId {
			get;
			set;
		}

		public string operatorUserAlias {
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



		public GroupRequestMessage (string operatorUserId, string operatorUserAlias, string data, string message, string extra)
		{
			this.operatorUserId = operatorUserId;
			this.operatorUserAlias = operatorUserAlias;
			this.data = data;
			this.message = message;
			this.extra = extra;
				
		}
	}
}