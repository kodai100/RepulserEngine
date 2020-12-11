using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        IEnumerable<EndpointSetting> EndpointSettingList { get; }
    }

    public interface ISignalPulserPresenter
    {
        IEnumerable<PulseSetting> PulseSettingList { get; }
        IObservable<OscMessage> OnSendAsObservable { get; }
    }
}