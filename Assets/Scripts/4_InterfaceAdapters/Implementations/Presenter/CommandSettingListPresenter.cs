using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class CommandSettingListPresenter : ICommandSettingListPresenter, IDisposable
    {
        
        private ICommandSettingUseCase commandSettingUseCase;

        public IObservable<IEnumerable<CommandSetting>> OnListChangedAsObservable => onListChangedSubject;
        
        private Subject<IEnumerable<CommandSetting>> onListChangedSubject = new Subject<IEnumerable<CommandSetting>>();
        
        public CommandSettingListPresenter(ICommandSettingUseCase commandSettingUseCase)
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