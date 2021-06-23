using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class CommandSettingRepository : ICommandSettingRepository, IDisposable
    {
        private List<CommandSetting> commandList = new List<CommandSetting>();

        private bool loaded;

        private Subject<IEnumerable<CommandSetting>> onDataChangedSubject = new Subject<IEnumerable<CommandSetting>>();
        public IObservable<IEnumerable<CommandSetting>> OnDataChangedAsObservable => onDataChangedSubject;

        public void Dispose()
        {
            onDataChangedSubject.Dispose();
        }

        public void Save(IEnumerable<CommandSetting> commandSettings)
        {
            var enumerable = commandSettings as CommandSetting[] ?? commandSettings.ToArray();

            onDataChangedSubject.OnNext(enumerable);

            var target = new CommandSettingListForSerialize(enumerable);

            FileIOUtility.Write(target, "CommandSetting");

            commandList = enumerable.ToList();
        }


        public IEnumerable<CommandSetting> Load()
        {
            if (loaded) return commandList;

            var data = FileIOUtility.Read<CommandSettingListForSerialize>("CommandSetting");

            commandList = data.Data.ToList();

            loaded = true;

            return data.Data;
        }
    }
}