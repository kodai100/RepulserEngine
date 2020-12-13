using System;

namespace ProjectBlue.RepulserEngine.Application
{
    
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            var wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.components;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] components;
        }
    }
    
}