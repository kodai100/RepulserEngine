namespace ProjectBlue.CodeGenerator
{

    public class RepositoryTemplate : CodeTemplateBase
    {
        
        public override string GetCode(string nameSpace, string className)
        {
            return Replace(REPOSITORY_CODE_TEMPLATE, nameSpace, className);
        }

        public override string FolderPath => "Data/Repository";

        private string REPOSITORY_CODE_TEMPLATE = @"

using #NAME_SPACE#.Data.DataStore;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Data.Repository
{

    I#CLASS_NAME#DataStore dataStore;

    public class #CLASS_NAME#Repository : I#CLASS_NAME#Repository 
    {

        public #CLASS_NAME#Repository(I#CLASS_NAME#DataStore dataStore)
        {
            this.dataStore = dataStore;
        }

    }
}
";


    }
    
}