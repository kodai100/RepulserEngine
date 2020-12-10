using System;
using Ltc;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class SignalPulserPresenter : ListPresenter<PulseSettingPresenter, PulseSettingView>, ISignalPulserPresenter
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
                Evaluate(pulse, timecodeIndicator.CurrentTimecode);
            });
            
        }

        private void Evaluate(PulseSettingPresenter pulseSettingPresenter, Timecode timecode)
        {
            if (pulseSettingPresenter == null || prevTimecode == timecode) return;

            if (timecode < pulseSettingPresenter.PulseSetting.Timecode)
            {
                pulseSettingPresenter.AlreadyPulsed = false;
                pulseSettingPresenter.SetBefore();
            }
            
            if (timecode == pulseSettingPresenter.PulseSetting.Timecode)
            {
                Pulse(pulseSettingPresenter);
            }
            
            if (pulseSettingPresenter.PulseSetting.Timecode < timecode)
            {
                pulseSettingPresenter.SetAfter();
            }

            prevTimecode = timecode;
        }
        
        private void Pulse(PulseSettingPresenter pulseSettingPresenter)
        {
            if (pulseSettingPresenter.AlreadyPulsed || pulseSettingPresenter.PulseSetting == null) return;

            var message = new Message
            {
                OscAddress = pulseSettingPresenter.PulseSetting.OscAddress,
                OscData = pulseSettingPresenter.PulseSetting.OscData
            };
            
            onSendSubject.OnNext(message);

            pulseSettingPresenter.AlreadyPulsed = true;
        }
        

    }

}