using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface ICommandSettingPresenter
    {
        CommandSetting CommandSetting { get; }
       
        IObservable<Unit> OnPauseButtonClickedAsObservable { get; }
        IObservable<Unit> OnStopButtonClickedAsObservable { get; }
        IObservable<Unit> OnBackButtonClickedAsObservable { get; }
        IObservable<Unit> OnSkipButtonClickedAsObservable { get; }

    }
    
}