using System;
using System.IO;
using ProjectBlue.RepulserEngine.Domain.Entity;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class GlobalFrameOffsetSettingRepository : IGlobalFrameOffsetSettingRepository, IDisposable
    {
        private bool loaded;

        private GlobalFrameOffset cache;

        private Subject<GlobalFrameOffset> onDataChangedSubject = new Subject<GlobalFrameOffset>();
        public IObservable<GlobalFrameOffset> OnDataSavedAsObservable => onDataChangedSubject;

        public void Dispose()
        {
            onDataChangedSubject.Dispose();
        }

        public void Save(GlobalFrameOffset frameOffset)
        {
            onDataChangedSubject.OnNext(frameOffset);

            FileIOUtility.Write(frameOffset, "OffsetFrame");

            cache = frameOffset;
        }


        public GlobalFrameOffset Load()
        {
            if (loaded) return cache;

            var data = FileIOUtility.Read<GlobalFrameOffset>("OffsetFrame");

            loaded = true;

            cache = data;

            return data;
        }
    }

    public class GlobalFrameOffsetSettingRepositoryInstaller : Installer<GlobalFrameOffsetSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalFrameOffsetSettingRepository>().AsSingle();
        }
    }
}