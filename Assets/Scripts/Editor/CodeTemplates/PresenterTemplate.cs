namespace ProjectBlue.CodeGenerator
{

    public class PresenterTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Presentation/Presenter";
        
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(PRESENTER_CODE_TEMPLATE, nameSpace, className);
        }
        
        public static string PRESENTER_CODE_TEMPLATE = @"

using #NAME_SPACE#.Presentation.View;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.Presenter
{

    I#CLASS_NAME#View view;

    public class #CLASS_NAME#Presenter : I#CLASS_NAME#Presenter 
    {

        public #CLASS_NAME#Presenter(I#CLASS_NAME#View view)
        {
            this.view = view;
        }

    }
}
";

    }
    
}