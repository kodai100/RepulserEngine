using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class TimecodeSettingRepository : ITimecodeSettingRepository
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "TimecodeSetting.json");

        private List<TimecodeSetting> endpointList = new List<TimecodeSetting>();

        private Subject<IEnumerable<TimecodeSetting>>
            onDataChangedSubject = new Subject<IEnumerable<TimecodeSetting>>();

        public IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable => onDataChangedSubject;

        private bool loaded;

        public void Save(IEnumerable<TimecodeSetting> endpointSettings)
        {
            var target = new TimecodeSettingListForSerialize(endpointSettings);

            FileIOUtility.Write(target, "TimecodeSetting");

            endpointList = endpointSettings.ToList();

            Debug.Log($"Saved : {JsonFilePath}");
        }


        public IEnumerable<TimecodeSetting> Load()
        {
            if (loaded) return endpointList;

            var data = FileIOUtility.Read<TimecodeSettingListForSerialize>("TimecodeSetting");

            endpointList = data.Data.ToList();

            loaded = true;

            return data.Data;
        }
    }
}