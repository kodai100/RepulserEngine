using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{

    [Serializable]
    public class OnAirSettingDataModel
    {

        [SerializeField] private bool isOnAir;

        public bool IsOnAir => isOnAir;

        public OnAirSettingDataModel(bool isOnAir)
        {
            this.isOnAir = isOnAir;
        }
        
        public OnAirSettingDataModel():this(false){}
    }

}