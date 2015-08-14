//
//  GroupOperationMessage.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/14.
//
//

#import "GroupRequestMessage.h"


@implementation GroupRequestMessage

+(instancetype)messageWithOperation:(NSString *)operatorUserId
                        operatorUserAlias:(NSString *)operatorUserAlias
                               data:(NSString *)data
                            message:(NSString *)message
                              extra:(NSString *)extra

{
    GroupRequestMessage *requestMessage = [[GroupRequestMessage alloc] init];
    if (requestMessage) {
        requestMessage.operatorUserAlias = operatorUserAlias;
        requestMessage.operatorUserId = operatorUserId;
        requestMessage.data = data;
        requestMessage.message = message;
        requestMessage.extra = extra;
    }
    return requestMessage;
}


+(RCMessagePersistent)persistentFlag {
    return (MessagePersistent_ISPERSISTED | MessagePersistent_ISCOUNTED);
}



-(NSData *)encode {
    
    NSMutableDictionary *dataDict=[NSMutableDictionary dictionary];
    [dataDict setObject:self.operatorUserAlias forKey:@"operatorUserAlias"];
    [dataDict setObject:self.operatorUserId forKey:@"operatorUserId"];
    [dataDict setObject:self.data forKey:@"data"];
    [dataDict setObject:self.message forKey:@"message"];
    [dataDict setObject:self.extra forKey:@"extra"];
    //NSDictionary* dataDict = [NSDictionary dictionaryWithObjectsAndKeys:self.content, @"content", nil];
    NSData *data = [NSJSONSerialization dataWithJSONObject:dataDict
                                                   options:kNilOptions
                                                     error:nil];
    return data;
}


-(void)decodeWithData:(NSData *)data {
    
    if (!data) {
        return;
    }
    NSDictionary *json = [NSJSONSerialization JSONObjectWithData:data
                                                         options:kNilOptions
                                                           error:&__error];
    
    if (json) {
        self.operatorUserAlias = json[@"operatorUserAlias"];
        self.operatorUserId = json[@"operatorUserId"];
        self.data = json[@"data"];
        self.message = json[@"message"];
        self.extra = json[@"extra"];
    }
}

+(NSString *)getObjectName {
    return GroupRequestMessageIdentifier;
}

@end