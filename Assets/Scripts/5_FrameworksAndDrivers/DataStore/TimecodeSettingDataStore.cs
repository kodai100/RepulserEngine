using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public class TimecodeSettingDataStore : ITimecodeSettingDataStore
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "TimecodeSetting.json");
        
        private List<TimecodeSetting> endpointList = new List<TimecodeSetting>();

        private Subject<IEnumerable<TimecodeSetting>> onDataChangedSubject = new Subject<IEnumerable<TimecodeSetting>>();
        public IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable => onDataChangedSubject;

        private bool loaded;
        
        public void Save(IEnumerable<TimecodeSetting> endpointSettings)
        {

            var target = new TimecodeSettingListForSerialize(endpointSettings);

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
        
        
        public IEnumerable<TimecodeSetting> Load()
        {

            if (loaded) return endpointList;
            
            var jsonDeserializedData = new TimecodeSettingListForSerialize();

            try 
            {
                using (var fs = new FileStream (JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader (fs)) 
                {
                    var result = sr.ReadToEnd ();
                    
                    jsonDeserializedData =  JsonUtility.FromJson<TimecodeSettingListForSerialize>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log (e);
            }
            
            endpointList = jsonDeserializedData.Data.ToList();

            loaded = true;
            
            return jsonDeserializedData.Data;
        }

    }
    
}
