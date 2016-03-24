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

/// NSCoding
- (instancetype)initWithCoder:(NSCoder *)aDecoder {
    self = [super init];
    if (self) {
        self.operation = [aDecoder decodeObjectForKey:@"operation"];
        self.operatorUserId = [aDecoder decodeObjectForKey:@"operatorUserId"];
        self.data = [aDecoder decodeObjectForKey:@"data"];
        self.message = [aDecoder decodeObjectForKey:@"message"];
        self.extra = [aDecoder decodeObjectForKey:@"extra"];
    }
    return self;
}

/// NSCoding
- (void)encodeWithCoder:(NSCoder *)aCoder {
    [aCoder encodeObject:self.operation forKey:@"operation"];
    [aCoder encodeObject:self.operatorUserId forKey:@"operatorUserId"];
    [aCoder encodeObject:self.data forKey:@"data"];
    [aCoder encodeObject:self.message forKey:@"message"];
    [aCoder encodeObject:self.extra forKey:@"extra"];
}

-(NSData *)encode {
    
    
    NSMutableDictionary *dataDict = [NSMutableDictionary dictionary];
    
    [dataDict setObject:self.operation forKey:@"operation"];
    [dataDict setObject:self.operatorUserId forKey:@"operatorUserId"];
    [dataDict setObject:self.data forKey:@"data"];
    [dataDict setObject:self.message forKey:@"message"];
    [dataDict setObject:self.extra forKey:@"extra"];
    
    NSData *data = [NSJSONSerialization dataWithJSONObject:dataDict
                                                   options:kNilOptions
                                                     error:nil];
    return data;
}


-(void)decodeWithData:(NSData *)data {
    if (data) {
        __autoreleasing NSError *error = nil;
        NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:data options:kNilOptions error:&error];
        if (dictionary) {
            self.operation = dictionary[@"operation"];
            self.operatorUserId = dictionary[@"operatorUserId"];
            self.data = dictionary[@"data"];
            self.message = dictionary[@"message"];
            self.extra = dictionary[@"extra"];
        }
    }
}

+(NSString *)getObjectName {
    return GroupOperationMessageIdentifier;
}

@end