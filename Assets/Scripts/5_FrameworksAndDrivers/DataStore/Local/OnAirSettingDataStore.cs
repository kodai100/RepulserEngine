using System;
using System.IO;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public class OnAirSettingDataStore : IOnAirSettingDataStore
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "OnAirSetting.json");

        private OnAirSettingDataModel onAirSettingDataModel;

        private bool loaded;

        public void Save(OnAirSettingDataModel onAirSettingDataModel)
        {
            var json = JsonUtility.ToJson(onAirSettingDataModel);

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

            this.onAirSettingDataModel = onAirSettingDataModel;

            Debug.Log($"Saved : {JsonFilePath}");
        }

        public OnAirSettingDataModel Load()
        {
            if (loaded) return onAirSettingDataModel;

            var jsonDeserializedData = new OnAirSettingDataModel();

            try
            {
                using (var fs = new FileStream(JsonFilePath, FileMode.Open))
                using (var sr = new StreamReader(fs))
                {
                    var result = sr.ReadToEnd();

                    jsonDeserializedData = JsonUtility.FromJson<OnAirSettingDataModel>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);

                onAirSettingDataModel = new OnAirSettingDataModel();
                loaded = true;
                return onAirSettingDataModel;
            }

            if (jsonDeserializedData == null)
            {
                jsonDeserializedData = new OnAirSettingDataModel();
            }

            onAirSettingDataModel = jsonDeserializedData;
            loaded = true;
            return onAirSettingDataModel;
        }
    }
}