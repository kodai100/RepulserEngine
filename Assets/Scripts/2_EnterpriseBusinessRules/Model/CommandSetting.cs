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
        [SerializeField] private string commandName;
        [SerializeField] private string commandArguments;
        [SerializeField] private string memo;

        public int CommandType => commandType;
        public string CommandName => commandName;
        public string CommandArguments => commandArguments;
        public string Memo => memo;
        
        
        public CommandSetting(int commandType, string commandName, string commandArguments, string memo)
        {
            this.commandType = commandType;
            this.commandName = commandName;
            this.commandArguments = commandArguments;
            this.memo = memo;
        }

        public CommandSetting() : this(-1, "NULL", "", "")
        {
        }

    }

}

