using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.Translators;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class MidiMappingSettingUseCase : IMidiMappingSettingUseCase
    {
        IMidiMappingSettingRepository repository;

        public MidiMappingSettingUseCase(IMidiMappingSettingRepository repository)
        {
            this.repository = repository;
        }

        private IEnumerable<MidiMappingSettingViewModel> viewModelList;
        private bool enabled;

        public IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable => subj;

        private Subject<IEnumerable<MidiMappingSettingViewModel>> subj =
            new Subject<IEnumerable<MidiMappingSettingViewModel>>();

        public (bool, IEnumerable<MidiMappingSettingViewModel>) Load()
        {
            var data = repository.Load();

            enabled = data.Item1;

            viewModelList = data.Item2.Select(MidiMappingSettingTranslator.Translate);
            subj.OnNext(viewModelList);

            return (enabled, viewModelList);
        }

        public (bool, IEnumerable<MidiMappingSettingViewModel>) GetCurrent()
        {
            return (enabled, viewModelList);
        }

        public void UpdateEnabled(bool isEnabled)
        {
            enabled = isEnabled;
        }

        public void Update(IEnumerable<MidiMappingSettingViewModel> list)
        {
            viewModelList = list;
            subj.OnNext(viewModelList);
        }

        public void Save()
        {
            repository.Save(enabled, viewModelList.Select(MidiMappingSettingTranslator.Translate));
        }
    }

    public class MidiMappingSettingUseCaseInstaller : Installer<MidiMappingSettingUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiMappingSettingUseCase>().AsSingle();
        }
    }
}