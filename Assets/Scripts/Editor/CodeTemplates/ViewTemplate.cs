namespace ProjectBlue.CodeGenerator
{

    public class ViewTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Presentation/View";
        
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(VIEW_CODE_TEMPLATE, nameSpace, className);
        }

        public static string VIEW_CODE_TEMPLATE = @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.View
{

    public class #CLASS_NAME#View : MonoBehaviour, I#CLASS_NAME#View 
    {

    }
}
";
        
    }
    
}