using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RongCloud
{
	public class RCGroup
	{

		public string groupId {
			get;
			set;
		}

		public string groupName {
			get;
			set;
		}

		public string portraitUri {
			get;
			set;
		}

		public override string ToString ()
		{
			return EncodeToJson ();
		}

		public string EncodeToJson ()
		{
			Dictionary<string,string> dict = new Dictionary<string, string> ();
			dict.Add ("groupId", this.groupId);
			dict.Add ("groupName", this.groupName);
			dict.Add ("portraitUri", this.portraitUri);
			return MiniJSON.Json.Serialize (dict);
		}

		public static RCGroup DecodeFromJson (string json)
		{
			Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
			if (dict != null) {
				RCGroup group = new RCGroup (dict ["groupId"].ToString (), dict ["groupName"].ToString (), dict ["portraitUri"].ToString ());
				return group;
			} else {
				Debug.LogError ("Deserialize error " + json);
				return null;
			}
		}

		public RCGroup (string groupId, string groupName, string portraitUri)
		{
			this.groupId = groupId;
			this.groupName = groupName;
			this.portraitUri = portraitUri;
		}
	}
}