namespace RongCloud
{
	


	public enum RCConnectErrorCode
	{
        RC_NET_NAVI_ERROR = 30000,
        RC_NET_CHANNEL_INVALID = 30001,
        RC_NET_UNAVAILABLE = 30002,
        RC_MSG_RESP_TIMEOUT = 30003,
        RC_HTTP_SEND_FAIL = 30004,
        RC_HTTP_REQ_TIMEOUT = 30005,
        RC_HTTP_RECV_FAIL = 30006,
        RC_NAVI_RESOURCE_ERROR = 30007,
        RC_NODE_NOT_FOUND = 30008,
        RC_DOMAIN_NOT_RESOLVE = 30009,
        RC_SOCKET_NOT_CREATED = 30010,
        RC_SOCKET_DISCONNECTED = 30011,
        RC_PING_SEND_FAIL = 30012,
        RC_PONG_RECV_FAIL = 30013,
        RC_MSG_SEND_FAIL = 30014,
        RC_CONN_ACK_TIMEOUT = 31000,
        RC_CONN_PROTO_VERSION_ERROR = 31001,
        RC_CONN_ID_REJECT = 31002,
        RC_CONN_SERVER_UNAVAILABLE = 31003,
        RC_CONN_TOKEN_INCORRECT = 31004,
        RC_CONN_NOT_AUTHRORIZED = 31005,
        RC_CONN_REDIRECTED = 31006,
        RC_CONN_PACKAGE_NAME_INVALID = 31007,
        RC_CONN_APP_BLOCKED_OR_DELETED = 31008,
        RC_CONN_USER_BLOCKED = 31009,
        RC_DISCONN_KICK = 31010,
        RC_QUERY_ACK_NO_DATA = 32001,
        RC_MSG_DATA_INCOMPLETE = 32002,
        RC_INVALID_ARGUMENT = -1000,
	}

    public enum RCChatRoomMemberOrder{
        RC_ChatRoom_Member_Asc = 1,
        RC_ChatRoom_Member_Desc = 2,
    }



	public enum RCConnectionStatus
	{
        ConnectionStatus_UNKNOWN = -1,
        ConnectionStatus_Connected = 0,
        ConnectionStatus_NETWORK_UNAVAILABLE = 1,
        ConnectionStatus_AIRPLANE_MODE = 2,
        ConnectionStatus_Cellular_2G = 3,
        ConnectionStatus_Cellular_3G_4G = 4,
        ConnectionStatus_WIFI = 5,
        ConnectionStatus_KICKED_OFFLINE_BY_OTHER_CLIENT = 6,
        ConnectionStatus_LOGIN_ON_WEB = 7,
        ConnectionStatus_SERVER_INVALID = 8,
        ConnectionStatus_VALIDATE_INVALID = 9,
        ConnectionStatus_Connecting = 10,
        ConnectionStatus_Unconnected = 11,
        ConnectionStatus_SignUp = 12,
        ConnectionStatus_TOKEN_INCORRECT = 31004,
        ConnectionStatus_DISCONN_EXCEPTION = 31011,
	}

	public enum RCConversationNotificationStatus
	{
        DO_NOT_DISTURB = 0,
        NOTIFY = 1,
	}

	public enum RCConversationType
	{
        ConversationType_PRIVATE = 1,
        ConversationType_DISCUSSION,
        ConversationType_GROUP,
        ConversationType_CHATROOM,
        ConversationType_CUSTOMERSERVICE,
        ConversationType_SYSTEM,
        ConversationType_APPSERVICE,
        ConversationType_PUBLICSERVICE,
        ConversationType_PUSHSERVICE,
	}

	public enum RCCurrentConnectionStatus
	{
        RC_DISCONNECTED = 9,
        RC_CONNECTED = 0,
        RC_CONNECTING = 2,
	}

	public enum RCDiscussionNotificationType
	{
        RCInviteDiscussionNotification = 1,
        RCQuitDiscussionNotification,
        RCRenameDiscussionTitleNotification,
        RCRemoveDiscussionMemberNotification,
        RCSwichInvitationAccessNotification,
	}

	public enum RCErrorCode
	{
        ERRORCODE_UNKNOWN = -1,
        REJECTED_BY_BLACKLIST = 405,
        ERRORCODE_TIMEOUT = 5004,
        SEND_MSG_FREQUENCY_OVERRUN = 20604,
        NOT_IN_DISCUSSION = 21406,
        NOT_IN_GROUP = 22406,
        FORBIDDEN_IN_GROUP = 22408,
        NOT_IN_CHATROOM = 23406,
        FORBIDDEN_IN_CHATROOM = 23408,
        KICKED_FROM_CHATROOM = 23409,
        RC_CHATROOM_NOT_EXIST = 23410,
        RC_CHATROOM_IS_FULL = 23411,
        CLIENT_NOT_INIT = 33001,
        DATABASE_ERROR = 33002,
        INVALID_PARAMETER = 33003,
        MSG_ROAMING_SERVICE_UNAVAILABLE = 33007,
	}

	public enum RCMediaType
	{
		MediaType_IMAGE = 1,
		MediaType_AUDIO,
		MediaType_VIDEO,
		MediaType_FILE = 100,
	}

	public enum RCMessageDirection
	{
		MessageDirection_SEND = 1,
		MessageDirection_RECEIVE,
	}

	public enum RCMessagePersistent
	{
        MessagePersistent_NONE = 0,
        MessagePersistent_ISPERSISTED = 1,
        MessagePersistent_ISCOUNTED = 3,
        MessagePersistent_STATUS = 16,
	}

	public enum RCNetworkStatus
	{
        RC_NotReachable = 0,
        RC_ReachableViaWiFi,
        RC_ReachableViaLTE,
        RC_ReachableVia3G,
        RC_ReachableVia2G,
	}


	public enum RCPublicServiceMenuItemType
	{
		RC_PUBLIC_SERVICE_MENU_ITEM_GROUP = 0,
		RC_PUBLIC_SERVICE_MENU_ITEM_VIEW = 1,
		RC_PUBLIC_SERVICE_MENU_ITEM_CLICK = 2,
	}

	public enum RCPublicServiceType
	{
		RC_APP_PUBLIC_SERVICE = 7,
		RC_PUBLIC_SERVICE = 8,
	}

	public enum RCReceivedStatus
	{
        ReceivedStatus_UNREAD = 0,
        ReceivedStatus_READ = 1,
        ReceivedStatus_LISTENED = 2,
        ReceivedStatus_DOWNLOADED = 4,
	}

	public enum RCSDKRunningMode
	{
		RCSDKRunningMode_Backgroud = 0,
		RCSDKRunningMode_Foregroud = 1,
	}

    public enum RCSearchType
	{
        RC_SEARCH_TYPE_EXACT = 0,
        RC_SEARCH_TYPE_FUZZY = 1,
	}

	public enum RCSentStatus
	{
        SentStatus_SENDING = 10,
        SentStatus_FAILED = 20,
        SentStatus_SENT = 30,
        SentStatus_RECEIVED = 40,
        SentStatus_READ = 50,
        SentStatus_DESTROYED = 60,
	}
}