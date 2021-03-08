using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ISendCommandPresenter
    {
        void Send(CommandSetting command);
    }

}