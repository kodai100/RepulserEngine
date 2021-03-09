using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class CommandSettingUseCase : ICommandSettingUseCase
    {

        private ICommandSettingRepository commandSettingRepository;
        
        public CommandSettingUseCase(ICommandSettingRepository commandSettingRepository)
        {
            this.commandSettingRepository = commandSettingRepository;
        }

        public IEnumerable<CommandSetting> Load()
        {
            return commandSettingRepository.Load();
        }
        
        public void Save(IEnumerable<CommandSetting> settings)
        {
            commandSettingRepository.Save(settings);
        }
    }
    
}