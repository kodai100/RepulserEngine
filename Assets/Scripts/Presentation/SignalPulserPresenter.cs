using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class SignalPulserPresenter : ListPresenter<PulseSettingPresenter, PulseSettingView>
    {
    
        [SerializeField] private TimecodeIndicator timecodeIndicator;

        protected override string SaveHash => "Pulser";

        private void Update()
        {
            
            ComponentList.ForEach(pulse =>
            {
                pulse.Evaluate(timecodeIndicator.CurrentTimecode);
            });
            
        }


    }

}