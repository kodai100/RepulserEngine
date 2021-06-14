using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IGlobalFrameOffsetSettingController
    {
        IObservable<GlobalFrameOffset> OnDataSavedAsObservable { get; }
        IObservable<GlobalFrameOffset> OnDataUpdatedAsObservable { get; }
        void Save();
        void Update(GlobalFrameOffset frameOffset);
        GlobalFrameOffset Load();
    }
}