using System;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class CommandTriggerUseCase : ICommandTriggerUseCase, IDisposable
    {
        private ICommandTriggerPresenter commandTriggerPresenter;

        public IObservable<string> OnCommandTriggeredAsObservable => onCommandTriggeredSubject;
        
        private Subject<string> onCommandTriggeredSubject = new Subject<string>();

        private CompositeDisposable disposable = new CompositeDisposable();

        public CommandTriggerUseCase(ICommandTriggerPresenter commandTriggerPresenter)
        {
            this.commandTriggerPresenter = commandTriggerPresenter;

            commandTriggerPresenter.OnTriggerAsObservable
                .Subscribe(x => {
                    SendCommand(x.CommandName);
                })
                .AddTo(disposable);
        }

        public void SendCommand(string commandName)
        {
            onCommandTriggeredSubject.OnNext(commandName);
        }

        public void Dispose()
        {
            disposable.Dispose();
            onCommandTriggeredSubject.Dispose();
        }
    }

}