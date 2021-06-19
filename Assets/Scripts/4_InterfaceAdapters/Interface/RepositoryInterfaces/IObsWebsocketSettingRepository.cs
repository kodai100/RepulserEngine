using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IObsWebsocketSettingRepository
    {
        void Save(ObsWebsocketSettingDataModel data);
        ObsWebsocketSettingDataModel Load();
    }
}