//
//  GroupOperationMessage.m
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/14.
//
//

#import "GroupOperationMessage.h"


@implementation GroupOperationMessage

+(instancetype)messageWithOperation:(NSString *)operation
                     operatorUserId:(NSString *)operatorUserId
                               data:(NSString *)data
                            message:(NSString *)message
                              extra:(NSString *)extra

{
    GroupOperationMessage *operationMessage = [[GroupOperationMessage alloc] init];
    if (operationMessage) {
        operationMessage.operation = operation;
        operationMessage.operatorUserId = operatorUserId;
        operationMessage.data = data;
        operationMessage.message = message;
        operationMessage.extra = extra;
    }
    return operationMessage;
}


+(RCMessagePersistent)persistentFlag {
    return (MessagePersistent_ISPERSISTED | MessagePersistent_ISCOUNTED);
}



-(NSData *)encode {
    
    NSMutableDictionary *dataDict=[NSMutableDictionary dictionary];
    [dataDict setObject:self.operation forKey:@"operation"];
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
        self.operation = json[@"operation"];
        self.operatorUserId = json[@"operatorUserId"];
        self.data = json[@"data"];
        self.message = json[@"message"];
        self.extra = json[@"extra"];
    }
}

+(NSString *)getObjectName {
    return GroupOperationMessageIdentifier;
}

@end