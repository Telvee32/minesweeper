using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public class EndCommand : Command
    {
        public EndCommand()
        {
            Type = CommandType.End;
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
