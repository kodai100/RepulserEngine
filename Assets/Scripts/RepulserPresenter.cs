using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RepulserPresenter : MonoBehaviour
{

    [SerializeField] private RepulserView _repulserView;
    [SerializeField] private TimecodeIndicator _timecodeIndicator;

    private List<PulseSettingPresenter> pulseList = new List<PulseSettingPresenter>();
    private int pulseNum = 0;
    
    private void Start()
    {
        
        Load();

        _repulserView.OnAddButtonClickedAsObservable.Subscribe(_ =>
        {
            pulseList.Add(_repulserView.AddPulser());
            pulseNum = pulseList.Count;
        }).AddTo(this);

        _repulserView.OnSaveButtonClickedAsObservable.Subscribe(_ =>
        {
            Save();
        }).AddTo(this);

    }

    private void Update()
    {
        
        pulseList.ForEach(pulse =>
        {
            pulse.Evaluate(_timecodeIndicator.CurrentTimecode);
        });
        
    }

    private void Load()
    {
        pulseNum = PlayerPrefs.GetInt("PulseNum", 0);
        
        for (var i = 0; i < pulseNum; i++)
        {
            var pulsePresenter = _repulserView.AddPulser();
            pulsePresenter.Load(i);
            pulseList.Add(pulsePresenter);
        }
    }

    private void Save()
    {
        for (var i = 0; i < pulseList.Count; i++)
        {
            pulseList[i].Save(i);
        }
        
        PlayerPrefs.SetInt("PulseNum", pulseNum);
    }
    
    
}
