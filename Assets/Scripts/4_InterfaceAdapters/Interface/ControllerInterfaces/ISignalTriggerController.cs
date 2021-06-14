using ProjectBlue.RepulserEngine.Domain.DataModel;
using System;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface ISignalTriggerController
    {
        IObservable<CommandSetting> OnTriggerAsObservable { get; }
        void Send(string command);
    }
}