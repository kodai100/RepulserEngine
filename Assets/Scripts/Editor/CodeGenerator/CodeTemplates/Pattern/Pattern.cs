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

            // Repository
            Generate(baseScriptPath,
                new RepositoryInterfaceTemplate(nameSpace, "4_InterfaceAdapters/Interface/RepositoryInterfaces/",
                    className));
            Generate(baseScriptPath,
                new RepositoryTemplate(nameSpace, "5_FrameworksAndDrivers/Repository/", className));

            // UseCase
            Generate(baseScriptPath, new UseCaseTemplate(nameSpace, "3_ApplicationBusinessRules/UseCase/", className));
            Generate(baseScriptPath,
                new UseCaseInterfaceTemplate(nameSpace, "3_ApplicationBusinessRules/UseCaseInterfaces/", className));

            // Controller
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