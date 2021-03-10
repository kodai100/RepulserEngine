using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface IOnAirSettingPresenter
    {
        OnAirSettingViewModel OnAirSettingViewModel { get; }
        void Save();
    }

}