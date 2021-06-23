using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class OnAirSettingRepository : IOnAirSettingRepository
    {
        private OnAirSettingDataModel onAirSettingDataModel;

        private bool loaded;

        public void Save(OnAirSettingDataModel onAirSettingDataModel)
        {
            FileIOUtility.Write(onAirSettingDataModel, "OnAirSetting");

            this.onAirSettingDataModel = onAirSettingDataModel;
        }

        public OnAirSettingDataModel Load()
        {
            if (loaded) return onAirSettingDataModel;

            var data = FileIOUtility.Read<OnAirSettingDataModel>("OnAirSetting");

            onAirSettingDataModel = data;

            loaded = true;
            return onAirSettingDataModel;
        }
    }
}