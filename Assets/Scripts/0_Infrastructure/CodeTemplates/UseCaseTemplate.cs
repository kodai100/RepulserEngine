namespace ProjectBlue.CodeGenerator
{

    public class UseCaseTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Domain/UseCase/";
        public override string FileName => $"{className}UseCase.cs";

        public UseCaseTemplate(string className, string nameSpaceName)
            : base(className, nameSpaceName) {}
        
        protected override string Template => @"

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