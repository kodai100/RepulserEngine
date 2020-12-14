using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface IPulseSettingPresenter
    {
        PulseSetting PulseSetting { get; }
        IObservable<Unit> OnSendButtonClickedAsObservable { get; }
    }
    
}