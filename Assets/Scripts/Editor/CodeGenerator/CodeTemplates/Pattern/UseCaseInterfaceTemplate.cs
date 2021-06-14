namespace ProjectBlue.CodeGenerator
{
    public class UseCaseInterfaceTemplate : CodeTemplateBase
    {
        public override string FileName => $"I{className}UseCase.cs";

        public UseCaseInterfaceTemplate(string nameSpaceName, string folderPath, string className) : base(nameSpaceName,
            folderPath, className) { }

        protected override string Template => @"


namespace #NAME_SPACE#.Domain.UseCase
{

    public interface I#CLASS_NAME#UseCase
    {
    }

}
";
    }
}
