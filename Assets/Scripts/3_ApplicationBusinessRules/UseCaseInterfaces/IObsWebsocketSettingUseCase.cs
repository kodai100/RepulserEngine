using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IObsWebsocketSettingUseCase
    {
        ObsWebsocketSettingViewModel ViewModel { get; }
        void Save();
        void Load();
    }
}