using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointSettingListPresenter : IEndPointListPresenter
    {

        private IEndPointSettingUseCase endPointSettingUseCase;

        public IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable =>
            endPointSettingUseCase.OnDataChangedAsObservable;

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