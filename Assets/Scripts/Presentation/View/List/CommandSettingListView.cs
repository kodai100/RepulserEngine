using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.View
{

    public class CommandSettingListView : ReorderableListView<CommandSettingView, CommandSetting>, ICommandSettingListView<CommandSetting>
    {
        public IObservable<IEnumerable<CommandSetting>> OnSaveAsObservable => onSaveSubject;
    
        private Subject<IEnumerable<CommandSetting>> onSaveSubject = new Subject<IEnumerable<CommandSetting>>();
        
    }
    
}