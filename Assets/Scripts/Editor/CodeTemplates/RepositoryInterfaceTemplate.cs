namespace ProjectBlue.CodeGenerator
{
    public class RepositoryInterfaceTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Domain/RepositoryInterfaces/";
        public override string FileName => $"I{className}Repository.cs";
        
        public RepositoryInterfaceTemplate(string nameSpaceName, string className) : base(nameSpaceName, className){}

        protected override string Template => @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Data.Repository
{

    public interface I#CLASS_NAME#Repository
    {
    }

}
";
    }
}