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

        public IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable => subj;

        private Subject<IEnumerable<MidiMappingSettingViewModel>> subj =
            new Subject<IEnumerable<MidiMappingSettingViewModel>>();

        public IEnumerable<MidiMappingSettingViewModel> Load()
        {
            var list = repository.Load();
            viewModelList = list.Select(MidiMappingSettingTranslator.Translate);
            subj.OnNext(viewModelList);
            return viewModelList;
        }

        public IEnumerable<MidiMappingSettingViewModel> GetCurrent()
        {
            return viewModelList;
        }

        public void Update(IEnumerable<MidiMappingSettingViewModel> list)
        {
            viewModelList = list;
            subj.OnNext(viewModelList);
        }

        public void Save()
        {
            repository.Save(viewModelList.Select(MidiMappingSettingTranslator.Translate));
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