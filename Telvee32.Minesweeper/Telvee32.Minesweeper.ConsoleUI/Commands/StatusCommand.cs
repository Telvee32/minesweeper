using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    class StatusCommand : Command
    {
        public StatusCommand()
        {
            Type = CommandType.Status;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public override void Build(Dictionary<string, object> args)
        {
            XPos = (int)args[CommandKeys.XPosition];
            YPos = (int)args[CommandKeys.YPosition];
        }

        public override Dictionary<string, object> GetRequiredArguments()
        {
            return new Dictionary<string, object>
            {
                { CommandKeys.XPosition, new int?() },
                { CommandKeys.YPosition, new int?() }
            };
        }
    }
}
