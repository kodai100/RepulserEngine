using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{

    [Serializable]
    public class EndpointSettingListForSerialize
    {
        public List<EndpointSetting> Data = new List<EndpointSetting>();

        public EndpointSettingListForSerialize(){}
        
        public EndpointSettingListForSerialize(IEnumerable<EndpointSetting> data)
        {
            
            Data.Clear();
            
            foreach (var component in data)
            {
                Data.Add(component);
            }
        }
    }
    

    public class EndpointSettingDataStore : IEndpointSettingDataStore
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "EndpointSetting.json");
        
        private List<EndpointSetting> endpointList;
        public IEnumerable<EndpointSetting> EndPointList => endpointList;

        public void Save(IEnumerable<EndpointSetting> endpointSettings)
        {

            var target = new EndpointSettingListForSerialize(endpointSettings);

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

            endpointList = endpointSettings.ToList();
            
            Debug.Log($"Saved : {JsonFilePath}");
        }
        
        
        public IEnumerable<EndpointSetting> Load()
        {

            var jsonDeserializedData = new EndpointSettingListForSerialize();

            try 
            {
                using (var fs = new FileStream (JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader (fs)) 
                {
                    var result = sr.ReadToEnd ();
                    
                    jsonDeserializedData =  JsonUtility.FromJson<EndpointSettingListForSerialize>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log (e);
            }
            
            endpointList = jsonDeserializedData.Data.ToList();
            
            return jsonDeserializedData.Data;
        }

    }
    
}
