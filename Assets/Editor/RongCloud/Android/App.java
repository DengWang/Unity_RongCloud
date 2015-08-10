package com.volvapps.rongcloudplugin;

import android.app.ActivityManager;
import android.app.Application;
import android.content.Context;
import io.rong.imlib.RongIMClient;

/**
 * Created by bob on 2015/1/30.
 */
public class App extends Application {

    @Override
    public void onCreate() {

        super.onCreate();

        /**
         * 注意：
         *
         * IMKit SDK调用第一步 初始化
         *
         * context上下文
         *
         * 只有两个进程需要初始化，主进程和 push 进程
         */
        if("io.rong.app".equals(getCurProcessName(getApplicationContext())) ||
                "io.rong.push".equals(getCurProcessName(getApplicationContext()))) {
        	RongIMClient.init(this);

            /**
             * 融云SDK事件监听处理
             *
             * 注册相关代码，只需要在主进程里做。
             */
            if ("io.rong.app".equals(getCurProcessName(getApplicationContext()))) {
                RongCloudEvent.init(this);
            }
        }
    }

    public static String getCurProcessName(Context context) {
        int pid = android.os.Process.myPid();
        ActivityManager activityManager = (ActivityManager) context
                .getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningAppProcessInfo appProcess : activityManager
                .getRunningAppProcesses()) {
            if (appProcess.pid == pid) {
                return appProcess.processName;
            }
        }
        return null;
    }

}
