using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class CommandSettingRepository : ICommandSettingRepository
    {

        private ICommandSettingDataStore commandSettingDataStore;
        
        public CommandSettingRepository(ICommandSettingDataStore commandSettingDataStore)
        {
            this.commandSettingDataStore = commandSettingDataStore;
        }

        public void Save(IEnumerable<CommandSetting> commandSettingList)
        {
            commandSettingDataStore.Save(commandSettingList);
        }

        public IEnumerable<CommandSetting> Load()
        {
            return commandSettingDataStore.Load();
        }
    }

}

