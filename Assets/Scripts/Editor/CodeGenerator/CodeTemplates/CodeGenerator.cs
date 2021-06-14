using System.IO;
using UnityEditor;

namespace ProjectBlue.CodeGenerator
{
    public class CodeGenerator
    {
        public static void Generate(string baseScriptPath, CodeTemplateBase codeTemplate)
        {
            var folderPath = Path.GetDirectoryName(Path.Combine(baseScriptPath + "/", codeTemplate.FolderPath));
            CreateFolder(folderPath);

            var assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, codeTemplate.FileName));
            File.WriteAllText(assetPath, codeTemplate.GetCode());
            AssetDatabase.Refresh();
        }

        private static void CreateFolder(string path)
        {
            var target = "";
            var splitChars = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var dir in path.Split(splitChars))
            {
                var parent = target;
                target = Path.Combine(target, dir);
                if (!AssetDatabase.IsValidFolder(target))
                {
                    AssetDatabase.CreateFolder(parent, dir);
                }
            }
        }
    }
}
