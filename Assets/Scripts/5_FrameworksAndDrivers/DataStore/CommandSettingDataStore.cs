using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;
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
    

    public class CommandSettingDataStore : ICommandSettingDataStore, IDisposable
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "CommandSetting.json");
        
        private List<CommandSetting> commandList = new List<CommandSetting>();

        private bool loaded;
        
        private Subject<IEnumerable<CommandSetting>> onDataChangedSubject = new Subject<IEnumerable<CommandSetting>>();
        public IObservable<IEnumerable<CommandSetting>> OnDataChangedAsObservable => onDataChangedSubject;

        public void Dispose()
        {
            onDataChangedSubject.Dispose();
        }

        public void Save(IEnumerable<CommandSetting> commandSettings)
        {

            onDataChangedSubject.OnNext(commandSettings);
            
            var target = new CommandSettingListForSerialize(commandSettings);

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

            commandList = commandSettings.ToList();
            
            Debug.Log($"Saved : {JsonFilePath}");
        }
        
        
        public IEnumerable<CommandSetting> Load()
        {

            if (loaded) return commandList;

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

            loaded = true;
            
            return jsonDeserializedData.Data;
        }

    }
    
}
