using System;
using System.IO;
using UnityEditor;

public static class BuildCommand
{
    // Wywołaj z Unity: -executeMethod BuildCommand.BuildAndroid
    public static void BuildAndroid()
    {
        try
        {
            var buildDir = Path.Combine(Directory.GetCurrentDirectory(), "build", "android");
            if (!Directory.Exists(buildDir)) Directory.CreateDirectory(buildDir);

            // Ustawienia podpisywania (jeśli plik keystore.jks istnieje w katalogu roboczym)
            var keystorePath = Path.Combine(Directory.GetCurrentDirectory(), "keystore.jks");
            if (File.Exists(keystorePath))
            {
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = keystorePath;
                var keystorePass = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PASSWORD") ?? "";
                var keyAlias = Environment.GetEnvironmentVariable("ANDROID_KEY_ALIAS") ?? "";
                var keyAliasPass = Environment.GetEnvironmentVariable("ANDROID_KEY_PASSWORD") ?? "";
                PlayerSettings.Android.keystorePass = keystorePass;
                PlayerSettings.Android.keyaliasName = keyAlias;
                PlayerSettings.Android.keyaliasPass = keyAliasPass;
            }

            // Wymuś APK zamiast AAB
            EditorUserBuildSettings.buildAppBundle = false;

            // Nazwa buildu
            var outputApk = Path.Combine(buildDir, "GameBuild.apk");

            // Zbierz wszystkie sceny z BuildSettings
            var scenes = new System.Collections.Generic.List<string>();
            foreach (var s in EditorBuildSettings.scenes)
            {
                if (s.enabled) scenes.Add(s.path);
            }

            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes.ToArray(),
                locationPathName = outputApk,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Console.WriteLine("Build succeeded: " + outputApk);
                EditorApplication.Exit(0);
            }
            else
            {
                Console.WriteLine("Build failed: " + report.summary.result);
                EditorApplication.Exit(1);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception during build: " + ex);
            EditorApplication.Exit(2);
        }
    }
}
