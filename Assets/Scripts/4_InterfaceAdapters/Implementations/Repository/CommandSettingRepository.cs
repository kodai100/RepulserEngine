using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class CommandSettingRepository : ICommandSettingRepository
    {

        private ICommandSettingDataStore commandSettingDataStore;

        public IObservable<IEnumerable<CommandSetting>> OnDataChangedAsObservable =>
            commandSettingDataStore.OnDataChangedAsObservable;
        
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

