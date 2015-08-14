using UnityEngine;
using System.Collections;

namespace RongCloud
{

	public class GroupOperationMessage : RCMessageContent
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



		public GroupOperationMessage (string operatorUserId, string operation, string data, string message, string extra)
		{
			this.operatorUserId = operatorUserId;
			this.operation = operation;
			this.data = data;
			this.message = message;
			this.extra = extra;
				
		}
	}
}