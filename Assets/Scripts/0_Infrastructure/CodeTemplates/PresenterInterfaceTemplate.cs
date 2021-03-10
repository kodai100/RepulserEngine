namespace ProjectBlue.CodeGenerator
{
    public class PresenterInterfaceTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Domain/PresenterInterfaces/";

        public override string FileName => $"I{className}Presenter.cs";

        public PresenterInterfaceTemplate(string nameSpaceName, string className) : base(nameSpaceName, className)
        {
        }

        protected override string Template =>  @"

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