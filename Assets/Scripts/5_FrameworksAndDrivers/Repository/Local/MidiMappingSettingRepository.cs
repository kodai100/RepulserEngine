using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class MidiMappingSettingRepository : IMidiMappingSettingRepository
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "MidiMappingSetting.json");

        private List<MidiMappingSettingDataModel> endpointList = new List<MidiMappingSettingDataModel>(); // cache

        private bool loaded;

        public void Save(IEnumerable<MidiMappingSettingDataModel> endpointSettings)
        {
            var target = new MidiMappingSettingListForSerialize(endpointSettings);

            var json = JsonUtility.ToJson(target);

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

            endpointList = endpointSettings.ToList();

            Debug.Log($"Saved : {JsonFilePath}");
        }


        public IEnumerable<MidiMappingSettingDataModel> Load()
        {
            if (loaded) return endpointList;

            var jsonDeserializedData = new MidiMappingSettingListForSerialize();

            try
            {
                using (var fs = new FileStream(JsonFilePath, FileMode.Open))
                using (var sr = new StreamReader(fs))
                {
                    var result = sr.ReadToEnd();

                    jsonDeserializedData = JsonUtility.FromJson<MidiMappingSettingListForSerialize>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            endpointList = jsonDeserializedData.Data.ToList();

            loaded = true;

            return jsonDeserializedData.Data;
        }
    }

    public class MidiMappingSettingRepositoryInstaller : Installer<MidiMappingSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiMappingSettingRepository>().AsSingle();
        }
    }
}