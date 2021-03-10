using System;
using System.Net;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    
    public class SendToEndpointUseCase : IDisposable, ISendToEndpointUseCase
    {

        private CompositeDisposable _disposable = new CompositeDisposable();

        private ISenderRepository senderRepository;
        
        public SendToEndpointUseCase(ISenderRepository senderRepository)
        {
            this.senderRepository = senderRepository;
        }

        public void Send(IPEndPoint endPoint, string commandName, string commandArgument, CommandType commandType)
        {

            if (commandType == CommandType.Osc)
            {
                SendOsc(endPoint, commandName, commandArgument);
            }
            
        }


        private void SendOsc(IPEndPoint endPoint, string commandName, string commandArgument)
        {
            
            // OSCアドレスに変換
            commandName = $"/{commandName}";
            
            if (string.IsNullOrEmpty(commandArgument))
            {
                senderRepository.Send(endPoint, commandName, "null");
                return;
            }
            
            // float detection
            if (commandArgument.Contains("."))
            {
                if (float.TryParse(commandArgument, out var floaResult))
                {
                    senderRepository.Send(endPoint, commandName, floaResult);
                }
                else
                {
                    senderRepository.Send(endPoint, commandName, commandArgument);
                }

                return;
            }
            
            // int detection
            if (int.TryParse(commandArgument, out var intResult))
            {
                senderRepository.Send(endPoint, commandName, intResult);
            }
            else
            {
                senderRepository.Send(endPoint, commandName, commandArgument);
            }
            
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}

