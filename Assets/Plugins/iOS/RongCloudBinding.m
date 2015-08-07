//
//  RongCloudBinding.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/6.
//
//



#import "UnityAppController.h"
#import <RongIMKit/RongIMKit.h>


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
//        [[RCIMClient sharedRCIMClient] setReceiveMessageDelegate:self object:nil];
        // Connect 成功
        UnitySendMessage( "RongCloudManager", "connectSuccess", userId.UTF8String);
        
        
        
    }error:^(RCConnectErrorCode status) {
        // Connect 失败
        UnitySendMessage( "RongCloudManager", "connectFailed", "");
    }tokenIncorrect:^() {
        UnitySendMessage( "RongCloudManager", "tokenIncorrect", "");
    }];
    
}







