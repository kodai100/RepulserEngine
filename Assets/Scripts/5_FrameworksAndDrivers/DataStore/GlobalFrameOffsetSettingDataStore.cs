using System;
using System.IO;
using ProjectBlue.RepulserEngine.Domain.Entity;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{
    public class GlobalFrameOffsetSettingDataStore : IGlobalFrameOffsetSettingDataStore, IDisposable
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "OffsetFrame.json");

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

            var json = JsonUtility.ToJson(frameOffset);

            using (var sw = new StreamWriter(JsonFilePath, false))
            {
                try
                {
                    sw.Write(json);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            cache = frameOffset;

            Debug.Log($"Saved : {JsonFilePath}");
        }


        public GlobalFrameOffset Load()
        {
            if (loaded) return cache;

            var jsonDeserializedData = new GlobalFrameOffset();

            try
            {
                using (var fs = new FileStream(JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader(fs))
                {
                    var result = sr.ReadToEnd();

                    jsonDeserializedData = JsonUtility.FromJson<GlobalFrameOffset>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log("No data");
                return jsonDeserializedData;
            }

            loaded = true;

            cache = jsonDeserializedData;

            return jsonDeserializedData;
        }
    }

    public class GlobalFrameOffsetSettingDataStoreInstaller : Installer<GlobalFrameOffsetSettingDataStoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalFrameOffsetSettingDataStore>().AsSingle();
        }
    }
}