using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildCommand
{
    private const string DefaultAndroidOutput = "build/android/GameBuild.apk";
    private const string DefaultIosOutput = "build/ios";
    private const string DefaultBundleId = "space.manus.hackslash";
    private const string DefaultVersion = "1.0.0";
    private const int DefaultAndroidVersionCode = 1;

    // Wywołaj z Unity: -executeMethod BuildCommand.BuildAndroid
    public static void BuildAndroid()
    {
        try
        {
            var outputApk = GetEnv("ANDROID_BUILD_PATH", DefaultAndroidOutput);
            EnsureDirectoryExists(Path.GetDirectoryName(outputApk));

            ConfigureAndroidSigning();
            ConfigureAndroidPlayerSettings();

            var scenes = GetEnabledScenes();
            if (scenes.Length == 0)
            {
                LogError("No enabled scenes found in Build Settings.");
                Exit(1);
                return;
            }

            var buildOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = outputApk,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildOptions);
            FinishBuild(report, outputApk);
        }
        catch (Exception ex)
        {
            LogError("Exception during Android build: " + ex.Message);
            Exit(2);
        }
    }

    public static void BuildiOS()
    {
        try
        {
            var outputPath = GetEnv("IOS_BUILD_PATH", DefaultIosOutput);
            EnsureDirectoryExists(outputPath);

            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, DefaultBundleId);
            PlayerSettings.bundleVersion = GetEnv("APP_VERSION", DefaultVersion);

            EditorUserBuildSettings.iOSBuildType = iOSBuildType.Release;

            var scenes = GetEnabledScenes();
            if (scenes.Length == 0)
            {
                LogError("No enabled scenes found in Build Settings.");
                Exit(1);
                return;
            }

            var buildOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = outputPath,
                target = BuildTarget.iOS,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildOptions);
            FinishBuild(report, outputPath);
        }
        catch (Exception ex)
        {
            LogError("Exception during iOS build: " + ex.Message);
            Exit(2);
        }
    }

    private static void ConfigureAndroidSigning()
    {
        var keystorePath = GetEnv("ANDROID_KEYSTORE_PATH", Path.Combine(Directory.GetCurrentDirectory(), "keystore.jks"));
        if (!File.Exists(keystorePath))
        {
            Log("Android keystore not found, building without custom keystore.");
            PlayerSettings.Android.useCustomKeystore = false;
            return;
        }

        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = GetEnv("ANDROID_KEYSTORE_PASSWORD", "");
        PlayerSettings.Android.keyaliasName = GetEnv("ANDROID_KEY_ALIAS", "");
        PlayerSettings.Android.keyaliasPass = GetEnv("ANDROID_KEY_PASSWORD", "");

        Log("Android signing configured using keystore: " + keystorePath);
    }

    private static void ConfigureAndroidPlayerSettings()
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, DefaultBundleId);
        PlayerSettings.bundleVersion = GetEnv("APP_VERSION", DefaultVersion);
        PlayerSettings.Android.bundleVersionCode = GetIntEnv("ANDROID_VERSION_CODE", DefaultAndroidVersionCode);

        EditorUserBuildSettings.androidBuildAppBundle = false;
        EditorUserBuildSettings.androidBuildType = AndroidBuildType.Release;
    }

    private static string[] GetEnabledScenes()
    {
        var scenes = new System.Collections.Generic.List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled) scenes.Add(scene.path);
        }

        return scenes.ToArray();
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (string.IsNullOrEmpty(path)) return;
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    private static string GetEnv(string name, string defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return string.IsNullOrEmpty(value) ? defaultValue : value;
    }

    private static int GetIntEnv(string name, int defaultValue)
    {
        var raw = Environment.GetEnvironmentVariable(name);
        return int.TryParse(raw, out var value) ? value : defaultValue;
    }

    private static void FinishBuild(BuildReport report, string outputPath)
    {
        if (report.summary.result == BuildResult.Succeeded)
        {
            Log("Build succeeded: " + outputPath);
            Exit(0);
        }

        LogError("Build failed: " + report.summary.result);
        Exit(1);
    }

    private static void Log(string message)
    {
        Console.WriteLine(message);
    }

    private static void LogError(string message)
    {
        Console.Error.WriteLine(message);
    }

    private static void Exit(int exitCode)
    {
        EditorApplication.Exit(exitCode);
    }
}
