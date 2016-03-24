//
//  GroupOperationMessage.h
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/14.
//
//




#import <RongIMLib/RongIMLib.h>

#define GroupOperationMessageIdentifier @"VOLV:GroupOp"



/**
 *  群组消息类
 */
@interface GroupOperationMessage : RCMessageContent<NSCoding>
/**
 *  操作人 UserId，可以为空
 */
@property(nonatomic, strong) NSString *operatorUserId;
/**
 *  操作名，对应 GroupOperationXxxx，或任意字符串。
 */
@property(nonatomic, strong) NSString *operation;
/**
 *  被操做人 UserId 或者操作数据（如改名后的名称）。
 */
@property(nonatomic, strong) NSString *data;
/**
 *  操作信息，可以为空，如：你被 xxx 踢出了群。
 */
@property(nonatomic, strong) NSString *message;
/**
 *  附加信息。
 */
@property(nonatomic, strong) NSString *extra; // 附加信息。

/**
 *  构造方法
 *
 *  @param operation      操作名，对应 GroupOperationXxxx，或任意字符串。
 *  @param operatorUserId 操作人 UserId，可以为空
 *  @param data           被操做人 UserId 或者操作数据（如改名后的名称）。
 *  @param message        操作信息，可以为空，如：你被 xxx 踢出了群。
 *  @param extra          附加信息
 *
 *  @return 类方法
 */
+ (instancetype)messageWithOperation:(NSString *)operation
                           operatorUserId:(NSString *)operatorUserId
                                     data:(NSString *)data
                                  message:(NSString *)message
                                    extra:(NSString *)extra;

@end


