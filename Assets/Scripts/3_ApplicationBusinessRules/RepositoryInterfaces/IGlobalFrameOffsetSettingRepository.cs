using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IGlobalFrameOffsetSettingRepository
    {
        IObservable<GlobalFrameOffset> OnDataSavedAsObservable { get; }
        void Save(GlobalFrameOffset frameOffset);
        GlobalFrameOffset Load();
    }
}