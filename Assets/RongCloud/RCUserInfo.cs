using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RongCloud
{
	public class RCUserInfo
	{

		public string userId {
			get;
			set;
		}

		public string name {
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
			dict.Add ("userId", this.userId);
			dict.Add ("name", this.name);
			dict.Add ("portraitUri", this.portraitUri);
			return MiniJSON.Json.Serialize (dict);
		}

		public static RCUserInfo DecodeFromJson (string json)
		{
			Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
			if (dict != null) {
				RCUserInfo userInfo = new RCUserInfo (dict ["userId"].ToString (), dict ["name"].ToString (), dict ["portraitUri"].ToString ());
				return userInfo;
			} else {
				Debug.LogError ("Deserialize error " + json);
				return null;
			}

		}

		public RCUserInfo (string userId, string name, string portraitUri)
		{
			this.userId = userId;
			this.name = name;
			this.portraitUri = portraitUri;
		}
	}
}