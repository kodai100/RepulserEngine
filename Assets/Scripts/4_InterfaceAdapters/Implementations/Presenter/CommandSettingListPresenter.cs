using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class CommandSettingListPresenter : ICommandSettingListPresenter
    {
        
        private ICommandSettingUseCase commandSettingUseCase;
        
        public CommandSettingListPresenter(ICommandSettingUseCase commandSettingUseCase)
        {
            this.commandSettingUseCase = commandSettingUseCase;
        }
        
        public void Save(IEnumerable<CommandSetting> settingList)
        {
            commandSettingUseCase.Save(settingList);
        }

        public IEnumerable<CommandSetting> Load()
        {
            return commandSettingUseCase.Load();
        }
    }

}