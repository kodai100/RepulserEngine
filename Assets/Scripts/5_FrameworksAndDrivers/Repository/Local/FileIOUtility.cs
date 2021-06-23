using System;
using System.IO;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class FileIOUtility
    {
        private static readonly string Directory = UnityEngine.Application.persistentDataPath;

        public static T Read<T>(string fileName) where T : new()
        {
            var jsonDeserializedData = new T();

            try
            {
                using var fs = new FileStream($"{Directory}/{fileName}.json", FileMode.Open);
                using var sr = new StreamReader(fs);

                var result = sr.ReadToEnd();

                jsonDeserializedData = JsonUtility.FromJson<T>(result);
            }
            catch (Exception e)
            {
                Debug.Log($"Load file error : {fileName}, {e.Message}");
            }

            return jsonDeserializedData;
        }

        public static void Write<T>(T serializeData, string fileName) where T : class
        {
            var json = JsonUtility.ToJson(serializeData);

            using var sw = new StreamWriter($"{Directory}/{fileName}.json", false);

            try
            {
                sw.Write(json);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}