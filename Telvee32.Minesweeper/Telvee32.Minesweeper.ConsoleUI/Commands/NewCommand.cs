using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class NewCommand : Command
    {
        public int XLength { get; set; }
        public int YLength { get; set; }
        public int Bombs { get; set; }

        public NewCommand()
        {
            Type = CommandType.New;
        }

        public override Dictionary<string, object> GetRequiredArguments()
        {
            return new Dictionary<string, object>
            {
                { CommandKeys.XLength, new int?() },
                { CommandKeys.YLength, new int?() },
                { CommandKeys.Bombs, new int?() }
            };
        }

        public override void Build(Dictionary<string, object> args)
        {
            XLength = (int)args[CommandKeys.XLength];
            YLength = (int)args[CommandKeys.YLength];
            Bombs = (int)args[CommandKeys.Bombs];
        }
    }
}
