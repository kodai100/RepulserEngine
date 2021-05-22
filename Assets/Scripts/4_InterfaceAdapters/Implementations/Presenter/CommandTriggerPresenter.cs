using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.ViewInterfaces;
using System;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class CommandTriggerPresenter : ICommandTriggerPresenter
    {
        public IObservable<CommandSetting> OnTriggerAsObservable => onTriggerSubject;
        private Subject<CommandSetting> onTriggerSubject = new Subject<CommandSetting>();

        private IOverlayView overlayView;
        
        public CommandTriggerPresenter(IOverlayView overlayView)
        {
            this.overlayView = overlayView;
        }
        
        public void Send(CommandSetting command)
        {
            overlayView.Trigger();

            onTriggerSubject.OnNext(command);
        }
    }

}