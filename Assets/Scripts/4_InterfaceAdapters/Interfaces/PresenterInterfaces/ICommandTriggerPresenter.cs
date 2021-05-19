using ProjectBlue.RepulserEngine.Domain.DataModel;
using System;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ICommandTriggerPresenter
    {
        IObservable<CommandSetting> OnTriggerAsObservable { get; }

        void Send(CommandSetting command);
    }

}