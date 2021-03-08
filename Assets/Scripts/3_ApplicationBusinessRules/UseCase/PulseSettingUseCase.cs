
using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class PulseSettingUseCase : IPulseSettingUseCase, IDisposable
    {
        public IObservable<IEnumerable<PulseSetting>> OnDataUpdatedAsObservable => onDataUpdatedSubject;
        private Subject<IEnumerable<PulseSetting>> onDataUpdatedSubject = new Subject<IEnumerable<PulseSetting>>();

        private IPulseSettingRepository pulseSettingRepository;

        public PulseSettingUseCase(IPulseSettingRepository pulseSettingRepository)
        {
            this.pulseSettingRepository = pulseSettingRepository;

        }
        
        public void Initialize()
        {
            var data = pulseSettingRepository.Load();
            
            onDataUpdatedSubject.OnNext(data);
            
        }

        public void Save(IEnumerable<PulseSetting> data)
        {
            pulseSettingRepository.Save(data);
        }

        public IEnumerable<PulseSetting> Load()
        {
            return pulseSettingRepository.Load();
        }

        public void Dispose()
        {
            onDataUpdatedSubject.Dispose();
        }
    }
    
}