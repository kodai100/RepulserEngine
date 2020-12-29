namespace ProjectBlue.CodeGenerator
{

    public class DataStoreTemplate : CodeTemplateBase
    {
        public override string FolderPath => "Data/DataStore";

        public override string GetCode(string nameSpace, string className)
        {
            return Replace(DATASTORE_CODE_TEMPLATE, nameSpace, className);
        }

        
        private string DATASTORE_CODE_TEMPLATE = @"

using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Data.DataStore
{

    public class #CLASS_NAME#DataStore : I#CLASS_NAME#DataStore
    {

    }
}
";

    }
    
}