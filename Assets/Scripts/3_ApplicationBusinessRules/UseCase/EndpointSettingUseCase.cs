using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Translators;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class EndpointSettingUseCase : IEndPointSettingUseCase, IDisposable
    {
        
        private IEndpointSettingRepository endpointSettingRepository;

        private IEnumerable<EndpointSettingViewModel> viewModelList;
        
        public EndpointSettingUseCase(IEndpointSettingRepository endpointSettingRepository)
        {
            this.endpointSettingRepository = endpointSettingRepository;
        }

        public IEnumerable<EndpointSettingViewModel> Load()
        {
            var list = endpointSettingRepository.Load();
            viewModelList = list.Select(element => EndpointSettingTranslator.Translate(element));
            return viewModelList;
        }
        
        public void Save(IEnumerable<EndpointSettingViewModel> settings)
        {
            viewModelList = settings;
            onDataChangedSubject.OnNext(settingList);
            endpointSettingRepository.Save(settingList);
        }

        public void Dispose()
        {
            onDataChangedSubject.Dispose();
        }
    }
    
}