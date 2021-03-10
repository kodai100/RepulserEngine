using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Translators;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class EndpointSettingUseCase : IEndPointSettingUseCase
    {
        
        private IEndpointSettingRepository endpointSettingRepository;

        private IEnumerable<EndpointSettingViewModel> viewModelList;

        public IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable => subj;
        private Subject<IEnumerable<EndpointSettingViewModel>> subj = new Subject<IEnumerable<EndpointSettingViewModel>>();
        
        public EndpointSettingUseCase(IEndpointSettingRepository endpointSettingRepository)
        {
            this.endpointSettingRepository = endpointSettingRepository;
        }

        public IEnumerable<EndpointSettingViewModel> Load()
        {
            var list = endpointSettingRepository.Load();
            viewModelList = list.Select(EndpointSettingTranslator.Translate);
            subj.OnNext(viewModelList);
            return viewModelList;
        }

        public IEnumerable<EndpointSettingViewModel> GetCurrent()
        {
            return viewModelList;
        }

        public void Update(IEnumerable<EndpointSettingViewModel> list)
        {
            viewModelList = list;
            subj.OnNext(viewModelList);
        }
        
        public void Save()
        {
            endpointSettingRepository.Save(viewModelList.Select(EndpointSettingTranslator.Translate));
        }
        
    }
    
}