using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RongCloud
{
	public class MessagesPool
	{

		public static Dictionary<long,RCMessage> MessageSendPool = new Dictionary<long, RCMessage> ();
		public static Dictionary<long,RCMessage> MessageReceivePool = new Dictionary<long, RCMessage>();
	}

}