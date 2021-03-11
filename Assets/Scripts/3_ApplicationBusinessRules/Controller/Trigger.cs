using System;
using System.Linq;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    
    public class Trigger : IDisposable
    {
        private IEndPointSettingUseCase endPointSettingUseCase;
        private ISendToEndpointUseCase sendToEndpointUseCase;
        private ICommandSettingUseCase commandSettingUseCase;
        private IOnAirSettingUseCase onAirSettingUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        public Trigger(
            ITimecodeEvaluationUseCase timecodeEvaluationUseCase,
            IEndPointSettingUseCase endPointSettingUseCase,
            ISendToEndpointUseCase sendToEndpointUseCase,
            ICommandSettingUseCase commandSettingUseCase,
            IOnAirSettingUseCase onAirSettingUseCase,
            ICommandTriggerUseCase commandTriggerUseCase)
        {
            this.endPointSettingUseCase = endPointSettingUseCase;
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.commandSettingUseCase = commandSettingUseCase;
            this.onAirSettingUseCase = onAirSettingUseCase;

            timecodeEvaluationUseCase.OnTriggerPulsedAsObservable.Subscribe(SendCommandGlobal).AddTo(disposable);

            commandTriggerUseCase.OnCommandTriggeredAsObservable.Subscribe(SendCommandGlobal).AddTo(disposable);
        }

        private void SendCommandGlobal(string command)
        {

            if (!onAirSettingUseCase.OnAirSettingViewModel.IsOnAir)
            {
                Debug.Log($"Current not on air. but this command triggered : {command}");
                return;
            }

            // TODO: CommandSettingUseCaseにGetCurrentを実装する
            var commandData = commandSettingUseCase.Load().FirstOrDefault(element => element.CommandName == command);

            var endPoints = endPointSettingUseCase.GetCurrent();
            foreach (var endPoint in endPoints)
            {
                
                // 設定で有効じゃない場合はスキップ
                if(!endPoint.ConnectionEnabled) continue;

                var ipEndPoint = endPoint.EndPoint;
                
                if (commandData != null)
                {
                    sendToEndpointUseCase.Send(ipEndPoint, commandData.CommandName, commandData.CommandArguments, commandData.CommandType);
                }
                
            }
        }
        
        public void Dispose()
        {
            disposable.Dispose();
        }
    }

}

