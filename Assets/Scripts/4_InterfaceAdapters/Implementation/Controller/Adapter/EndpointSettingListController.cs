using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class EndpointSettingListController : IEndPointListController
    {
        private IEndPointSettingUseCase endPointSettingUseCase;

        public IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable =>
            endPointSettingUseCase.OnListRecreatedAsObservable;

        public EndpointSettingListController(IEndPointSettingUseCase endPointSettingUseCase)
        {
            this.endPointSettingUseCase = endPointSettingUseCase;
        }

        public IEnumerable<EndpointSettingViewModel> Load()
        {
            return endPointSettingUseCase.Load();
        }


        public void Update(IEnumerable<EndpointSettingViewModel> settingList)
        {
            endPointSettingUseCase.Update(settingList);
        }

        public void Save()
        {
            endPointSettingUseCase.Save();
        }
    }
}