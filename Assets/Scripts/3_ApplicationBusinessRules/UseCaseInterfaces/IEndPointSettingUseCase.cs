using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IEndPointSettingUseCase
    {
        void Save(IEnumerable<EndpointSetting> settings);
        IEnumerable<EndpointSetting> Load();
    }

}