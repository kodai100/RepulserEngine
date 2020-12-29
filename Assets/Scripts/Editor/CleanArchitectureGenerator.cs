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

        private void GenerateCodes()
        {
            
            // View
            Generate(new ViewTemplate());
            
            // Presenter
            Generate(new ViewInterfaceTemplate());
            Generate(new PresenterTemplate());
            
            // DataStore
            Generate(new DataStoreTemplate());
            
            // Repository
            Generate(new DataStoreInterfaceTemplate());
            Generate(new RepositoryTemplate());
            
            // UseCase
            Generate(new RepositoryInterfaceTemplate());
            Generate(new PresenterInterfaceTemplate());
            Generate(new UseCaseTemplate());
        }

        private void Generate(CodeTemplateBase codeTemplate)
        {
            var folderPath = Path.GetDirectoryName(Path.Combine(baseScriptPath, codeTemplate.FolderPath));
            CreateFolder(folderPath);
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, className+".cs"));
            File.WriteAllText(assetPath, codeTemplate.GetCode(nameSpace, className));
            AssetDatabase.Refresh();
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