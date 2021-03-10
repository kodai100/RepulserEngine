using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Infrastructure;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class SignalSendAvailabilityButtonsView : MonoBehaviour
    {
        
        [Inject] private IEndPointListPresenter endPointListPresenter;
        
        [Inject] private DiContainer container;

        [SerializeField] private SignalSendAvailabilityButton signalSendAvailabilityButtonPrefab;

        private List<SignalSendAvailabilityButton> buttons = new List<SignalSendAvailabilityButton>();
        
        private void Start()
        {
            endPointListPresenter.OnListRecreatedAsObservable.Subscribe(GenerateButtons).AddTo(this);
        }

        private void GenerateButtons(IEnumerable<EndpointSettingViewModel> list)
        {
            ClearButtons();
            
            foreach (var (element, i) in list.WithIndex())
            {
                var obj = container.InstantiatePrefab(signalSendAvailabilityButtonPrefab, transform);
                var button = obj.GetComponent<SignalSendAvailabilityButton>();
                button.SetIndex(i);
                button.SetEndPointViewModel(element);
                buttons.Add(button);
            }
            
        }

        private void ClearButtons()
        {
            buttons.ForEach(button =>
            {
                Destroy(button.gameObject);
            });
            buttons.Clear();
        }
    }

}