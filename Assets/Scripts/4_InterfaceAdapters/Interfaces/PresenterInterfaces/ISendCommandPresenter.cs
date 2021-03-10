using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ISendCommandPresenter
    {
        void Send(CommandSetting command);
    }

}