using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RepulserPresenter : MonoBehaviour
{

    [SerializeField] private RepulserView _repulserView;
    [SerializeField] private TimecodeIndicator _timecodeIndicator;

    private List<PulseSettingPresenter> pulseList = new List<PulseSettingPresenter>();
    
    private void Start()
    {

        _repulserView.OnAddButtonClickedAsObservable.Subscribe(_ =>
        {

            pulseList.Add(_repulserView.AddPulser());

        }).AddTo(this);

        _repulserView.OnSaveButtonClickedAsObservable.Subscribe(_ =>
        {
            pulseList.ForEach(pulse =>
            {
                pulse.Save();
            });
        }).AddTo(this);

    }

    private void Update()
    {
        
        pulseList.ForEach(pulse =>
        {
            pulse.Evaluate(_timecodeIndicator.CurrentTimecode);
        });
        
    }
    
    
}
