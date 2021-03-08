using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingListPresenter : IPulseSettingListPresenter
    {
        private IPulseSettingUseCase pulseSettingUseCase;

        public PulseSettingListPresenter(IPulseSettingUseCase pulseSettingUseCase)
        {
            this.pulseSettingUseCase = pulseSettingUseCase;
        }
        
        public void Save(IEnumerable<PulseSetting> settingList)
        {
            pulseSettingUseCase.Save(settingList);
        }

        public IEnumerable<PulseSetting> Load()
        {
            return pulseSettingUseCase.Load();
        }
    }

}