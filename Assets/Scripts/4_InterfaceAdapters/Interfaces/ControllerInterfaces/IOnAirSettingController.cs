using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IOnAirSettingController
    {
        OnAirSettingViewModel OnAirSettingViewModel { get; }
        void Save();
    }
}