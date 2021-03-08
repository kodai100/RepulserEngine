﻿using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.View;
using UnityEngine;
using Zenject;

public class TimecodeSettingListView : ReorderableListView<TimecodeSettingView, TimecodeSetting>
{

    [Inject] private ITimecodeSettingListPresenter timecodeSettingListPresenter;
    
    protected override void OnSaveButtonClicked(IEnumerable<TimecodeSetting> items)
    {
        timecodeSettingListPresenter.Save(items);
        Debug.Log("Clicked");
    }

    protected override void StartInternal()
    {
        SetData(timecodeSettingListPresenter.Load());
    }
}