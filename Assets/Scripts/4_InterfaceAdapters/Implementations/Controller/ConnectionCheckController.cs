using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class ConnectionCheckController : IConnectionCheckController, IDisposable
    {
        private IConnectionCheckUseCase connectionCheckUseCase;
        private CompositeDisposable disposable = new CompositeDisposable();

        private IEnumerable<EndpointSettingViewModel> endpointSettingList;

        public ConnectionCheckController(IConnectionCheckUseCase connectionCheckUseCase,
            IEndPointSettingUseCase endpointSettingUseCase)
        {
            this.connectionCheckUseCase = connectionCheckUseCase;

            endpointSettingUseCase.OnListRecreatedAsObservable.Subscribe(list => { endpointSettingList = list; })
                .AddTo(disposable);
        }

        public Task<bool> Check(int endpointId)
        {
            var list = endpointSettingList.ToList();

            if (endpointId < list.Count)
            {
                return connectionCheckUseCase.Check(list[endpointId].EndPoint.Address);
            }

            throw new IndexOutOfRangeException($"Invalid index was specified {endpointId}");
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}