using UnityEditor;

namespace ProjectBlue.CodeGenerator
{
    public static class Pattern
    {
        private static int _step;
        private const float TotalStepNum = 10f;

        public static void Run(string baseScriptPath, string nameSpace, string className)
        {
            _step = 0;

            // Model
            // Generate(baseScriptPath, new ModelTemplate(nameSpace, "2_EnterpriseBusinessRules/Model/", className));

            // DataStore
            Generate(baseScriptPath, new DataStoreTemplate(nameSpace, "5_FrameworksAndDrivers/DataStore/", className));

            // Repository
            Generate(baseScriptPath,
                new DataStoreInterfaceTemplate(nameSpace, "4_InterfaceAdapters/Interface/DataStoreInterfaces/",
                    className));
            Generate(baseScriptPath,
                new RepositoryTemplate(nameSpace, "4_InterfaceAdapters/Implementation/Repository/", className));

            // UseCase
            Generate(baseScriptPath,
                new RepositoryInterfaceTemplate(nameSpace, "3_ApplicationBusinessRules/RepositoryInterfaces/",
                    className));
            Generate(baseScriptPath, new UseCaseTemplate(nameSpace, "3_ApplicationBusinessRules/UseCase/", className));
            Generate(baseScriptPath,
                new UseCaseInterfaceTemplate(nameSpace, "3_ApplicationBusinessRules/UseCaseInterfaces/", className));

            // Presenter
            Generate(baseScriptPath,
                new ControllerTemplate(nameSpace, "4_InterfaceAdapters/Implementation/Controller/", className));
            Generate(baseScriptPath,
                new ControllerInterfaceTemplate(nameSpace, "4_InterfaceAdapters/Interface/ControllerInterfaces/",
                    className));

            EditorUtility.ClearProgressBar();
        }

        private static void Generate(string baseScriptPath, CodeTemplateBase codeTemplateBase)
        {
            CodeGenerator.Generate(baseScriptPath, codeTemplateBase);
            _step++;
            EditorUtility.DisplayProgressBar("Generating CA codes...", codeTemplateBase.FileName, _step / TotalStepNum);
        }
    }
}
