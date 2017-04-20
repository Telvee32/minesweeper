using System.Collections.Generic;

namespace Telvee32.Minesweeper.Common.Model
{
    public class Tile
    {
        public bool HasBomb { get; set; }
        public bool HasFlag { get; set; }
        public List<Tile> Neighbours { get; set; }
    }
}
