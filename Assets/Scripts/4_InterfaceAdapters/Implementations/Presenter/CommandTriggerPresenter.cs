using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.ViewInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class CommandTriggerPresenter : ICommandTriggerPresenter
    {
        private IOverlayView overlayView;
        
        public CommandTriggerPresenter(IOverlayView overlayView)
        {
            this.overlayView = overlayView;
        }
        
        public void Send(CommandSetting command)
        {
            overlayView.Trigger();
        }
    }

}