namespace ProjectBlue.CodeGenerator
{
    public class RepositoryTemplate : CodeTemplateBase
    {
        public override string FileName => $"{className}Repository.cs";

        public RepositoryTemplate(string nameSpaceName, string folderPath, string className) : base(nameSpaceName,
            folderPath, className)
        {
        }

        protected override string Template => @"

using Zenject;
using UniRx;

namespace #NAME_SPACE#.Repository
{

    public class #CLASS_NAME#Repository : I#CLASS_NAME#Repository 
    {

        public #CLASS_NAME#Repository()
        {
        }

    }

    public class #CLASS_NAME#RepositoryInstaller : Installer<#CLASS_NAME#RepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<#CLASS_NAME#Repository>().AsSingle();
        }
    }
}
";
    }
}