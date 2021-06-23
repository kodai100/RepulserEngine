using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class EndpointSettingRepository : IEndpointSettingRepository
    {
        private List<EndpointSettingDataModel> endpointList = new List<EndpointSettingDataModel>(); // cache

        private bool loaded;

        public void Save(IEnumerable<EndpointSettingDataModel> endpointSettings)
        {
            var target = new EndpointSettingListForSerialize(endpointSettings);

            FileIOUtility.Write(target, "EndpointSetting");

            endpointList = endpointSettings.ToList();
        }


        public IEnumerable<EndpointSettingDataModel> Load()
        {
            if (loaded) return endpointList;

            var data = FileIOUtility.Read<EndpointSettingListForSerialize>("EndpointSetting");

            endpointList = data.Data.ToList();

            loaded = true;

            return data.Data;
        }
    }
}