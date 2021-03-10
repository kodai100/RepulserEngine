using System;
using System.Linq;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    
    public class Trigger : IInitializable, IDisposable
    {
        private ITimecodeEvaluationUseCase timecodeEvaluationUseCase;
        private IEndPointSettingUseCase endPointSettingUseCase;
        private ISendToEndpointUseCase sendToEndpointUseCase;
        private ICommandSettingUseCase commandSettingUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        public Trigger(
            ITimecodeEvaluationUseCase timecodeEvaluationUseCase,
            IEndPointSettingUseCase endPointSettingUseCase,
            ISendToEndpointUseCase sendToEndpointUseCase,
            ICommandSettingUseCase commandSettingUseCase)
        {
            this.timecodeEvaluationUseCase = timecodeEvaluationUseCase;
            this.endPointSettingUseCase = endPointSettingUseCase;
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.commandSettingUseCase = commandSettingUseCase;
        }
        
        
        public void Initialize()
        {

            timecodeEvaluationUseCase.OnTriggerPulsedAsObservable.Subscribe(SendCommandGlobal).AddTo(disposable);

        }

        private void SendCommandGlobal(string command)
        {

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

