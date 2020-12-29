using System.IO;
using ProjectBlue.CodeGenerator;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CleanArchitectureGenerator : EditorWindow
    {

        [MenuItem("ProjectBLUE/Clean Architecture Generator")]
        private static void ShowWindow()
        {
            var window = GetWindow<CleanArchitectureGenerator>();
            window.titleContent = new GUIContent("Clean Architecture Generator");
            window.Show();
        }

        private string baseScriptPath = "Assets/Scripts";
        private string className = "SampleClass";
        private string nameSpace = "ProjectBlue.RepulserEngine";
        
        private void OnGUI()
        {

            baseScriptPath = EditorGUILayout.TextField("Base Script Path", baseScriptPath);
            nameSpace = EditorGUILayout.TextField("Namespace", nameSpace);
            className = EditorGUILayout.TextField("Class Name", className);

            if (GUILayout.Button("Generate"))
            {
                GenerateCodes();
            }

        }

        private int step = 0;
        
        private void GenerateCodes()
        {

            // View
            step = 0;
            Generate(new ViewTemplate(nameSpace, className));
            
            // Presenter
            Generate(new ViewInterfaceTemplate(nameSpace, className));
            Generate(new PresenterTemplate(nameSpace, className));
            
            // DataStore
            Generate(new DataStoreTemplate(nameSpace, className));
            
            // Repository
            Generate(new DataStoreInterfaceTemplate(nameSpace, className));
            Generate(new RepositoryTemplate(nameSpace, className));
            
            // UseCase
            Generate(new RepositoryInterfaceTemplate(nameSpace, className));
            Generate(new PresenterInterfaceTemplate(nameSpace, className));
            Generate(new UseCaseTemplate(nameSpace, className));
            
            EditorUtility.ClearProgressBar();
        }

        private void Generate(CodeTemplateBase codeTemplate)
        {
            EditorUtility.DisplayProgressBar ("Generating CA codes...", codeTemplate.FileName, step/9f);//プログレスバー表示
            
            var folderPath = Path.GetDirectoryName(Path.Combine(baseScriptPath+"/", codeTemplate.FolderPath));
            CreateFolder(folderPath);
            
            Debug.Log(Path.Combine(folderPath, codeTemplate.FileName));
            
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, codeTemplate.FileName));
            File.WriteAllText(assetPath, codeTemplate.GetCode());
            AssetDatabase.Refresh();
            
            step++;
        }

        private static void CreateFolder(string path)
        {
            var target = "";
            var splitChars = new char[]{ Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var dir in path.Split(splitChars)) {
                var parent = target;
                target = Path.Combine(target, dir);
                if (!AssetDatabase.IsValidFolder(target)) {
                    AssetDatabase.CreateFolder(parent, dir);
                }
            }
        }
    }
}