using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Translators
{
    public class ObsWebsocketSettingTranslator
    {
        public static ObsWebsocketSettingDataModel Translate(ObsWebsocketSettingViewModel viewModel)
        {
            var dataModel = new ObsWebsocketSettingDataModel(viewModel.ServerAddress.Value, viewModel.Password.Value,
                viewModel.AutoReconnectOnStart.Value, viewModel.ChangeScene.Value, viewModel.RestartMedia.Value);
            return dataModel;
        }

        public static ObsWebsocketSettingViewModel Translate(ObsWebsocketSettingDataModel dataModel)
        {
            var viewModel = new ObsWebsocketSettingViewModel(dataModel.ServerAddress, dataModel.Password,
                dataModel.AutoReconnectOnStart, dataModel.ChangeScene, dataModel.RestartMedia);
            return viewModel;
        }
    }
}