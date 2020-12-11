using System;
using System.Collections.Generic;
using System.Linq;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class SignalPulserPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView>, ISignalPulserPresenter
    {

        [SerializeField] private TimecodeDecoderPresenter timecodeDecoderPresenter;

        private Subject<OscMessage> onSendSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendAsObservable => onSendSubject;

        private Timecode prevTimecode;

        protected override string SaveHash => "Pulser";

        public IEnumerable<PulseSetting> PulseSettingList
            => ComponentList.Select(presenter => presenter.PulseSetting);

        private void Update()
        {
            
            ComponentList.ForEach(pulse =>
            {
                pulse.Evaluate(timecodeDecoderPresenter.CurrentTimecode, message => { onSendSubject.OnNext(message); });
            });
            
        }

        private void OnDestroy()
        {
            onSendSubject.Dispose();
        }

    }

}