using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Infrastructure;
using ProjectBlue.RepulserEngine.Presentation;
using UnityEngine;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.View
{
    public class ConnectionCheckButtonsView : MonoBehaviour
    {

        [Inject] private IEndPointListPresenter endPointListPresenter;
        [Inject] private DiContainer container;

        [SerializeField] private ConnectionCheckButton connectionCheckButtonPrefab;

        private List<ConnectionCheckButton> buttons = new List<ConnectionCheckButton>();
        
        private void Start()
        {
            endPointListPresenter.OnListRecreatedAsObservable.Subscribe(list =>
            {
                GenerateButtons(list);
            }).AddTo(this);
        }

        private void GenerateButtons(IEnumerable<EndpointSettingViewModel> list)
        {
            ClearButtons();
            
            foreach (var (element, i) in list.WithIndex())
            {
                var obj = container.InstantiatePrefab(connectionCheckButtonPrefab, transform);
                var button = obj.GetComponent<ConnectionCheckButton>();
                button.SetIndex(i);
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