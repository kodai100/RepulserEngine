using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class
        MidiMappingSettingListView : ReorderableListView<MidiMappingSettingCellView, MidiMappingSettingViewModel>
    {
        // [Inject] private IMidiMappingSettingListController midiMappingSettingListController;

        protected override void OnSaveButtonClicked(IEnumerable<MidiMappingSettingViewModel> items)
        {
            // midiMappingSettingListController.Save(items);
        }

        protected override void OnUpdateList(IEnumerable<MidiMappingSettingViewModel> items)
        {
            // TODO: 上流に変更伝える
            // midiMappingSettingListController.Update(items);
        }

        protected override void Start()
        {
            base.Start();

            // RecreateAllItem(midiMappingSettingListController.Load());
        }
    }
}