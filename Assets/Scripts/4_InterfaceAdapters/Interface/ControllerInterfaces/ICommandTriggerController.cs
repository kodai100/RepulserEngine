using ProjectBlue.RepulserEngine.Domain.DataModel;
using System;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface ICommandTriggerController
    {
        void Send(CommandSetting command);
    }
}