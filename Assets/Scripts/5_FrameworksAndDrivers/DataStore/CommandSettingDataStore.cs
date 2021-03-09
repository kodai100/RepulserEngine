using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{
    
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
            var enumerable = commandSettings as CommandSetting[] ?? commandSettings.ToArray();
            
            onDataChangedSubject.OnNext(enumerable);
            
            var target = new CommandSettingListForSerialize(enumerable);

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

            commandList = enumerable.ToList();
            
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

            commandList = jsonDeserializedData.Data.ToList();
            
            loaded = true;
            
            return jsonDeserializedData.Data;
        }

    }
    
}
