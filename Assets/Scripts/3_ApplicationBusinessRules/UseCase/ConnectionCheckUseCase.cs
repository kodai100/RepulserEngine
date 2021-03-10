using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class ConnectionCheckUseCase : IConnectionCheckUseCase, IDisposable
    {
        private IConnectionCheckRepository connectionCheckRepository;

        private CompositeDisposable disposable = new CompositeDisposable();

        private IEnumerable<EndpointSettingViewModel> endpointSettingList;
        
        public ConnectionCheckUseCase(
            IConnectionCheckRepository connectionCheckRepository,
            IEndPointListPresenter endPointListPresenter)
        {
            this.connectionCheckRepository = connectionCheckRepository;

            endPointListPresenter.OnListRecreatedAsObservable.Subscribe(list =>
            {
                endpointSettingList = list;
            }).AddTo(disposable);
        }
        
        public Task<bool> Check(int endpointId)
        {

            var list = endpointSettingList.ToList();

            if (endpointId < list.Count)
            {
                return connectionCheckRepository.Check(list[endpointId].EndPoint.Address);
            }
            
            throw new IndexOutOfRangeException($"Invalid index was specified {endpointId}");
            
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }

}