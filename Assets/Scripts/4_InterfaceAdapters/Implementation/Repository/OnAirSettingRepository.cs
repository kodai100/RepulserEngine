using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    
    public class OnAirSettingRepository : IOnAirSettingRepository
    {

        private IOnAirSettingDataStore onAirSettingDataStore;

        public OnAirSettingRepository(IOnAirSettingDataStore onAirSettingDataStore)
        {
            this.onAirSettingDataStore = onAirSettingDataStore;
        }

        public void Save(OnAirSettingDataModel onAirSettingDataModel)
        {
            onAirSettingDataStore.Save(onAirSettingDataModel);
        }

        OnAirSettingDataModel IOnAirSettingRepository.Load()
        {
            return onAirSettingDataStore.Load();
        }

    }

}

