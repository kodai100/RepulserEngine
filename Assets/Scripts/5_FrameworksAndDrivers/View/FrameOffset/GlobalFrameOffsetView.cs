using System;
using System.Collections.Generic;
using kodai100.TimeCodeCalculation;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.Entity;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class GlobalFrameOffsetView : MonoBehaviour
    {
        [Inject] private IGlobalFrameOffsetSettingController globalFrameOffsetController;

        [SerializeField] private TMP_Dropdown framerateDropdown;
        [SerializeField] private TMP_InputField offsetFrameInput;

        [SerializeField] private Image offsetFrameInputBackground;

        [SerializeField] private Button saveButton;

        private Color defaultColor;

        private FrameRateType frameRateType = FrameRateType.F_30;
        private int offset;

        private void Start()
        {
            var savedData = globalFrameOffsetController.Load();

            defaultColor = offsetFrameInputBackground.color;

            framerateDropdown.options = new List<TMP_Dropdown.OptionData>()
            {
                new TMP_Dropdown.OptionData("30"), new TMP_Dropdown.OptionData("29.97"),
                new TMP_Dropdown.OptionData("60"), new TMP_Dropdown.OptionData("59.97")
            };

            framerateDropdown.value = savedData.FrameRateType;
            offsetFrameInput.text = savedData.Offset.ToString();

            framerateDropdown.OnValueChangedAsObservable().Subscribe(value =>
            {
                frameRateType = (FrameRateType) Enum.ToObject(typeof(FrameRateType), value);

                globalFrameOffsetController.Update(new GlobalFrameOffset
                    {FrameRateType = (int) frameRateType, Offset = offset});
            }).AddTo(this);

            offsetFrameInput.OnValueChangedAsObservable().Subscribe(text =>
            {
                if (int.TryParse(text, out var result))
                {
                    offset = result;
                    offsetFrameInputBackground.color = defaultColor;

                    globalFrameOffsetController.Update(new GlobalFrameOffset
                        {FrameRateType = (int) frameRateType, Offset = offset});
                }
                else
                {
                    offsetFrameInputBackground.color = Color.red;
                }
            }).AddTo(this);

            saveButton.OnClickAsObservable().Subscribe(_ => { globalFrameOffsetController.Save(); }).AddTo(this);
        }
    }
}