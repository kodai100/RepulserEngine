using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ICommandTriggerPresenter
    {
        void Send(CommandSetting command);
    }

}