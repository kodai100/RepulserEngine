namespace ProjectBlue.CodeGenerator
{
    public class ViewInterfaceTemplate: CodeTemplateBase
    {
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(VIEW_INTERFACE_CODE_TEMPLATE, nameSpace, className);
        }

        public override string FolderPath => "Presentation/Presenter/ViewInterfaces";
        
        public static string VIEW_INTERFACE_CODE_TEMPLATE = @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.View
{

    public interface I#CLASS_NAME#View
    {
    }

}
";
    }
}