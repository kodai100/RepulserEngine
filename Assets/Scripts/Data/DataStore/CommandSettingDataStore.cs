using System;
using System.Collections.Generic;
using System.IO;
using ProjectBlue.RepulserEngine.Domain.Model;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{

    [Serializable]
    public class CommandSettingListForSerialize
    {
        public List<CommandSetting> Data = new List<CommandSetting>();

        public CommandSettingListForSerialize(){}
        
        public CommandSettingListForSerialize(IEnumerable<CommandSetting> data)
        {
            
            Data.Clear();
            
            foreach (var component in data)
            {
                Data.Add(component);
            }
        }
    }
    

    public class CommandSettingDataStore : ICommandSettingDataStore
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "CommandSetting.json");
        
        public CommandSettingDataStore() { }

        public void Save(IEnumerable<CommandSetting> CommandSettings)
        {

            var target = new CommandSettingListForSerialize(CommandSettings);

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
        
        
        public IEnumerable<CommandSetting> Load()
        {

            var jsonDeserializedData = new CommandSettingListForSerialize();

            try 
            {
                using (var fs = new FileStream (JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader (fs)) 
                {
                    var result = sr.ReadToEnd ();
                    
                    jsonDeserializedData =  JsonUtility.FromJson<CommandSettingListForSerialize>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log (e);
            }
            
            return jsonDeserializedData.Data;
        }

    }
    
}
