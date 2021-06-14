using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class CommandSettingListController : ICommandSettingListController, IDisposable
    {
        private ICommandSettingUseCase commandSettingUseCase;

        public IObservable<IEnumerable<CommandSetting>> OnListChangedAsObservable => onListChangedSubject;

        private Subject<IEnumerable<CommandSetting>> onListChangedSubject = new Subject<IEnumerable<CommandSetting>>();

        public CommandSettingListController(ICommandSettingUseCase commandSettingUseCase)
        {
            this.commandSettingUseCase = commandSettingUseCase;
        }

        public void Save(IEnumerable<CommandSetting> settingList)
        {
            var commandSettings = settingList as CommandSetting[] ?? settingList.ToArray();
            commandSettingUseCase.Save(commandSettings);
            onListChangedSubject.OnNext(commandSettings);
        }

        public IEnumerable<CommandSetting> Load()
        {
            var list = commandSettingUseCase.Load().ToList();
            onListChangedSubject.OnNext(list);
            return list;
        }

        public void Dispose()
        {
            onListChangedSubject.Dispose();
        }
    }
}