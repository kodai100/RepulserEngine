using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IGlobalFrameOffsetSettingUseCase
    {
        IObservable<GlobalFrameOffset> OnDataSavedAsObservable { get; }
        IObservable<GlobalFrameOffset> OnDataUpdatedAsObservable { get; }

        /// <summary>
        /// Save to file
        /// </summary>
        /// <param name="frameOffset"></param>
        void Save();

        /// <summary>
        /// update value internal event hook
        /// </summary>
        /// <param name="frameOffset"></param>
        void Update(GlobalFrameOffset frameOffset);

        GlobalFrameOffset Load();
    }
}