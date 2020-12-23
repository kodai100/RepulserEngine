using System;
using System.Collections.Generic;

namespace ProjectBlue.RepulserEngine.View
{
    public interface IListView<T>
    {
        IObservable<IEnumerable<T>> OnSaveAsObservable { get; }
        void SetData(IEnumerable<T> settingList);
    }
}