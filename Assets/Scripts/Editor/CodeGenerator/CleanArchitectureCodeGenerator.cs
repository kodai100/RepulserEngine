using ProjectBlue.CodeGenerator;
using UnityEditor;
using UnityEngine;

namespace inc.stu.RemoteStudio.Editors
{
    public class CleanArchitectureCodeGenerator : EditorWindow
    {
        [MenuItem("ProjectBlue/Architecture/Clean Architecture Generator")]
        private static void ShowWindow()
        {
            var window = GetWindow<CleanArchitectureCodeGenerator>();
            window.titleContent = new GUIContent("CA Generator");
            window.Show();
        }

        private string _baseScriptPath;
        private string _nameSpaceName;
        private string _className;

        private void OnGUI()
        {
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                _baseScriptPath = EditorGUILayout.TextField("Base script path", _baseScriptPath);
                _nameSpaceName = EditorGUILayout.TextField("Namespace name", _nameSpaceName);
                _className = EditorGUILayout.TextField("Class Name", _className);

                if (GUILayout.Button("Generate Codes"))
                {
                    GenerateCodes();
                }

                EditorGUI.EndDisabledGroup();
            }
        }

        private void OnEnable()
        {
            _baseScriptPath = EditorPrefs.GetString("CAGEN_BASEPATH", "Assets/Scripts");
            _nameSpaceName = EditorPrefs.GetString("CAGEN_NAMESPACE", "ProjectBlue.ProductName");
            _className = EditorPrefs.GetString("CAGEN_CLASSNAME", "SampleClass");
        }

        private void OnDestroy()
        {
            Save();
        }

        private void Save()
        {
            EditorPrefs.SetString("CAGEN_BASEPATH", _baseScriptPath);
            EditorPrefs.SetString("CAGEN_NAMESPACE", _nameSpaceName);
            EditorPrefs.SetString("CAGEN_CLASSNAME", _className);
        }

        private void GenerateCodes()
        {
            Save();

            Pattern.Run(_baseScriptPath, _nameSpaceName, _className);
        }
    }
}