
using System.Collections.Generic;
using Ltc;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class TimecodeDisplayPresenter : ITimecodeDisplayPresenter
    {
        private IEnumerable<ITimecodeDisplayView> timecodeDisplayViews;
        
        public TimecodeDisplayPresenter(IEnumerable<ITimecodeDisplayView> timecodeDisplayViews)
        {

            this.timecodeDisplayViews = timecodeDisplayViews;

        }


        public void UpdateTimecode(Timecode timecode)
        {
            foreach (var timecodeDisplayView in timecodeDisplayViews)
            {
                timecodeDisplayView.UpdateTimecode(timecode);
            }
        }
    }
    
}