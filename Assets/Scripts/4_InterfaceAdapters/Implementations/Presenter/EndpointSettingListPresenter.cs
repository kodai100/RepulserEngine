using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointSettingListPresenter : IEndPointListPresenter
    {

        private IEndPointSettingUseCase endPointSettingUseCase;
        
        public IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable =>
            endPointSettingUseCase.OnListRecreatedAsObservable;

        public EndpointSettingListPresenter(IEndPointSettingUseCase endPointSettingUseCase)
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