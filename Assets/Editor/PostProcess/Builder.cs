using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using System.IO;

public class Builder : Editor
{

	[PostProcessBuild (500)]
	public static void OnPostProcessBuild (BuildTarget buildTarget, string path)
	{
		#if UNITY_ANDROID

		#elif UNITY_IPHONE
		string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject pbxProj = new PBXProject ();
		pbxProj.ReadFromFile (projPath);
		string target = pbxProj.TargetGuidByName (PBXProject.GetUnityTargetName ());

		pbxProj.AddFrameworkToProject (target, "AssetsLibrary.framework", false);
		pbxProj.AddFrameworkToProject (target, "AudioToolbox.framework", false);
		pbxProj.AddFrameworkToProject (target, "AVFoundation.framework", false);
		pbxProj.AddFrameworkToProject (target, "CFNetwork.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreAudio.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreLocation.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreMedia.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreTelephony.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreVideo.framework", false);
		pbxProj.AddFrameworkToProject (target, "CoreGraphics.framework", false);
		pbxProj.AddFrameworkToProject (target, "ImageIO.framework", false);

		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libc++.dylib", "Frameworks/libc++.dylib", PBXSourceTree.Sdk));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libc++abi.dylib", "Frameworks/libc++abi.dylib", PBXSourceTree.Sdk));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libsqlite3.dylib", "Frameworks/libsqlite3.dylib", PBXSourceTree.Sdk));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libstdc++.dylib", "Frameworks/libstdc++.dylib", PBXSourceTree.Sdk));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libxml2.dylib", "Frameworks/libxml2.dylib", PBXSourceTree.Sdk));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile ("usr/lib/libz.dylib", "Frameworks/libz.dylib", PBXSourceTree.Sdk));

		pbxProj.AddFrameworkToProject (target, "MapKit.framework", false);
		pbxProj.AddFrameworkToProject (target, "OpenGLES.framework", false);
		pbxProj.AddFrameworkToProject (target, "QuartzCore.framework", false);
		pbxProj.AddFrameworkToProject (target, "SystemConfiguration.framework", false);
		pbxProj.AddFrameworkToProject (target, "UIKit.framework", false);

		DirectoryInfo di = new DirectoryInfo (Application.dataPath);
		string rongCloudLibFloder = Path.Combine (di.Parent.FullName, "RongCloudLib");




		CopyAndReplaceDirectory(Path.Combine (rongCloudLibFloder, "RongIMLib.framework"), Path.Combine(path, "Frameworks/RongIMLib.framework"));
		pbxProj.AddFileToBuild(target, pbxProj.AddFile("Frameworks/RongIMLib.framework", "Frameworks/RongIMLib.framework", PBXSourceTree.Source));


//		pbxProj.AddFileToBuild (target, pbxProj.AddFile (Path.Combine (rongCloudLibFloder, "RongIMLib.framework"), "Frameworks/RongIMLib.framework", PBXSourceTree.Absolute));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile (Path.Combine (Application.dataPath, "Editor/RongCloud/RongCloudBinding.m"), "Libraries/RongCloudBinding.m", PBXSourceTree.Absolute));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile (Path.Combine (Application.dataPath, "Editor/RongCloud/RongCloudManager.m"), "Libraries/RongCloudManager.m", PBXSourceTree.Absolute));
		pbxProj.AddFileToBuild (target, pbxProj.AddFile (Path.Combine (Application.dataPath, "Editor/RongCloud/RongCloudManager.h"), "Libraries/RongCloudManager.h", PBXSourceTree.Absolute));



		pbxProj.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
		pbxProj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");

		pbxProj.WriteToFile (projPath);
		PlistDocument plist = new PlistDocument ();
		string plistPath = Path.Combine (path, "Info.plist");
		plist.ReadFromFile (plistPath);
		var deviceCapabilities = plist.root ["UIRequiredDeviceCapabilities"].AsArray ();
		deviceCapabilities.AddString ("front-facing-camera");
		deviceCapabilities.AddString ("video-camera");
//		plist.root.SetBoolean ("LSHasLocalizedDisplayName", true);

		plist.WriteToFile (plistPath);
		#endif

	}


	internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
	{
		if (Directory.Exists(dstPath))
			Directory.Delete(dstPath);
		if (File.Exists(dstPath))
			File.Delete(dstPath);

		Directory.CreateDirectory(dstPath);

		foreach (var file in Directory.GetFiles(srcPath))
			File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

		foreach (var dir in Directory.GetDirectories(srcPath))
			CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
	}


}
