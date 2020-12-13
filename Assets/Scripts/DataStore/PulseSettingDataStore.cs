using System;
using System.Collections.Generic;
using System.IO;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{

    [Serializable]
    public class PulseSettingListForSerialize
    {
        public List<PulseSetting> Data = new List<PulseSetting>();

        public PulseSettingListForSerialize(){}
        
        public PulseSettingListForSerialize(IEnumerable<PulseSetting> data)
        {
            
            Data.Clear();
            
            foreach (var component in data)
            {
                Data.Add(component);
            }
        }
    }
    

    public class PulseSettingDataStore : IPulseSettingDataStore
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "PulseSetting.json");
        
        public PulseSettingDataStore() { }

        public void Save(IEnumerable<PulseSetting> pulseSettings)
        {

            var target = new PulseSettingListForSerialize(pulseSettings);

            var json = JsonUtility.ToJson(target);
            
            using (var sw = new StreamWriter (JsonFilePath, false)) 
            {
                try
                {
                    sw.Write (json);
                }
                catch (Exception e)
                {
                    Debug.Log (e);
                }
            }
            
            Debug.Log($"Saved : {JsonFilePath}");
        }
        
        
        public IEnumerable<PulseSetting> Load()
        {

            var jsonDeserializedData = new PulseSettingListForSerialize();

            try 
            {
                using (var fs = new FileStream (JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader (fs)) 
                {
                    var result = sr.ReadToEnd ();
                    
                    jsonDeserializedData =  JsonUtility.FromJson<PulseSettingListForSerialize>(result);
                }
            }
            catch (Exception e) //失敗した時の処理
            {
                Debug.Log (e);
            }
            
            return jsonDeserializedData.Data;
        }

    }
    
}
