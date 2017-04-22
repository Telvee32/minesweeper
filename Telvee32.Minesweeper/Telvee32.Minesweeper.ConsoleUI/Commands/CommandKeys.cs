using System.Collections.Generic;

namespace Telvee32.Minesweeper.ConsoleUI.Commands
{
    public static class CommandKeys
    {
        public static string XPosition = "XPosition";
        public static string YPosition = "YPosition";

        public static string Open = "Open";

        public static string XLength = "XLength";
        public static string YLength = "YLength";
        public static string Bombs = "Bombs";

        public static string CommandName = "CommandName";

        public static Dictionary<CommandType, string> CommandDescriptions = new Dictionary<CommandType, string>
        {
            {
                CommandType.Flag, "Flag. X, Y."
            },
            {
                CommandType.Unflag, "Unflag. X, Y."
            },
            {
                CommandType.Open, "Open. X, Y."
            },
            {
                CommandType.New, "New. X, Y, Bombs."
            },
            {
                CommandType.End, "End,"
            },
            {
                CommandType.Quit, "Quit."
            },
            {
                CommandType.Help, "Help. CommandName."
            }
        };
    }
}
