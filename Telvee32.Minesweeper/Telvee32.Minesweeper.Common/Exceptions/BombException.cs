using System;

namespace Telvee32.Minesweeper.Common.Exceptions
{
    public class BombException : Exception
    {
        public BombException(int x, int y) : base($"Bomb opened at ({x}, {y}).")
        {            
        }
    }
}
