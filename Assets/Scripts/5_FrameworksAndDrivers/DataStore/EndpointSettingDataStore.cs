using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public class EndpointSettingDataStore : IEndpointSettingDataStore
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "EndpointSetting.json");

        private List<EndpointSettingDataModel> endpointList = new List<EndpointSettingDataModel>(); // cache

        private bool loaded;

        public void Save(IEnumerable<EndpointSettingDataModel> endpointSettings)
        {
            var target = new EndpointSettingListForSerialize(endpointSettings);

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


        public IEnumerable<EndpointSettingDataModel> Load()
        {
            if (loaded) return endpointList;

            var jsonDeserializedData = new EndpointSettingListForSerialize();

            try
            {
                using (var fs = new FileStream(JsonFilePath, FileMode.Open))
                using (var sr = new StreamReader(fs))
                {
                    var result = sr.ReadToEnd();

                    jsonDeserializedData = JsonUtility.FromJson<EndpointSettingListForSerialize>(result);
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
}