//
//  RongCloudMnager.h
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/7.
//
//

#import <RongIMKit/RongIMKit.h>

@interface RongCloudMnager : NSObject <RCConnectionStatusChangeDelegate,RCIMClientReceiveMessageDelegate>


    + (RongCloudMnager*)sharedManager;

@end
