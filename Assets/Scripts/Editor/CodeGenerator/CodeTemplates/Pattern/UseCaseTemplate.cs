namespace ProjectBlue.CodeGenerator
{
    public class UseCaseTemplate : CodeTemplateBase
    {
        public override string FileName => $"{className}UseCase.cs";

        public UseCaseTemplate(string className, string folderPath, string nameSpaceName) : base(className, folderPath,
            nameSpaceName) { }

        protected override string Template => @"

using #NAME_SPACE#.Repository;
using Zenject;
using UniRx;

namespace #NAME_SPACE#.Domain.UseCase
{

    public class #CLASS_NAME#UseCase : I#CLASS_NAME#UseCase
    {

        I#CLASS_NAME#Repository repository;

        public #CLASS_NAME#UseCase(I#CLASS_NAME#Repository repository)
        {
            this.repository = repository;
        }
    }

    public class #CLASS_NAME#UseCaseInstaller : Installer<#CLASS_NAME#UseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<#CLASS_NAME#UseCase>().AsSingle();
        }
    }
}
";
    }
}
