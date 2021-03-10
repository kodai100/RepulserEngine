using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IEndPointSettingUseCase
    {
        void Update(IEnumerable<EndpointSettingViewModel> list);
        void Save();
        IEnumerable<EndpointSettingViewModel> GetCurrent();
        IEnumerable<EndpointSettingViewModel> Load();
        public IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable { get; }
    }

}