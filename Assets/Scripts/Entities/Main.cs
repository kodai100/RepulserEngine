
using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using UniRx;

namespace ProjectBlue.RepulserEngine.Entity
{

    public class Main : IDisposable
    {

        private CompositeDisposable disposable = new CompositeDisposable();

        private SendToEndpointUseCase sendToEndpointUseCase;
        
        public Main(TimecodeEvaluationUseCase timecodeEvaluationUseCase, SendToEndpointUseCase sendToEndpointUseCase, PulseSettingUseCase pulseSettingUseCase, CommandSettingUseCase commandSettingUseCase)
        {

            this.sendToEndpointUseCase = sendToEndpointUseCase;
            
            timecodeEvaluationUseCase.OnSendMessageAsObservable.Subscribe(Send).AddTo(disposable);

            pulseSettingUseCase.OnSendTriggeredAsObservable.Subscribe(Send).AddTo(disposable);

            commandSettingUseCase.OnSendCommandAsObservable.Subscribe(Send).AddTo(disposable);
        }

        private void Send(OscMessage message)
        {
            if (string.IsNullOrEmpty(message.OverrideIp))
            {
                sendToEndpointUseCase.Send(message.OscAddress, message.OscData);
            }
            else
            {
                sendToEndpointUseCase.SendToSpecificIP(message.OverrideIp, message.OscAddress, message.OscData);
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
    
}