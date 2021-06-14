using System;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class SignalTriggerController : IDisposable, ISignalTriggerController
    {
        public IObservable<CommandSetting> OnTriggerAsObservable { get; }

        private IEndPointSettingUseCase endPointSettingUseCase;
        private ISendToEndpointUseCase sendToEndpointUseCase;
        private ICommandSettingUseCase commandSettingUseCase;
        private IOnAirSettingUseCase onAirSettingUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        private IOverlayUseCase overlayUseCase;

        public SignalTriggerController(
            ITimecodeEvaluationUseCase timecodeEvaluationUseCase,
            IEndPointSettingUseCase endPointSettingUseCase,
            ISendToEndpointUseCase sendToEndpointUseCase,
            ICommandSettingUseCase commandSettingUseCase,
            IOnAirSettingUseCase onAirSettingUseCase,
            ICommandTriggerUseCase commandTriggerUseCase,
            IOverlayUseCase overlayUseCase)
        {
            this.endPointSettingUseCase = endPointSettingUseCase;
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.commandSettingUseCase = commandSettingUseCase;
            this.onAirSettingUseCase = onAirSettingUseCase;

            this.overlayUseCase = overlayUseCase;

            timecodeEvaluationUseCase.OnTriggerPulsedAsObservable.Subscribe(Send).AddTo(disposable);

            commandTriggerUseCase.OnCommandTriggeredAsObservable.Subscribe(Send).AddTo(disposable);
        }

        public void Send(string command)
        {
            if (!onAirSettingUseCase.OnAirSettingViewModel.IsOnAir)
            {
                Debug.Log($"Current not on air. but this command triggered : {command}");
                return;
            }

            // TODO: CommandSettingUseCaseにGetCurrentを実装する
            var commandData = commandSettingUseCase.Load()
                .FirstOrDefault(element => element.CommandName == command);

            var endPoints = endPointSettingUseCase.GetCurrent();
            foreach (var endPoint in endPoints)
            {
                // 設定で有効じゃない場合はスキップ
                if (!endPoint.ConnectionEnabled) continue;

                var ipEndPoint = endPoint.EndPoint;

                if (commandData != null)
                {
                    sendToEndpointUseCase.Send(ipEndPoint, commandData.CommandName, commandData.CommandArguments,
                        commandData.CommandType);

                    overlayUseCase.Trigger(Color.red);
                }
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}