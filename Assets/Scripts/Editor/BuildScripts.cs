using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildScripts : MonoBehaviour
{

    private static string LocationPathName()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (var i = 0; i < args.Length; ++i)
        {
            switch (args[i])
            {
                case "-locationPathName":
                    return args[i + 1];
            }
        }

        return null;
    }
    
    
    public static void BuildStandaloneWindows64()
    {

        var locationPathName = LocationPathName();

        if (locationPathName != null)
        {
            // build
            var report = BuildPipeline.BuildPlayer(
                GetEnabledScenes(),
                LocationPathName(),
                BuildTarget.StandaloneWindows64,
                BuildOptions.None
            );

            Debug.Log(report.summary);
        }
        else
        {
            Debug.Log("Build Path is not set");
        }
        
        
    }
    
    static string[] GetEnabledScenes()
    {
        return (
            from scene in EditorBuildSettings.scenes
            where scene.enabled
            where !string.IsNullOrEmpty(scene.path)
            select scene.path
        ).ToArray();
    }
}