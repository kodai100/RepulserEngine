using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{
    public interface IObsWebsocketSettingDataStore
    {
        void Save(ObsWebsocketSettingDataModel data);
        ObsWebsocketSettingDataModel Load();
    }
}