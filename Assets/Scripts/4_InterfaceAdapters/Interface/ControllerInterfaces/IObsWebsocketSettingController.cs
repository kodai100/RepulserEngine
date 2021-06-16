using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IObsWebsocketSettingController
    {
        ObsWebsocketSettingViewModel ViewModel { get; }
        void Save();
        void Load();
    }
}