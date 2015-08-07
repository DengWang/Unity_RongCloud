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


void _init(const char * appKey){
    [[RCIMClient sharedRCIMClient] init: GetStringParam(appKey)];

}

void _setDeviceToken(const char * deviceToken){
    [[RCIMClient sharedRCIMClient] setDeviceToken:GetStringParam(deviceToken)];
}

void _connectToRongCloudServer(const char * token){
    
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


void _sendTextMessage(int conversationType,const char * targetId, const char * content,const char * extra){
    RCTextMessage *rcTextMessage = [RCTextMessage messageWithContent:GetStringParam(content)];
    rcTextMessage.extra = GetStringParam(extra);
    [[RCIMClient sharedRCIMClient] sendMessage:(RCConversationType)conversationType targetId:GetStringParam(targetId) content:rcTextMessage pushContent:GetStringParam(content)  success:^(long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageSuccess", [NSString stringWithFormat:@"%li",messageId].UTF8String);
    } error:^(RCErrorCode nErrorCode,long messageId){
        UnitySendMessage( RONGCLOUDMANAGER, "onSendMessageFailed", [NSString stringWithFormat:@"%i",nErrorCode].UTF8String);
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







