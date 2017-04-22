using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class QuitCommand : Command
    {
        public QuitCommand()
        {
            Type = CommandType.Quit;
        }

        public override void Build(Dictionary<string, object> args)
        {
            
        }

        public override Dictionary<string, object> GetRequiredArguments()
        {
            return new Dictionary<string, object>();
        }
    }
}
