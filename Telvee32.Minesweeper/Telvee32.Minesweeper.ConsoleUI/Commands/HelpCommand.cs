using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class HelpCommand : Command
    {
        public CommandType RequiredCommand { get; set; }

        public HelpCommand()
        {
            Type = CommandType.Help;
        }

        public override Dictionary<string, object> GetRequiredArguments()
        {
            return new Dictionary<string, object>
            {
                { CommandKeys.CommandName, string.Empty }
            };
        }

        public override void Build(Dictionary<string, object> args)
        {
            RequiredCommand = (CommandType)args[CommandKeys.CommandName];
        }
    }
}
