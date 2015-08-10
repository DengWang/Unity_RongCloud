//
//  RongCloudManager.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/7.
//
//

#import <RongIMLib/RongIMLib.h>
#import "RongCloudManager.h"

@implementation RongCloudManager


+ (RongCloudManager*)sharedManager
{
    static RongCloudManager *sharedManager = nil;
    
    if( !sharedManager )
        sharedManager = [[RongCloudManager alloc] init];
    
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


+ (NSString *) RCMessageToJson:(RCMessage *) message
{
    NSLog(@"%@",message.objectName);
    NSDictionary * content = nil;
    if ( [message.content isMemberOfClass:[RCTextMessage class]]) {
        RCTextMessage *msg = (RCTextMessage *)message.content;
        content = @{
                    @"content":msg.content,
                    @"extra":msg.extra == nil ? @"" : msg.extra,
                    };
       
    }else if ([message.content isMemberOfClass:[RCGroupNotificationMessage class]]){
        RCGroupNotificationMessage *msg = (RCGroupNotificationMessage *)message.content;
        content =@{
                   @"operatorUserId":msg.operatorUserId,
                   @"data":msg.data,
                   @"operation":msg.operation,
                   @"message":msg.message,
                   @"extra":msg.extra == nil ? @"" : msg.extra,
                   };
    }
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
                                                         @"content":content == nil ? @"{}":content,
                                                         }];
    return json;
    
}






- (void)onConnectionStatusChanged:(RCConnectionStatus)status {
    UnitySendMessage(RONGCLOUDMANAGER, "onConnectionStatusChanged", [NSString stringWithFormat:@"%i",status].UTF8String);
}

- (void)onReceived:(RCMessage *)message left:(int)nLeft object:(id)object;
{
    NSLog(@"%lu   %@",(unsigned long)message.conversationType,message.objectName);
    UnitySendMessage(RONGCLOUDMANAGER, "onReceived",[RongCloudManager RCMessageToJson:message].UTF8String);
}

@end
