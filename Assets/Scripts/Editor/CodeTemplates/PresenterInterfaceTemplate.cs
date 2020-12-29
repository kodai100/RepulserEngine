namespace ProjectBlue.CodeGenerator
{
    public class PresenterInterfaceTemplate : CodeTemplateBase
    {
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(PRESENTER_INTERFACE_CODE_TEMPLATE, nameSpace, className);
        }

        public override string FolderPath => "Domain/PresenterInterfaces";
        
        public static string PRESENTER_INTERFACE_CODE_TEMPLATE = @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.Presenter
{

    public interface I#CLASS_NAME#Presenter
    {
    }

}
";
        
    }
}