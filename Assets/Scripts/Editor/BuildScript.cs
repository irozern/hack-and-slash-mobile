using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Automated build script for CI/CD pipelines (Codemagic, GitHub Actions, etc.)
/// Usage: Unity -quit -batchmode -nographics -projectPath . -executeMethod BuildScript.BuildAndroid
/// </summary>
public class BuildScript
{
    // Build settings
    private const string ANDROID_BUILD_PATH = "build/outputs/apk/release/app-release-unsigned.apk";
    private const string IOS_BUILD_PATH = "build/iOS";
    private const string BUNDLE_ID = "space.manus.hackslash";
    private const string VERSION = "1.0.0";
    private const int BUILD_NUMBER = 1;

    /// <summary>
    /// Build Android APK for release
    /// </summary>
    public static void BuildAndroid()
    {
        try
        {
            Debug.Log("=== Starting Android Build ===");
            
            // Set build settings
            SetBuildSettings();
            SetAndroidSettings();
            
            // Get scenes
            string[] scenes = GetScenes();
            
            // Build APK
            BuildPlayerOptions buildOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = ANDROID_BUILD_PATH,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };
            
            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            
            // Check result
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("✓ Android build succeeded!");
                Debug.Log($"Build size: {report.summary.totalSize / (1024 * 1024)} MB");
                Environment.Exit(0);
            }
            else
            {
                Debug.LogError("✗ Android build failed!");
                Environment.Exit(1);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"✗ Build error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Build iOS XCode project
    /// </summary>
    public static void BuildiOS()
    {
        try
        {
            Debug.Log("=== Starting iOS Build ===");
            
            // Set build settings
            SetBuildSettings();
            SetIOSSettings();
            
            // Get scenes
            string[] scenes = GetScenes();
            
            // Build XCode project
            BuildPlayerOptions buildOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = IOS_BUILD_PATH,
                target = BuildTarget.iOS,
                options = BuildOptions.None
            };
            
            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            
            // Check result
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("✓ iOS build succeeded!");
                Debug.Log($"Build size: {report.summary.totalSize / (1024 * 1024)} MB");
                Environment.Exit(0);
            }
            else
            {
                Debug.LogError("✗ iOS build failed!");
                Environment.Exit(1);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"✗ Build error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Set common build settings
    /// </summary>
    private static void SetBuildSettings()
    {
        Debug.Log("Setting build settings...");
        
        // Set version
        PlayerSettings.bundleVersion = VERSION;
        
        // Set company and product name
        PlayerSettings.companyName = "Manus";
        PlayerSettings.productName = "Hack & Slash";
        
        // Set graphics
        PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.OpenGLES3 });
        
        // Set scripting backend
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        
        // Set optimization
        PlayerSettings.stripEngineCode = true;
        
        Debug.Log("✓ Build settings configured");
    }

    /// <summary>
    /// Set Android-specific settings
    /// </summary>
    private static void SetAndroidSettings()
    {
        Debug.Log("Setting Android settings...");
        
        // Set bundle ID
        PlayerSettings.Android.bundleVersionCode = BUILD_NUMBER;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, BUNDLE_ID);
        
        // Set API levels
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel33;
        
        // Set architecture
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        
        // Set permissions
        PlayerSettings.Android.useCustomKeystore = true;
        
        // Set build type
        EditorUserBuildSettings.androidBuildAppBundle = false;
        EditorUserBuildSettings.androidBuildType = AndroidBuildType.Release;
        
        Debug.Log("✓ Android settings configured");
    }

    /// <summary>
    /// Set iOS-specific settings
    /// </summary>
    private static void SetIOSSettings()
    {
        Debug.Log("Setting iOS settings...");
        
        // Set bundle ID
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, BUNDLE_ID);
        
        // Set minimum iOS version
        PlayerSettings.iOS.minimumiOSVersion = iOSTargetOSVersion.iOS12;
        
        // Set architecture
        PlayerSettings.iOS.architectureType = iOSArchitecture.ARM64;
        
        // Set build type
        EditorUserBuildSettings.iOSBuildType = iOSBuildType.Release;
        
        Debug.Log("✓ iOS settings configured");
    }

    /// <summary>
    /// Get all scenes to build
    /// </summary>
    private static string[] GetScenes()
    {
        string[] scenes = new string[EditorBuildSettingsScene.Length];
        
        for (int i = 0; i < EditorBuildSettingsScene.Length; i++)
        {
            scenes[i] = EditorBuildSettingsScene[i].path;
        }
        
        if (scenes.Length == 0)
        {
            Debug.LogWarning("No scenes found in build settings!");
            scenes = new[] { "Assets/Scenes/GameScene.unity" };
        }
        
        Debug.Log($"Building {scenes.Length} scene(s):");
        foreach (string scene in scenes)
        {
            Debug.Log($"  - {scene}");
        }
        
        return scenes;
    }

    /// <summary>
    /// Print build info
    /// </summary>
    [MenuItem("Build/Print Info")]
    public static void PrintBuildInfo()
    {
        Debug.Log("=== Build Configuration ===");
        Debug.Log($"Bundle ID: {BUNDLE_ID}");
        Debug.Log($"Version: {VERSION}");
        Debug.Log($"Build Number: {BUILD_NUMBER}");
        Debug.Log($"Android Path: {ANDROID_BUILD_PATH}");
        Debug.Log($"iOS Path: {IOS_BUILD_PATH}");
        Debug.Log($"Scenes: {EditorBuildSettingsScene.Length}");
    }

    /// <summary>
    /// Clean build directories
    /// </summary>
    [MenuItem("Build/Clean")]
    public static void CleanBuild()
    {
        Debug.Log("Cleaning build directories...");
        
        if (Directory.Exists("build"))
        {
            Directory.Delete("build", true);
            Debug.Log("✓ Cleaned build directory");
        }
        
        if (Directory.Exists("Temp"))
        {
            Directory.Delete("Temp", true);
            Debug.Log("✓ Cleaned Temp directory");
        }
    }
}
