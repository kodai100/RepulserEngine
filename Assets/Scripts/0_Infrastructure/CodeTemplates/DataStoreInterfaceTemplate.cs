namespace ProjectBlue.CodeGenerator
{
    public class DataStoreInterfaceTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Data/Repository/DataStoreInterfaces/";

        public override string FileName => $"I{className}DataStore.cs";

        public DataStoreInterfaceTemplate(string nameSpaceName, string className) : base(nameSpaceName, className)
        {
        }

        protected override string Template =>  @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Data.DataStore
{

    public interface I#CLASS_NAME#DataStore
    {
    }

}
";
    }
}