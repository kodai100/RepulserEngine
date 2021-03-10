using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class OnAirSettingPresenter : IOnAirSettingPresenter
    {
        private IOnAirSettingUseCase onAirSettingUseCase;
        
        public OnAirSettingViewModel OnAirSettingViewModel => onAirSettingUseCase.OnAirSettingViewModel;

        public OnAirSettingPresenter(IOnAirSettingUseCase onAirSettingUseCase)
        {
            this.onAirSettingUseCase = onAirSettingUseCase;
        }

        public void Save()
        {
            onAirSettingUseCase.Save();
        }
    }

}