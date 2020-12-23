using System;
using Ltc;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.Model
{

    public enum CommandState
    {
        Predecessor, Pulse, Successor
    }
    
    [Serializable]
    public class CommandSetting
    {
        [SerializeField] private string oscAddress;
      

        public CommandState CommandState { get; private set; } = CommandState.Predecessor;
        
        public string OscAddress => oscAddress;
        public CommandSetting(string oscAddress)
        {
            this.oscAddress = oscAddress;
        }

        public void UpdateData(string oscAddress)
        {
            this.oscAddress = oscAddress;
        }
        
        
    }

}

