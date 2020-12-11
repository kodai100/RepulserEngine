using System;
using Ltc;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class SignalPulserPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView>, ISignalPulserPresenter
    {

        [SerializeField] private TimecodeIndicator timecodeIndicator;

        private Subject<Message> onSendSubject = new Subject<Message>();
        public IObservable<Message> OnSendAsObservable => onSendSubject;

        private Timecode prevTimecode;

        protected override string SaveHash => "Pulser";

        private void Update()
        {
            
            ComponentList.ForEach(pulse =>
            {
                pulse.Evaluate(timecodeIndicator.CurrentTimecode, message => { onSendSubject.OnNext(message); });
            });
            
        }

        private void OnDestroy()
        {
            onSendSubject.Dispose();
        }

    }

}