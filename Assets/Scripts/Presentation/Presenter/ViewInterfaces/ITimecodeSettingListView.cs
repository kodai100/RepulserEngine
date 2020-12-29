using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.View
{
    public interface ITimecodeSettingListView<T> : IListView<T>
    {
        void OnUpdateCommandData(IEnumerable<CommandSetting> commands);
    }

}

