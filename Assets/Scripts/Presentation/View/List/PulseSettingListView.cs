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
        
        protected override void StartInternal()
        {
            // Saveボタンが押されたときに、すべてのリスト要素の内容をバリデーションして、通ったらセーブするために上流に流す。
            saveButton.OnClickAsObservable().Subscribe(_ =>
            {
                // TODO validate
                // if(validate == false) return;

                onSaveSubject.OnNext(ReorderedComponentList.Select(component =>component.CreateData()));
            
                foreach (var component in ReorderedComponentList)
                {
                    component.SetBackgroundSaved();
                }
                
                // TODO bind send button

            }).AddTo(this);
        }
        
        public void UpdateTimecode(Timecode timecode)
        {
            throw new NotImplementedException();
        }


    }
    
}