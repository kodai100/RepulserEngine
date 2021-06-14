namespace ProjectBlue.CodeGenerator
{
    public class DataStoreInterfaceTemplate : CodeTemplateBase
    {
        public override string FileName => $"I{className}DataStore.cs";

        public DataStoreInterfaceTemplate(string nameSpaceName, string folderPath, string className) : base(
            nameSpaceName, folderPath, className) { }

        protected override string Template => @"


namespace #NAME_SPACE#.Data.DataStore
{

    public interface I#CLASS_NAME#DataStore
    {
    }

}
";
    }
}
