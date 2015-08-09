//
//  RongCloudBinding.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/6.
//
//



#import "UnityAppController.h"
#import <RongIMLib/RongIMLib.h>
#import "RongCloudManager.h"

// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil




const char * _getCurrentUserInfo(){
    RCUserInfo *  userInfo = [RCIMClient sharedRCIMClient].currentUserInfo;
    NSString * json = [RongCloudManager jsonFromObject:@{
                                                         @"userId":userInfo.userId,
                                                         @"name":userInfo.name,
                                                         @"portraitUri":userInfo.portraitUri
                                                         }];
    return MakeStringCopy(json);
}

int _getSdkRunningMode(){
    return (int)[RCIMClient sharedRCIMClient].sdkRunningMode;
}



void _init(const char * appKey){
    [[RCIMClient sharedRCIMClient] init: GetStringParam(appKey)];
    
}

void _setDeviceToken(const char * deviceToken){
    [[RCIMClient sharedRCIMClient] setDeviceToken:GetStringParam(deviceToken)];
}

void _connectWithToken(const char * token){
    
    // 快速集成第二步，连接融云服务器
    [[RCIMClient sharedRCIMClient] connectWithToken:GetStringParam(token) success: ^(NSString *userId) {
        [[RCIMClient sharedRCIMClient] setReceiveMessageDelegate:[RongCloudManager sharedManager] object:nil];
        [[RCIMClient sharedRCIMClient] setRCConnectionStatusChangeDelegate:[RongCloudManager sharedManager]];
        UnitySendMessage( RONGCLOUDMANAGER, "onConnectSuccess", userId.UTF8String);
    }error:^(RCConnectErrorCode status) {
        // Connect 失败
        UnitySendMessage( RONGCLOUDMANAGER, "onConnectFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }tokenIncorrect:^() {
        UnitySendMessage( RONGCLOUDMANAGER, "onTokenIncorrect", "");
    }];
}


void _disconnect(BOOL isReceivePush){
    [[RCIMClient sharedRCIMClient] disconnect:isReceivePush];
}

void _disconnectWithoutParam() {
    [[RCIMClient sharedRCIMClient] disconnect];
}

void _logout(){
    [[RCIMClient sharedRCIMClient] logout];
}

void _sendTextMessage(int conversationType,const char * targetId, const char * content, const char * extra,const char * pushContent){
    RCTextMessage *rcTextMessage = [RCTextMessage messageWithContent:GetStringParam(content)];
    rcTextMessage.extra = GetStringParam(extra);
    [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:rcTextMessage pushContent:GetStringParam(pushContent)  success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendTextMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendTextMessageFailed", [NSString stringWithFormat:@"%i",nErrorCode].UTF8String);
    }];
}



void _sendTextMessageWithPushData(int conversationType,const char * targetId, const char * content, const char * extra,const char * pushContent, const char * pushData){
    RCTextMessage *rcTextMessage = [RCTextMessage messageWithContent:GetStringParam(content)];
    rcTextMessage.extra = GetStringParam(extra);
    [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:rcTextMessage pushContent:GetStringParam(pushContent) pushData:GetStringParam(pushData) success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendTextMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendTextMessageFailed", [NSString stringWithFormat:@"%i",nErrorCode].UTF8String);
    }];
}




void _getUserInfo(const char * userId){
    [[RCIMClient sharedRCIMClient] getUserInfo:GetStringParam(userId) success:^(RCUserInfo *userInfo) {
        NSString * json = [RongCloudManager jsonFromObject:@{
                                                             @"userId": userInfo.userId,
                                                             @"name":userInfo.name,
                                                             @"portraitUri":userInfo.portraitUri
                                                             }];
        
        UnitySendMessage( RONGCLOUDMANAGER, "onGetUserInfoSuccess", json.UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetUserInfoFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


int _getTotalUnreadCount(){
    return [[RCIMClient sharedRCIMClient] getTotalUnreadCount ];
}

int _getUnreadCount(int conversationType,const char * targetId){
    return [[RCIMClient sharedRCIMClient] getUnreadCount:(RCConversationType)conversationType targetId:GetStringParam(targetId)];
}


int _getUnreadCountWithoutTargetId(const char * conversationTypesJson){
    NSArray * conversationTypes = [RongCloudManager objectFromJson:GetStringParam(conversationTypesJson)];
    [[RCIMClient sharedRCIMClient] getUnreadCount:conversationTypes];
}

const char * _getLatestMessages(int conversationType,const char * targetId, int count){
    NSArray * latestMessages = [[RCIMClient sharedRCIMClient] getLatestMessages:(RCConversationType)conversationType targetId:GetStringParam(targetId) count:count];
    NSMutableString *reString = [NSMutableString string];
    [reString appendString:@"["];
    NSMutableArray *values = [NSMutableArray array];
    for (RCMessage * message  in latestMessages) {
        if ( [message.content isMemberOfClass:[RCTextMessage class]]) {
            RCTextMessage *msg = (RCTextMessage *)message.content;
            NSString * json = [RongCloudManager jsonFromObject:@{
                                                                 @"conversationType": [NSString stringWithFormat:@"%i",message.conversationType],
                                                                 @"targetId":message.targetId,
                                                                 @"messageId":[NSString stringWithFormat:@"%li",message.messageId],
                                                                 @"messageDirection":[NSString stringWithFormat:@"%i",message.messageDirection],
                                                                 @"senderUserId":message.senderUserId,
                                                                 @"receivedStatus":[NSString stringWithFormat:@"%i",message.receivedStatus],
                                                                 @"sentStatus":[NSString stringWithFormat:@"%i",message.sentStatus],
                                                                 @"receivedTime":[NSString stringWithFormat:@"%lli",message.receivedTime],
                                                                 @"sentTime":[NSString stringWithFormat:@"%lli",message.sentTime],
                                                                 @"objectName":message.objectName,
                                                                 @"content":msg.content,
                                                                 @"extraContent": msg.extra == nil ? @"" : msg.extra,
                                                                 @"extraMessage":message.extra
                                                                 }];
            [values addObject:[NSString stringWithFormat:@"%@",json]];
        }
    }
    [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
    [reString appendString:@"]"];
    return MakeStringCopy(reString);
    
}


const char * _getHistoryMessages(int conversationType,const char * targetId,long oldestMessageId, int count){
    NSArray * historyMessages = [[RCIMClient sharedRCIMClient] getHistoryMessages:(RCConversationType) conversationType targetId:GetStringParam(targetId) oldestMessageId:oldestMessageId count:count];
    
    NSMutableString *reString = [NSMutableString string];
    [reString appendString:@"["];
    NSMutableArray *values = [NSMutableArray array];
    for (RCMessage * message  in historyMessages) {
        if ( [message.content isMemberOfClass:[RCTextMessage class]]) {
            RCTextMessage *msg = (RCTextMessage *)message.content;
            NSString * json = [RongCloudManager jsonFromObject:@{
                                                                 @"conversationType": [NSString stringWithFormat:@"%i",message.conversationType],
                                                                 @"targetId":message.targetId,
                                                                 @"messageId":[NSString stringWithFormat:@"%li",message.messageId],
                                                                 @"messageDirection":[NSString stringWithFormat:@"%i",message.messageDirection],
                                                                 @"senderUserId":message.senderUserId,
                                                                 @"receivedStatus":[NSString stringWithFormat:@"%i",message.receivedStatus],
                                                                 @"sentStatus":[NSString stringWithFormat:@"%i",message.sentStatus],
                                                                 @"receivedTime":[NSString stringWithFormat:@"%lli",message.receivedTime],
                                                                 @"sentTime":[NSString stringWithFormat:@"%lli",message.sentTime],
                                                                 @"objectName":message.objectName,
                                                                 @"content":msg.content,
                                                                 @"extraContent": msg.extra == nil ? @"" : msg.extra,
                                                                 @"extraMessage":message.extra
                                                                 }];
            [values addObject:[NSString stringWithFormat:@"%@",json]];
        }
    }
    [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
    [reString appendString:@"]"];
    return MakeStringCopy(reString);
    
}

BOOL _deleteMessages(const char * messageIdsJson){
    NSArray * messageIds = [RongCloudManager objectFromJson:GetStringParam(messageIdsJson)];
    return [[RCIMClient sharedRCIMClient] deleteMessages:messageIds];
}

BOOL _clearMessages(int conversationType,const char * targetId){
    
    return  [[RCIMClient sharedRCIMClient] clearMessages:(RCConversationType)conversationType targetId:GetStringParam(targetId)];
}

BOOL _clearMessagesUnreadStatus(int conversationType,const char * targetId){
    
    return [[RCIMClient sharedRCIMClient] clearMessagesUnreadStatus:(RCConversationType)conversationType targetId:GetStringParam(targetId)];
}

BOOL _setMessageReceivedStatus(long messageId,int receivedStatus){
    return  [[RCIMClient sharedRCIMClient] setMessageReceivedStatus:messageId receivedStatus:(RCReceivedStatus)receivedStatus];
}

void _getConversationNotificationStatus(int conversationType,const char * targetId){
    [[RCIMClient sharedRCIMClient] getConversationNotificationStatus:(RCConversationType)conversationType targetId:GetStringParam(targetId) success:^(RCConversationNotificationStatus nStatus) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetConversationNotificationStatusSuccess", [NSString stringWithFormat:@"%i",nStatus].UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetConversationNotificationStatusFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _setConversationNotificationStatus(int conversationType,const char * targetId, BOOL isBlocked){
    [[RCIMClient sharedRCIMClient] setConversationNotificationStatus:(RCConversationType)conversationType targetId:GetStringParam(targetId) isBlocked:isBlocked success:^(RCConversationNotificationStatus nStatus) {
        UnitySendMessage( RONGCLOUDMANAGER, "onSetConversationNotificationStatusSuccess", [NSString stringWithFormat:@"%i",nStatus].UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onSetConversationNotificationStatusFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _syncGroups(const char * groupListJson){
    NSArray * groupList;
    [[RCIMClient sharedRCIMClient] syncGroups:groupList success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onSyncGroupsSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onSyncGroupsFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
    
}

void _joinGroup(const char * groupId,const char * groupName){
    [[RCIMClient sharedRCIMClient] joinGroup:GetStringParam(groupId) groupName:GetStringParam(groupName) success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onJoinGroupSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onJoinGroupFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


void _quitGroup(const char * groupId){
    [[RCIMClient sharedRCIMClient] quitGroup:GetStringParam(groupId) success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onQuitGroupSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onQuitGroupFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


int _getCurrentNetworkStatus(){
    return (int)[[RCIMClient sharedRCIMClient] getCurrentNetworkStatus];
}


void _addToBlacklist(const char * userId){
    [[RCIMClient sharedRCIMClient] addToBlacklist:GetStringParam(userId) success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onAddToBlacklistSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onAddToBlacklistFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _removeFromBlacklist(const char * userId){
    [[RCIMClient sharedRCIMClient] removeFromBlacklist:GetStringParam(userId) success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onRemoveFromBlacklistSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onRemoveFromBlacklistFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _getBlacklistStatus(const char * userId){
    [[RCIMClient sharedRCIMClient] getBlacklistStatus:GetStringParam(userId) success:^(int bizStatus) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetBlacklistStatusSuccess", [NSString stringWithFormat:@"%i",bizStatus].UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetBlacklistStatusFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _getBlacklist(){
    [[RCIMClient sharedRCIMClient] getBlacklist:^(NSArray *blockUserIds) {
        NSString * json = [RongCloudManager jsonFromObject:blockUserIds];
        UnitySendMessage( RONGCLOUDMANAGER, "onGetBlacklistSuccess", json.UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetBlacklistFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


int _getConnectionStatus(){
    return (int)[[RCIMClient sharedRCIMClient] getConnectionStatus];
}

void _getRemoteHistoryMessages(int conversationType,const char * targetId,long recordTime,int count){
    [[RCIMClient sharedRCIMClient] getRemoteHistoryMessages:(RCConversationType)conversationType targetId:GetStringParam(targetId) recordTime:recordTime count:count success:^(NSArray *messages) {
        NSMutableString *reString = [NSMutableString string];
        [reString appendString:@"["];
        NSMutableArray *values = [NSMutableArray array];
        for (RCMessage * message  in messages) {
            if ( [message.content isMemberOfClass:[RCTextMessage class]]) {
                RCTextMessage *msg = (RCTextMessage *)message.content;
                NSString * json = [RongCloudManager jsonFromObject:@{
                                                                     @"conversationType": [NSString stringWithFormat:@"%i",message.conversationType],
                                                                     @"targetId":message.targetId,
                                                                     @"messageId":[NSString stringWithFormat:@"%li",message.messageId],
                                                                     @"messageDirection":[NSString stringWithFormat:@"%i",message.messageDirection],
                                                                     @"senderUserId":message.senderUserId,
                                                                     @"receivedStatus":[NSString stringWithFormat:@"%i",message.receivedStatus],
                                                                     @"sentStatus":[NSString stringWithFormat:@"%i",message.sentStatus],
                                                                     @"receivedTime":[NSString stringWithFormat:@"%lli",message.receivedTime],
                                                                     @"sentTime":[NSString stringWithFormat:@"%lli",message.sentTime],
                                                                     @"objectName":message.objectName,
                                                                     @"content":msg.content,
                                                                     @"extraContent": msg.extra == nil ? @"" : msg.extra,
                                                                     @"extraMessage":message.extra
                                                                     }];
                [values addObject:[NSString stringWithFormat:@"%@",json]];
            }
        }
        [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
        [reString appendString:@"]"];
        
        UnitySendMessage( RONGCLOUDMANAGER, "onGetRemoteHistoryMessagesSuccess", reString.UTF8String);
    }];
}
