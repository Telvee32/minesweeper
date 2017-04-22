using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public abstract class Command
    {
        public CommandType Type { get; set; }

        public string Description => CommandKeys.CommandDescriptions[Type];

        public abstract Dictionary<string, object> GetRequiredArguments();

        public abstract void Build(Dictionary<string, object> args);
    }
}
