using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

public class TimecodeSettingListView : ReorderableListView<TimecodeSettingView, TimecodeSetting>, ITimecodeSettingListView<TimecodeSetting>
{
    
    public void OnUpdateCommandData(IEnumerable<CommandSetting> commands)
    {
        throw new System.NotImplementedException();
    }
}