using System.Collections.Generic;
using System.Linq;
using Telvee32.Minesweeper.Common.Exceptions;

namespace Telvee32.Minesweeper.Common.Model
{
    public class Tile
    {
        public bool HasBomb { get; set; }
        public bool HasFlag { get; set; }
        public bool IsOpen { get; set; }
        public List<Tile> Neighbours { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string Label
        {
            get
            {
                if (IsOpen)
                {
                    return Bombs == 0 ? "" : Bombs.ToString();
                }
                else
                {
                    return HasFlag ? "F" : "";
                }
            }
        }

        public int Bombs => Neighbours.Where(t => t.HasBomb).Count();

        public void Flag()
        {

        }

        /// <summary>
        /// Opens this Tile.
        /// </summary>
        public void Open()
        {
            if (IsOpen) return;
            IsOpen = true;

            if (HasBomb)
            {
                // FAIL
                throw new BombException(XPosition, YPosition);
            }

            /*
             * If opening a tile with no explosive neighbours,
             * recursively opens all neighbours with no bombs.
             */ 
            if(Bombs == 0)
            {
                Neighbours.ForEach(t =>
                {
                    t.Open();
                });
            }
        }

        /// <summary>
        /// Opens this Tile's neighbours, and maybe their neighbours.
        /// </summary>
        public void OpenNeighbours()
        {
            var flags = Neighbours.Where(t => t.HasFlag).Count();
         
            if(flags == Bombs)
            {
                foreach(var neighbour in Neighbours)
                {
                    if (!neighbour.IsOpen && !neighbour.HasFlag)
                    {
                        neighbour.Open();
                    }
                }
            }
        }
    }
}
