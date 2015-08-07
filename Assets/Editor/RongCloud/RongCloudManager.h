//
//  RongCloudMnager.h
//  Unity-iPhone
//
//  Created by Deng Wang on 15/8/7.
//
//

#import <RongIMLib/RongIMLib.h>
#define RONGCLOUDMANAGER "RongCloudManager"
@interface RongCloudManager : NSObject <RCConnectionStatusChangeDelegate,RCIMClientReceiveMessageDelegate>


+ (RongCloudMnager*)sharedManager;

@end
