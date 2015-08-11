using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace RongCloud
{
	public class RCUtils
	{

		public static List<RCMessage> PraseRCMessages (string json)
		{
			List<object> tempList = MiniJSON.Json.Deserialize (json) as List<object>;
			List<RCMessage> messages = new List<RCMessage> ();
			if (tempList != null) {
				foreach (var item in tempList) {
					var message = RCMessage.DecodeFromJson (item as Dictionary<string,object>);	
					messages.Add (message);
				}
			} 
			return messages;
		}
	}

}