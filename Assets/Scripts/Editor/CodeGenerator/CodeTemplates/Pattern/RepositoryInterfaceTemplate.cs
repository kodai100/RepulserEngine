namespace ProjectBlue.CodeGenerator
{
    public class RepositoryInterfaceTemplate : CodeTemplateBase
    {
        public override string FileName => $"I{className}Repository.cs";

        public RepositoryInterfaceTemplate(string nameSpaceName, string folderPath, string className) : base(
            nameSpaceName, folderPath, className) { }

        protected override string Template => @"


namespace #NAME_SPACE#.Repository
{

    public interface I#CLASS_NAME#Repository
    {
    }

}
";
    }
}
