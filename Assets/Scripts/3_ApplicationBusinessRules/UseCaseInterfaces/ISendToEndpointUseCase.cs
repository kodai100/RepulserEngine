using System.Net;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{

    public interface ISendToEndpointUseCase
    {
        void Send(IPEndPoint endPoint, string command, string argument, CommandType commandType);
    }

}