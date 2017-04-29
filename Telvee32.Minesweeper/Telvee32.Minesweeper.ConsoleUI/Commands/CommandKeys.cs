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
                CommandType.Flag, "Puts a flag on a given tile.\n" +
                "Usage: flag x y\n" +
                "Parameters:\n" +
                "\tx: X-coordinate\n" +
                "\ty: Y-coordinate"
            },
            {
                CommandType.Unflag, "Removes the flag from a given tile.\n" +
                "Usage: unflag x y\n" +
                "Parameters:\n" +
                "\tx: X-coordinate\n" +
                "\ty: Y-coordinate"
            },
            {
                CommandType.Open, "Opens a tile.\n" +
                "Usage: open x y\n" +
                "Parameters:\n" +
                "\tx: X-coordinate\n" +
                "\ty: Y-coordinate"
            },
            {
                CommandType.New, "Starts a new game.\n" +
                "Usage: new x y bombs\n" +
                "Parameters:\n" +
                "\tx: X-coordinate\n" +
                "\ty: Y-coordinate\n" +
                "\tbombs: number of bombs on the board"
            },
            {
                CommandType.End, "Ends the game."
            },
            {
                CommandType.Quit, "Quits the game and closes the program."
            },
            {
                CommandType.Help, "Displays information about a command.\n" +
                "Usage: help command\n" +
                "Parameters:\n" +
                "\tcommand: Name of a command"
            },
            {
                CommandType.Status, "Displays the status of a particular tile.\n" +
                "Usage: status x y\n" +
                "Parameters:\n" +
                "\tx: X-coordinate\n" +
                "\ty: Y-coordinate"
            }
        };
    }
}
