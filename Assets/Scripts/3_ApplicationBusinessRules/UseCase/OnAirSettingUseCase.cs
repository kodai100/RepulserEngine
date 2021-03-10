using System;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.Translators;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class OnAirSettingUseCase : IOnAirSettingUseCase, IDisposable
    {
        
        private IOnAirSettingRepository onAirSettingRepository;

        public OnAirSettingViewModel OnAirSettingViewModel => onAirSettingViewModel;
        private OnAirSettingViewModel onAirSettingViewModel = new OnAirSettingViewModel();

        public OnAirSettingUseCase(IOnAirSettingRepository onAirSettingRepository)
        {
            this.onAirSettingRepository = onAirSettingRepository;
            
            Load();    // 本当は上位レイヤで呼ぶべき
        }

        public OnAirSettingViewModel Load()
        {
            var onAirSettingDataModel = onAirSettingRepository.Load();
            onAirSettingViewModel = OnAirSettingTranslator.Translate(onAirSettingDataModel);
            return onAirSettingViewModel;
        }
        
        public void Save()
        {
            onAirSettingRepository.Save(OnAirSettingTranslator.Translate(onAirSettingViewModel));
        }

        public void Dispose()
        {
            Save();
            Debug.Log("Auto Save");
        }
    }
    
}