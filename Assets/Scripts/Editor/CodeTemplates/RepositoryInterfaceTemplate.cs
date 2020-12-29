namespace ProjectBlue.CodeGenerator
{
    public class RepositoryInterfaceTemplate : CodeTemplateBase
    {
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(REPOSITORY_INTERFACE_CODE_TEMPLATE, nameSpace, className);
        }

        public override string FolderPath => "Domain/RepositoryInterfaces";
        
        public static string REPOSITORY_INTERFACE_CODE_TEMPLATE = @"

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