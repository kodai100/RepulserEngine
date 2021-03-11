

using System;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface ICommandTriggerUseCase
    {
        IObservable<string> OnCommandTriggeredAsObservable { get; }
        void SendCommand(string commandName);
    }
}