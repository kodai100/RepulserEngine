namespace ProjectBlue.CodeGenerator
{
    public class ControllerTemplate : CodeTemplateBase
    {
        public override string FileName => $"{className}Controller.cs";

        public ControllerTemplate(string nameSpaceName, string folderPath, string className) : base(nameSpaceName,
            folderPath, className) { }

        protected override string Template => @"

using #NAME_SPACE#.Domain.UseCase;
using Zenject;
using UniRx;

namespace #NAME_SPACE#.Controllers
{

    public class #CLASS_NAME#Controller : I#CLASS_NAME#Controller 
    {

        I#CLASS_NAME#UseCase useCase;

        public #CLASS_NAME#Controller(I#CLASS_NAME#UseCase useCase)
        {
            this.useCase = useCase;
        }

    }

    public class #CLASS_NAME#ControllerInstaller : Installer<#CLASS_NAME#ControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<#CLASS_NAME#Controller>().AsSingle();
        }
    }
}
";
    }
}
