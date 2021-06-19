using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class OnAirSettingController : IOnAirSettingController
    {
        private IOnAirSettingUseCase onAirSettingUseCase;

        public OnAirSettingViewModel OnAirSettingViewModel => onAirSettingUseCase.OnAirSettingViewModel;

        public OnAirSettingController(IOnAirSettingUseCase onAirSettingUseCase)
        {
            this.onAirSettingUseCase = onAirSettingUseCase;
        }

        public void Save()
        {
            onAirSettingUseCase.Save();
        }
    }
}