using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class UnflagCommand : Command
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public UnflagCommand()
        {
            Type = CommandType.Unflag;
        }

        public override Dictionary<string, object> GetRequiredArguments()
        {
            return new Dictionary<string, object>
            {
                { CommandKeys.XPosition, new int?() },
                { CommandKeys.YPosition, new int?() }
            };
        }

        public override void Build(Dictionary<string, object> args)
        {
            XPos = (int)args[CommandKeys.XPosition];
            YPos = (int)args[CommandKeys.YPosition];
        }
    }
}
