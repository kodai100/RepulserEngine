using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class EndpointSettingUseCase : IEndPointSettingUseCase, IDisposable
    {
        
        private IEndpointSettingRepository endpointSettingRepository;

        public IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable => onDataChangedSubject;
        
        private Subject<IEnumerable<EndpointSetting>> onDataChangedSubject = new Subject<IEnumerable<EndpointSetting>>();
        
        public EndpointSettingUseCase(IEndpointSettingRepository endpointSettingRepository)
        {
            this.endpointSettingRepository = endpointSettingRepository;
        }

        public IEnumerable<EndpointSetting> Load()
        {
            var list = endpointSettingRepository.Load();
            var endpointSettings = list as EndpointSetting[] ?? list.ToArray();
            onDataChangedSubject.OnNext(endpointSettings);
            return endpointSettings;
        }
        
        public void Save(IEnumerable<EndpointSetting> settings)
        {
            var settingList = settings as EndpointSetting[] ?? settings.ToArray();
            onDataChangedSubject.OnNext(settingList);
            endpointSettingRepository.Save(settingList);
        }

        public void Dispose()
        {
            onDataChangedSubject.Dispose();
        }
    }
    
}