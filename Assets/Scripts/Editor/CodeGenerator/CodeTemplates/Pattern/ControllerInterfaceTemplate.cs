namespace ProjectBlue.CodeGenerator
{
    public class ControllerInterfaceTemplate : CodeTemplateBase
    {
        public override string FileName => $"I{className}Controller.cs";

        public ControllerInterfaceTemplate(string nameSpaceName, string folderPath, string className) : base(
            nameSpaceName, folderPath, className) { }

        protected override string Template => @"

namespace #NAME_SPACE#.Controllers
{

    public interface I#CLASS_NAME#Controller
    {
    }

}
";
    }
}
