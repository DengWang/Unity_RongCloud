//
//  RongCloudManager.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/7.
//
//

#import <RongIMKit/RongIMKit.h>
#import "RongCloudMnager.h"

@implementation RongCloudMnager


+ (RongCloudMnager*)sharedManager
{
    static RongCloudMnager *sharedManager = nil;
    
    if( !sharedManager )
        sharedManager = [[RongCloudMnager alloc] init];
    
    return sharedManager;
}


+ (id)objectFromJson:(NSString*)json
{
    NSError *error = nil;
    NSData *data = [NSData dataWithBytes:json.UTF8String length:json.length];
    NSObject *object = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&error];
    
    if( error )
        NSLog( @"failed to deserialize JSON: %@ with error: %@", json, [error localizedDescription] );
    
    return object;
}


+ (NSString*)jsonFromObject:(id)object
{
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:object options:0 error:&error];
    
    if( jsonData && !error )
        return [[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] autorelease];
    else
        NSLog( @"jsonData was null, error: %@", [error localizedDescription] );
    
    return @"{}";
}

- (void)onConnectionStatusChanged:(RCConnectionStatus)status {
//    if (status == ConnectionStatus_KICKED_OFFLINE_BY_OTHER_CLIENT) {
//        UIAlertView *alert = [[UIAlertView alloc]
//                              initWithTitle:@"提示"
//                              message:@"您"
//                              @"的帐号在别的设备上登录，您被迫下线！"
//                              delegate:nil
//                              cancelButtonTitle:@"知道了"
//                              otherButtonTitles:nil, nil];
//        [alert show];
//        RCDLoginViewController *loginVC = [[RCDLoginViewController alloc] init];
        // [loginVC defaultLogin];
        // RCDLoginViewController* loginVC = [storyboard
        // instantiateViewControllerWithIdentifier:@"loginVC"];
//        UINavigationController *_navi =
//        [[UINavigationController alloc] initWithRootViewController:loginVC];
//        self.window.rootViewController = _navi;
    
//    }
    UnitySendMessage("RongCloudMnager", "onConnectionStatusChanged", [NSString stringWithFormat:@"%i",status].UTF8String);
}

- (void)onReceived:(RCMessage *)message left:(int)nLeft object:(id)object;
{
//    if ([message.content isMemberOfClass:[RCInformationNotificationMessage class]]) {
//        RCInformationNotificationMessage *msg=(RCInformationNotificationMessage *)message.content;
//        //NSString *str = [NSString stringWithFormat:@"%@",msg.message];
//        if ([msg.message rangeOfString:@"你已添加了"].location!=NSNotFound) {
//            [RCDDataSource syncFriendList:^(NSMutableArray *friends) {
//            }];
//        }
//    }
//    NSString *json = [RongCloudMnager jsonFromObject:@{ @"type": message.conversationType, @"content": message.content,@"sendId": message.senderUserId,@"extra":message.extra}];
//        UnitySendMessage("RongCloudMnager", "onReceived", );
}

@end
