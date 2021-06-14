

using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{

    public class OffsetFrameDataStore : IOffsetFrameDataStore
    {

    }

    public class OffsetFrameDataStoreInstaller : Installer<OffsetFrameDataStoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OffsetFrameDataStore>().AsSingle();
        }
    }
}
