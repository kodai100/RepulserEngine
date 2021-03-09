using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Infrastructure;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using Zenject;

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
            endPointListPresenter.OnDataChangedAsObservable.Subscribe(list =>
            {
                GenerateButtons(list);
            }).AddTo(this);
        }

        private void GenerateButtons(IEnumerable<EndpointSettingDataModel> list)
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