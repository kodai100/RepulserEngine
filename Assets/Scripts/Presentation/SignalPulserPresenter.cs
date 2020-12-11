using System.Collections.Generic;
using System.Linq;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class SignalPulserPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView>, ISignalPulserPresenter
    {

        private Timecode prevTimecode;

        protected override string SaveHash => "Pulser";

        public IEnumerable<PulseSetting> PulseSettingList
            => ComponentList.Select(presenter => presenter.PulseSetting);
    }

}