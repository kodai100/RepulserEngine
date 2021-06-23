using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class MidiMappingSettingRepository : IMidiMappingSettingRepository
    {
        private List<MidiMappingSettingDataModel> mappingList = new List<MidiMappingSettingDataModel>(); // cache

        private bool enabled;

        private bool loaded;

        public void Save(bool enabled, IEnumerable<MidiMappingSettingDataModel> endpointSettings)
        {
            var target = new MidiMappingSettingListForSerialize(enabled, endpointSettings);

            FileIOUtility.Write(target, "MidiMappingSetting");

            mappingList = endpointSettings.ToList();
            this.enabled = enabled;
        }


        public (bool, IEnumerable<MidiMappingSettingDataModel>) Load()
        {
            if (loaded) return (enabled, mappingList);

            var data = FileIOUtility.Read<MidiMappingSettingListForSerialize>("MidiMappingSetting");

            mappingList = data.Data.ToList();

            loaded = true;

            return (data.Enabled, data.Data);
        }
    }

    public class MidiMappingSettingRepositoryInstaller : Installer<MidiMappingSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiMappingSettingRepository>().AsSingle();
        }
    }
}