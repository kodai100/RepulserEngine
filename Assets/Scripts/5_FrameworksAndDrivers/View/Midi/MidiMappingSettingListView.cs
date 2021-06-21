using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class
        MidiMappingSettingListView : ReorderableListView<MidiMappingSettingCellView, MidiMappingSettingViewModel>
    {
        [SerializeField] private Toggle enableToggle;

        [Inject] private IMidiCommunicationController midiCommunicationController;
        [Inject] private IMidiMappingSettingController midiMappingSettingListController;

        protected override void OnSaveButtonClicked(IEnumerable<MidiMappingSettingViewModel> items)
        {
            midiMappingSettingListController.Save();
        }

        protected override void OnUpdateList(IEnumerable<MidiMappingSettingViewModel> items)
        {
            // TODO: 上流に変更伝える
            midiMappingSettingListController.Update(items);
        }

        protected override void Start()
        {
            base.Start();

            var data = midiMappingSettingListController.Load();

            // initialization
            enableToggle.isOn = data.Item1;
            ConnectionChange(enableToggle);

            RecreateAllItem(data.Item2);

            enableToggle.OnValueChangedAsObservable().Subscribe(ConnectionChange).AddTo(this);
        }

        private void ConnectionChange(bool isConnect)
        {
            if (isConnect)
            {
                midiCommunicationController.Connect();
            }
            else
            {
                midiCommunicationController.Disconnect();
            }

            midiMappingSettingListController.UpdateEnabled(isConnect);
        }
    }
}