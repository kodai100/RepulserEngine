using System;
using System.Collections.Generic;
using System.Linq;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.View
{

    public class PulseSettingListView : ReorderableListView<PulseSettingView, PulseSetting>, IPulseSettingListView<PulseSetting>
    {
        
        public IObservable<int> OnSendAsObservable { get; }
        public IObservable<IEnumerable<PulseSetting>> OnSaveAsObservable => onSaveSubject;
    
        private Subject<IEnumerable<PulseSetting>> onSaveSubject = new Subject<IEnumerable<PulseSetting>>();

        public void UpdateTimecode(Timecode timecode)
        {
            throw new NotImplementedException();
        }


    }
    
}