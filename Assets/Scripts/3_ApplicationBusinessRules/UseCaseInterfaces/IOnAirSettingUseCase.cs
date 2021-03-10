using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IOnAirSettingUseCase
    {
        OnAirSettingViewModel OnAirSettingViewModel { get; }
        void Save();
        
    }

}