using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class SignalPulserPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView>, ISignalPulserPresenter
    {

        [SerializeField] private TimecodeIndicator timecodeIndicator;

        private Subject<OscMessage> onSendSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendAsObservable => onSendSubject;

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