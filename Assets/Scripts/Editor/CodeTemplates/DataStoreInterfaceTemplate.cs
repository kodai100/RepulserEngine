namespace ProjectBlue.CodeGenerator
{
    public class DataStoreInterfaceTemplate : CodeTemplateBase
    {
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(DATASTORE_INTERFACE_CODE_TEMPLATE, nameSpace, className);
        }

        public override string FolderPath => "Data/Repository/DataStoreInterfaces";
        
        private string DATASTORE_INTERFACE_CODE_TEMPLATE = @"

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