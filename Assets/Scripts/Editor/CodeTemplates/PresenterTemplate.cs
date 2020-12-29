namespace ProjectBlue.CodeGenerator
{

    public class PresenterTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Presentation/Presenter/";
        public override string FileName => $"{className}Presenter.cs";

        public PresenterTemplate(string nameSpaceName, string className) : base(nameSpaceName, className){}
        
        protected override string Template => @"

using #NAME_SPACE#.Presentation.View;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.Presenter
{

    public class #CLASS_NAME#Presenter : I#CLASS_NAME#Presenter 
    {

        I#CLASS_NAME#View view;

        public #CLASS_NAME#Presenter(I#CLASS_NAME#View view)
        {
            this.view = view;
        }

    }
}
";

    }
    
}