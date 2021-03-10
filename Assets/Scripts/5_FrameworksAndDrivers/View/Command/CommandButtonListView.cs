using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class CommandButtonListView : MonoBehaviour
    {

        [Inject] private DiContainer container;
        [Inject] private ICommandSettingListPresenter commandSettingPresenter;

        [SerializeField] private CommandButtonView commandButtonPrefab;
        [SerializeField] private Transform containerTransform; 
        
        private List<CommandButtonView> list = new List<CommandButtonView>();
        
        private void Start()
        {

            commandSettingPresenter.OnListChangedAsObservable.Subscribe(ReGenerateButtons).AddTo(this);

        }

        private void ReGenerateButtons(IEnumerable<CommandSetting> commands)
        {
            list.ForEach(button =>
            {
                Destroy(button.gameObject);
            });
            
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