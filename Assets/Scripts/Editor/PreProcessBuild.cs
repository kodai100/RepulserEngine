using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreProcessBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;


    private static string GetGitHubRef()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (var i = 0; i < args.Length; ++i)
        {
            switch (args[i])
            {
                case "-githubref":
                    return args[i + 1];
            }
        }

        return null;
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        var githubRef = GetGitHubRef();

        var res = githubRef.Split('/');

        if (res.Length == 3)
        {
            PlayerSettings.bundleVersion = res[2];
            Debug.Log($"======== Version : {PlayerSettings.bundleVersion}");
        }
    }
}