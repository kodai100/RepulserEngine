namespace ProjectBlue.CodeGenerator
{
    public class DataStoreTemplate : CodeTemplateBase
    {
        public override string FileName => $"{className}DataStore.cs";

        public DataStoreTemplate(string nameSpaceName, string folderPath, string className) : base(nameSpaceName,
            folderPath, className) { }

        protected override string Template => @"

using Zenject;
using UniRx;

namespace #NAME_SPACE#.Data.DataStore
{

    public class #CLASS_NAME#DataStore : I#CLASS_NAME#DataStore
    {

    }

    public class #CLASS_NAME#DataStoreInstaller : Installer<#CLASS_NAME#DataStoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<#CLASS_NAME#DataStore>().AsSingle();
        }
    }
}
";
    }
}
