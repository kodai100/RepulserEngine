using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{

    public interface IOnAirSettingRepository
    {
        void Save(OnAirSettingDataModel onAirSettingDataModel);
        OnAirSettingDataModel Load();
    }

}