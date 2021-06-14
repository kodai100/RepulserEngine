using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class CommandButtonListView : MonoBehaviour
    {
        [Inject] private DiContainer container;
        [Inject] private ICommandSettingListController commandSettingController;

        [SerializeField] private CommandButtonView commandButtonPrefab;
        [SerializeField] private Transform containerTransform;

        private List<CommandButtonView> list = new List<CommandButtonView>();

        private void Start()
        {
            commandSettingController.OnListChangedAsObservable.Subscribe(ReGenerateButtons).AddTo(this);
        }

        private void ReGenerateButtons(IEnumerable<CommandSetting> commands)
        {
            list.ForEach(button => { Destroy(button.gameObject); });

            list.Clear();

            foreach (var commandSetting in commands)
            {
                var obj = container.InstantiatePrefab(commandButtonPrefab, containerTransform);
                var buttonView = obj.GetComponent<CommandButtonView>();
                buttonView.Initialize(commandSetting.CommandName, commandSetting);
                list.Add(buttonView);
            }
        }
    }
}