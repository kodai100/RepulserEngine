using System;
using Ltc;

namespace ProjectBlue.RepulserEngine.View
{
    public interface IPulseSettingListView<T> : IListView<T>
    {
        IObservable<int> OnSendAsObservable { get; }
        void UpdateTimecode(Timecode timecode);
    }

}