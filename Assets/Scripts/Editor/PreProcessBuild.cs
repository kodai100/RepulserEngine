using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreProcessBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;


    private static string GetVersion()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (var i = 0; i < args.Length; ++i)
        {
            switch (args[i])
            {
                case "-version":
                    return args[i + 1];
            }
        }

        return null;
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerSettings.bundleVersion = GetVersion();

        Debug.Log($"======== Version : {GetVersion()}");
    }
}