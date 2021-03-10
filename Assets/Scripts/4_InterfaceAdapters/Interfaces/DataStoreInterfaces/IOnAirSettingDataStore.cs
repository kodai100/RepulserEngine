using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IOnAirSettingDataStore
    {
        void Save(OnAirSettingDataModel onAirSettingDataModel);
        OnAirSettingDataModel Load();
    }

}