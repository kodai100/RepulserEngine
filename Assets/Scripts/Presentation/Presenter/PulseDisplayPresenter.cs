using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseDisplayPresenter : IPulseDisplayPresenter
    {

        private IOverlayView overlayView;
        
        public PulseDisplayPresenter(IOverlayView overlayView)
        {
            this.overlayView = overlayView;
        }
        
        public void Trigger()
        {
            overlayView.Trigger();
        }

    }

}

