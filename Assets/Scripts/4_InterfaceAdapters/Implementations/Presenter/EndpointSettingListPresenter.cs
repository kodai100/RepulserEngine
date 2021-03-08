using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointSettingListPresenter : IEndPointListPresenter
    {

        private IEndPointSettingUseCase endPointSettingUseCase;

        public EndpointSettingListPresenter(IEndPointSettingUseCase endPointSettingUseCase)
        {
            this.endPointSettingUseCase = endPointSettingUseCase;
        }

        public IEnumerable<EndpointSetting> Load()
        {
            return endPointSettingUseCase.Load();
        }
        
        public void Save(IEnumerable<EndpointSetting> settingList)
        {
            endPointSettingUseCase.Save(settingList);
        }
    }
}