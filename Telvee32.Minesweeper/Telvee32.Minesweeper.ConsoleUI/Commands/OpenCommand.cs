using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class OpenCommand : Command
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public OpenCommand()
        {
            Type = CommandType.Open;
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
