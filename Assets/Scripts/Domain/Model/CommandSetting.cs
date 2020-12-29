using System;
using Ltc;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private string commandId;
        [SerializeField] private string command;
        [SerializeField] private string memo;

        public int CommandType => commandType;
        public string CommandId => commandId;
        public string Command => command;
        public string Memo => memo;
        
        
        public CommandSetting(int commandType, string commandId, string command, string memo)
        {
            this.commandType = commandType;
            this.commandId = commandId;
            this.command = command;
            this.memo = memo;
        }

    }

}

