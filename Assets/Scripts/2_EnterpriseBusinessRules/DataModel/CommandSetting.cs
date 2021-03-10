using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{
    
    public enum CommandType
    {
        Osc, Raw
    }
    
    [Serializable]
    public class CommandSetting
    {

        [SerializeField] private CommandType commandType;
        [SerializeField] private string commandName;
        [SerializeField] private string commandArguments;
        [SerializeField] private string memo;

        public CommandType CommandType => commandType;
        public string CommandName => commandName;
        public string CommandArguments => commandArguments;
        public string Memo => memo;
        
        
        public CommandSetting(CommandType commandType, string commandName, string commandArguments, string memo)
        {
            this.commandType = commandType;
            this.commandName = commandName;
            this.commandArguments = commandArguments;
            this.memo = memo;
        }

        public CommandSetting() : this(CommandType.Osc, "NULL", "", "")
        {
        }

    }

}

