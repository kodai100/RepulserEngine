namespace ProjectBlue.CodeGenerator
{

    public class UseCaseTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Domain/UseCase";
        
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(USE_CASE_CODE_TEMPLATE, nameSpace, className);
        }

        public static string USE_CASE_CODE_TEMPLATE = @"

using #NAME_SPACE#.Presentation.Presenter;
using #NAME_SPACE#.Data.Repository;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Domain.UseCase
{

    public class #CLASS_NAME#UseCase
    {

        I#CLASS_NAME#Presenter presenter;
        I#CLASS_NAME#Repository repository;

        public #CLASS_NAME#UseCase(I#CLASS_NAME#Presenter presenter, I#CLASS_NAME#Repository repository)
        {
            this.presenter = presenter;
            this.repository = repository;
        }
    }
}
";
        
        
        
        

    }
    
}