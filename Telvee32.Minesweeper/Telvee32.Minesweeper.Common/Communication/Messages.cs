namespace Telvee32.Minesweeper.Common.Communication
{
    public static class Messages
    {
        public static string NoSuchTile(int x, int y)
        {
            return $"No such tile at ({x}, {y}).";
        }

        public static string FlagAlreadyOpen(int x, int y)
        {
            return $"Cannot flag tile at ({x}. {y}) as it is already open.";
        }

        public static string UnflagAlreadyOpen(int x, int y)
        {
            return $"Cannot unflag tile at ({x}. {y}) as it is already open.";
        }

        public static string OpenAlreadyFlagged(int x, int y)
        {
            return $"Cannot open tile at ({x}, {y}) as it has a flag.";
        }
    }
}
