//
//  RongCloudBinding.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/6.
//
//



#import "UnityAppController.h"
#import "GroupOperationMessage.h"
#import "GroupRequestMessage.h"
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
    [[RCIMClient sharedRCIMClient] initWithAppKey: GetStringParam(appKey)];
    [[RCIMClient sharedRCIMClient] registerMessageType: GroupOperationMessage.class];
    [[RCIMClient sharedRCIMClient] registerMessageType: GroupRequestMessage.class];
}

void _setDeviceToken(const char * deviceToken){
    [[RCIMClient sharedRCIMClient] setDeviceToken:GetStringParam(deviceToken)];
}

void _connectWithToken(const char * token){
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


void _disconnect(){
     [[RCIMClient sharedRCIMClient] disconnect];
}

void _logout(){
     [[RCIMClient sharedRCIMClient] logout];
}

void _sendTextMessage(int conversationType,const char * targetId, const char * content, const char * extra,const char * pushContent, const char * pushData){
    RCTextMessage *rcTextMessage = [RCTextMessage messageWithContent:GetStringParam(content)];
    rcTextMessage.extra = GetStringParam(extra);
    RCMessage * message  = [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:rcTextMessage pushContent:GetStringParam(pushContent) pushData:GetStringParam(pushData) success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        NSDictionary * dict = @{@"messageId": @(messageId),@"errorCode":@((int)nErrorCode)};
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageFailed", [RongCloudManager jsonFromObject:dict].UTF8String);
        
    }];
    UnitySendMessage(RONGCLOUDMANAGER, "onSendMessageResult", [RongCloudManager RCMessageToJson:message].UTF8String);
}


void _sendCmdMessage (int conversationType,const char * targetId, const char * name, const char * data,const char * pushContent, const char * pushData){
    RCCommandNotificationMessage * rcCmdMessage = [RCCommandNotificationMessage notificationWithName:GetStringParam(name) data:GetStringParam(data)];
    RCMessage * message  = [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:rcCmdMessage pushContent:GetStringParam(pushContent) pushData:GetStringParam(pushData) success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        NSDictionary * dict = @{@"messageId": @(messageId),@"errorCode":@((int)nErrorCode)};
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageFailed", [RongCloudManager jsonFromObject:dict].UTF8String);
        
    }];
    UnitySendMessage(RONGCLOUDMANAGER, "onSendMessageResult", [RongCloudManager RCMessageToJson:message].UTF8String);
    
}


void _sendOperationMessage(int conversationType,const char * targetId, const char * operatorUserId, const char * operation, const char * data, const char * messageStr ,const char * extra ,const char * pushContent, const char * pushData){
    GroupOperationMessage  * operationMessage = [GroupOperationMessage messageWithOperation:GetStringParam(operation) operatorUserId:GetStringParam(operatorUserId) data:GetStringParam(data) message:GetStringParam(messageStr) extra:GetStringParam(extra)];
    RCMessage * message  = [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:operationMessage pushContent:GetStringParam(pushContent) pushData:GetStringParam(pushData) success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        NSDictionary * dict = @{@"messageId": @(messageId),@"errorCode":@((int)nErrorCode)};
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageFailed", [RongCloudManager jsonFromObject:dict].UTF8String);
        
    }];
    UnitySendMessage(RONGCLOUDMANAGER, "onSendMessageResult", [RongCloudManager RCMessageToJson:message].UTF8String);
    
}


void _sendRequestMessage(int conversationType,const char * targetId, const char * operatorUserId, const char * operatorUserAlias, const char * data, const char * messageStr ,const char * extra ,const char * pushContent, const char * pushData){
    GroupRequestMessage  * requestMessage = [GroupRequestMessage messageWithOperation:GetStringParam(operatorUserId) operatorUserAlias:GetStringParam(operatorUserAlias) data:GetStringParam(data) message:GetStringParam(messageStr) extra:GetStringParam(extra)];
    RCMessage * message  = [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:requestMessage pushContent:GetStringParam(pushContent) pushData:GetStringParam(pushData) success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        NSDictionary * dict = @{@"messageId": @(messageId),@"errorCode":@((int)nErrorCode)};
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageFailed", [RongCloudManager jsonFromObject:dict].UTF8String);
        
    }];
    UnitySendMessage(RONGCLOUDMANAGER, "onSendMessageResult", [RongCloudManager RCMessageToJson:message].UTF8String);
    
}


int _getTotalUnreadCount(){
    return [[RCIMClient sharedRCIMClient] getTotalUnreadCount ];
}

int _getUnreadCount(int conversationType,const char * targetId){
    return [[RCIMClient sharedRCIMClient] getUnreadCount:(RCConversationType)conversationType targetId:GetStringParam(targetId)];
}


const char * _getLatestMessages(int conversationType,const char * targetId, int count){
    NSArray * latestMessages = [[RCIMClient sharedRCIMClient] getLatestMessages:(RCConversationType)conversationType targetId:GetStringParam(targetId) count:count];
    
    if (latestMessages == nil) {
        return "[]";
    }
    
    NSMutableString *reString = [NSMutableString string];
    [reString appendString:@"["];
    NSMutableArray *values = [NSMutableArray array];
    for (RCMessage * message  in latestMessages) {
        [values addObject:[NSString stringWithFormat:@"%@",[RongCloudManager RCMessageToJson:message]]];
    }
    [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
    [reString appendString:@"]"];
    return MakeStringCopy(reString);
}


const char * _getHistoryMessages(int conversationType,const char * targetId,long oldestMessageId, int count){
    NSArray * historyMessages = [[RCIMClient sharedRCIMClient] getHistoryMessages:(RCConversationType) conversationType targetId:GetStringParam(targetId) oldestMessageId:oldestMessageId count:count];
    if (historyMessages == nil) {
        return "[]";
    }
    NSMutableString *reString = [NSMutableString string];
    [reString appendString:@"["];
    NSMutableArray *values = [NSMutableArray array];
    for (RCMessage * message  in historyMessages) {
        [values addObject:[NSString stringWithFormat:@"%@",[RongCloudManager RCMessageToJson:message]]];
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

BOOL _setMessageSentStatus(long messageId, int sentStatus){
    return [[RCIMClient sharedRCIMClient] setMessageSentStatus:messageId sentStatus:(RCSentStatus)sentStatus];
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
            [values addObject:[NSString stringWithFormat:@"%@",[RongCloudManager RCMessageToJson:message]]];
        }
        [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
        [reString appendString:@"]"];
        UnitySendMessage( RONGCLOUDMANAGER, "onGetRemoteHistoryMessagesSuccess", MakeStringCopy(reString));
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetRemoteHistoryMessagesFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


void _joinChatRoom(const char * targetId, int messageCount){
    [[RCIMClient sharedRCIMClient] joinChatRoom:GetStringParam(targetId) messageCount:messageCount success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onJoinChatRoomSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onJoinChatRoomFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _quitChatRoom(const char * targetId){
    [[RCIMClient sharedRCIMClient] quitChatRoom:GetStringParam(targetId) success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onQuitChatRoomSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onQuitChatRoomFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}


void _getNotificationQuietHours(){
    [[RCIMClient sharedRCIMClient] getNotificationQuietHours:^(NSString *startTime, int spansMin) {
        NSDictionary * dict = @{@"startTime":startTime,@"spanMinutes":@(spansMin)};
        UnitySendMessage( RONGCLOUDMANAGER, "onGetNotificationQuietHoursSuccess", [RongCloudManager jsonFromObject:dict].UTF8String);
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onGetNotificationQuietHoursFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
    }];
}

void _setNotificationQuietHours(const char * startTime, int spansMin){
    [[RCIMClient sharedRCIMClient] setNotificationQuietHours:GetStringParam(startTime) spanMins:spansMin success:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onSetNotificationQuietHoursFailed", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onSetNotificationQuietHoursFailed", [NSString stringWithFormat:@"%i",status].UTF8String);
        
    }];
}

void _removeNotificationQuietHours(){
    [[RCIMClient sharedRCIMClient] removeNotificationQuietHours:^{
        UnitySendMessage( RONGCLOUDMANAGER, "onRemoveNotificationQuietHoursSuccess", "");
    } error:^(RCErrorCode status) {
        UnitySendMessage( RONGCLOUDMANAGER, "onRemoveNotificationQuietHoursFailed", [NSString stringWithFormat:@"%i",status].UTF8String);

    }];
}