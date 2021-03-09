using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
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

        public IEnumerable<EndpointSettingViewModel> Load()
        {
            return endPointSettingUseCase.Load();
        }
        
        public void Save(IEnumerable<EndpointSettingViewModel> settingList)
        {
            endPointSettingUseCase.Save(settingList);
        }
    }
}