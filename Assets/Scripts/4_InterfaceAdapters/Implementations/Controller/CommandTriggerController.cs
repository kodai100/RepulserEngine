using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class CommandTriggerController : ICommandTriggerController
    {
        private ICommandTriggerUseCase commandTriggerUseCase;

        public CommandTriggerController(ICommandTriggerUseCase commandTriggerUseCase)
        {
            this.commandTriggerUseCase = commandTriggerUseCase;
        }

        public void Send(CommandSetting command)
        {
            commandTriggerUseCase.SendCommand(command.CommandName);
        }
    }
}