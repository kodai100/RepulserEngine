using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class MidiMappingSettingController : IMidiMappingSettingController
    {
        IMidiMappingSettingUseCase useCase;

        public MidiMappingSettingController(IMidiMappingSettingUseCase useCase)
        {
            this.useCase = useCase;
        }

        public IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable =>
            useCase.OnListRecreatedAsObservable;

        public (bool, IEnumerable<MidiMappingSettingViewModel>) Load()
        {
            return useCase.Load();
        }

        public void UpdateEnabled(bool isEnabled)
        {
            useCase.UpdateEnabled(isEnabled);
        }

        public void Update(IEnumerable<MidiMappingSettingViewModel> settingList)
        {
            useCase.Update(settingList);
        }

        public void Save()
        {
            useCase.Save();
        }
    }

    public class MidiMappingSettingControllerInstaller : Installer<MidiMappingSettingControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiMappingSettingController>().AsSingle();
        }
    }
}