using System;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class CommandTriggerUseCase : ICommandTriggerUseCase, IDisposable
    {

        public IObservable<string> OnCommandTriggeredAsObservable => onCommandTriggeredSubject;
        
        private Subject<string> onCommandTriggeredSubject = new Subject<string>();

        public void SendCommand(string commandName)
        {
            onCommandTriggeredSubject.OnNext(commandName);
        }

        public void Dispose()
        {
            onCommandTriggeredSubject.Dispose();
        }
    }

}