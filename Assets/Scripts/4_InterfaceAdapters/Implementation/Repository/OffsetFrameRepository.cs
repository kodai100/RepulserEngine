

using ProjectBlue.RepulserEngine.Data.DataStore;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Repository
{

    public class OffsetFrameRepository : IOffsetFrameRepository 
    {

        IOffsetFrameDataStore dataStore;

        public OffsetFrameRepository(IOffsetFrameDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

    }

    public class OffsetFrameRepositoryInstaller : Installer<OffsetFrameRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OffsetFrameRepository>().AsSingle();
        }
    }
}
