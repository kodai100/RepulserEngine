using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface ICommandSettingUseCase
    {
        void Save(IEnumerable<CommandSetting> settings);
        IEnumerable<CommandSetting> Load();
    }

}