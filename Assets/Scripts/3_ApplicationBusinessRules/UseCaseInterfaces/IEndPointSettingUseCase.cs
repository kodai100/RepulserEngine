using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IEndPointSettingUseCase
    {
        void Save(IEnumerable<EndpointSettingViewModel> settings);
        IEnumerable<EndpointSettingViewModel> Load();
    }

}