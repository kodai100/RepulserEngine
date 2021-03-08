using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.ViewInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class SendCommandPresenter : ISendCommandPresenter
    {
        private IOverlayView overlayView;
        
        public SendCommandPresenter(IOverlayView overlayView)
        {
            this.overlayView = overlayView;
        }
        
        public void Send(CommandSetting command)
        {
            overlayView.Trigger();
        }
    }

}