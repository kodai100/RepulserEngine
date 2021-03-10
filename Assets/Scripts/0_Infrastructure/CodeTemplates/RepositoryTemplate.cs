namespace ProjectBlue.CodeGenerator
{

    public class RepositoryTemplate : CodeTemplateBase
    {
        public override string FolderPath => "Data/Repository/";
        public override string FileName => $"{className}Repository.cs";

        public RepositoryTemplate(string nameSpaceName, string className) : base(nameSpaceName, className){}
        
        protected override string Template =>  @"

using #NAME_SPACE#.Data.DataStore;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Data.Repository
{

    public class #CLASS_NAME#Repository : I#CLASS_NAME#Repository 
    {

        I#CLASS_NAME#DataStore dataStore;

        public #CLASS_NAME#Repository(I#CLASS_NAME#DataStore dataStore)
        {
            this.dataStore = dataStore;
        }

    }
}
";


    }
    
}