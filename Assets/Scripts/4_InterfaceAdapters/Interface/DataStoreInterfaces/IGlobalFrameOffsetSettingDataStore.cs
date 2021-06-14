using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{
    public interface IGlobalFrameOffsetSettingDataStore
    {
        IObservable<GlobalFrameOffset> OnDataSavedAsObservable { get; }
        void Save(GlobalFrameOffset frameOffset);
        GlobalFrameOffset Load();
    }
}