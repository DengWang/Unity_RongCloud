package com.volvapps.rongcloudplugin;

import android.app.Activity;
import android.util.Log;
import android.widget.Toast;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

public class RongCloudPluginBase {
	protected static String TAG = "Unity";
	protected static final String MANAGER_NAME = "RongCloudAndroidManager";
	protected static RongCloudPlugin _instance;
	protected Class<?> _unityPlayerClass;
	protected Field _unityPlayerActivityField;
	private Method _unitySendMessageMethod;
	public static Activity _activity;

	public static RongCloudPlugin instance() {
		if (_instance == null) {
			_instance = new RongCloudPlugin();
		}
		return _instance;
	}

	public RongCloudPluginBase() {
		try {
			this._unityPlayerClass = Class
					.forName("com.unity3d.player.UnityPlayer");
			this._unityPlayerActivityField = this._unityPlayerClass
					.getField("currentActivity");
			this._unitySendMessageMethod = this._unityPlayerClass.getMethod(
					"UnitySendMessage", new Class[] { String.class,
							String.class, String.class });
		} catch (ClassNotFoundException e) {
			Log.i(TAG, "could not find UnityPlayer class: " + e.getMessage());
		} catch (NoSuchFieldException e) {
			Log.i(TAG,
					"could not find currentActivity field: " + e.getMessage());
		} catch (Exception e) {
			Log.i(TAG,
					"unkown exception occurred locating getActivity(): "
							+ e.getMessage());
		}
	}

	protected Activity getActivity() {
		if (this._unityPlayerActivityField != null) {
			try {
				Activity activity = (Activity) this._unityPlayerActivityField
						.get(this._unityPlayerClass);
				if (activity == null) {
					Log.e(TAG,
							"Something has gone terribly wrong. The Unity Activity does not exist. This could be due to a low memory situation");
				}
				return activity;
			} catch (Exception e) {
				Log.i(TAG, "error getting currentActivity: " + e.getMessage());
			}
		}

		return _activity;
	}

	protected boolean isActivityAlive() {
		try {
			Activity activity = (Activity) _instance._unityPlayerActivityField
					.get(_instance._unityPlayerClass);
			if (activity != null)
				return true;
		} catch (Exception localException) {
		}
		return false;
	}

	public void UnitySendMessage(String m, String p) {
		if (p == null) {
			p = "";
		}

		if (this._unitySendMessageMethod != null) {
			try {
				this._unitySendMessageMethod.invoke(null, new Object[] {
						MANAGER_NAME, m, p });
			} catch (IllegalArgumentException e) {
				Log.i(TAG,
						"could not find UnitySendMessage method: "
								+ e.getMessage());
			} catch (IllegalAccessException e) {
				Log.i(TAG,
						"could not find UnitySendMessage method: "
								+ e.getMessage());
			} catch (InvocationTargetException e) {
				Log.i(TAG,
						"could not find UnitySendMessage method: "
								+ e.getMessage());
			}
		} else {
			Toast.makeText(getActivity(), "UnitySendMessage:\n" + m + "\n" + p,
					Toast.LENGTH_LONG).show();
			Log.i(TAG, "UnitySendMessage: MANAGER_NAME, " + m + ", " + p);
		}
	}
}
