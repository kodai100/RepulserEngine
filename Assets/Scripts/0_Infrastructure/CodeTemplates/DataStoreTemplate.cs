namespace ProjectBlue.CodeGenerator
{

    public class DataStoreTemplate : CodeTemplateBase
    {
        public override string FolderPath => "Data/DataStore/";
        public override string FileName => $"{className}DataStore.cs";

        public DataStoreTemplate(string nameSpaceName, string className) : base(nameSpaceName, className)
        {
        }

        protected override string Template => @"

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