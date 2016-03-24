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

/// NSCoding
- (instancetype)initWithCoder:(NSCoder *)aDecoder {
    self = [super init];
    if (self) {
        self.operatorUserId = [aDecoder decodeObjectForKey:@"operatorUserId"];
        self.operatorUserAlias = [aDecoder decodeObjectForKey:@"operatorUserAlias"];
        self.data = [aDecoder decodeObjectForKey:@"data"];
        self.message = [aDecoder decodeObjectForKey:@"message"];
        self.extra = [aDecoder decodeObjectForKey:@"extra"];
    }
    return self;
}

/// NSCoding
- (void)encodeWithCoder:(NSCoder *)aCoder {
    [aCoder encodeObject:self.operatorUserId forKey:@"operatorUserId"];
    [aCoder encodeObject:self.operatorUserAlias forKey:@"operatorUserAlias"];
    [aCoder encodeObject:self.data forKey:@"data"];
    [aCoder encodeObject:self.message forKey:@"message"];
    [aCoder encodeObject:self.extra forKey:@"extra"];
}

-(NSData *)encode {
    
    NSMutableDictionary *dataDict=[NSMutableDictionary dictionary];
    [dataDict setObject:self.operatorUserAlias forKey:@"operatorUserAlias"];
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
            self.operatorUserAlias = dictionary[@"operatorUserAlias"];
            self.operatorUserId = dictionary[@"operatorUserId"];
            self.data = dictionary[@"data"];
            self.message = dictionary[@"message"];
            self.extra = dictionary[@"extra"];
        }
    }

}

+(NSString *)getObjectName {
    return GroupRequestMessageIdentifier;
}

@end