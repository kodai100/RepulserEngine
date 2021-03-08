using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{

    public interface IPulseSettingUseCase
    {
        void Save(IEnumerable<PulseSetting> setting);
        IEnumerable<PulseSetting> Load();
    }

}