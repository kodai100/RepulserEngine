using System;
using Ltc;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.Model
{

    public enum CommandType
    {
        Raw, Osc
    }
    
    [Serializable]
    public class CommandSetting
    {

        [SerializeField] private int commandType;
        [SerializeField] private string eventName;
        [SerializeField] private string command;
        [SerializeField] private string memo;

        public int CommandType => commandType;
        public string EventName => eventName;
        public string Command => command;
        public string Memo => memo;
        
        
        public CommandSetting(int commandType, string eventName, string command, string memo)
        {
            this.commandType = commandType;
            this.eventName = eventName;
            this.command = command;
            this.memo = memo;
        }

    }

}

