namespace ProjectBlue.CodeGenerator
{
    public class ViewInterfaceTemplate: CodeTemplateBase
    {

        public override string FolderPath => "Presentation/Presenter/ViewInterfaces/";

        public override string FileName => $"I{className}View.cs";
        
        public ViewInterfaceTemplate(string nameSpaceName, string className) : base(nameSpaceName, className){}

        protected override string Template =>  @"

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