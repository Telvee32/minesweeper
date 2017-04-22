using System;
using System.Linq;

namespace Telvee32.Minesweeper.Common.Model
{
    public class Board
    {
        /*
         * A list of (x,y) coordinates of tiles
         * relative to a particular tile X.
         * 
         * ttt
         * tXt
         * ttt
         * 
         */
        private int[] _relativePositions = new int[]
        {
            // x,y
            -1, -1, // top left
            -1, 0, // middle left
            -1, 1, // bottom left
            0, -1, // top middle
            0, 1, // bottom middle
            1, -1, // top right
            1, 0, // middle right
            1, 1 // bottom right
        };

        private Tile[][] _tiles { get; set; }

        private int _totalTiles => _tiles.Length * _tiles[0].Length;

        public int Flags { get; set; }

        public void Build()
        {
            // initialise Tiles
        }

        /// <summary>
        /// Calculates and populates the list of neighbours for a Tile.
        /// Assumes Tiles matrix has been fully populated.
        /// </summary>
        /// <param name="x">x position</param>
        public void SetNeighbours(Tile tile)
        {
            for(int i = 0; i < _relativePositions.Length; i++)
            {
                var dx = _relativePositions[i]; // all even-numbered values of i are x co-ordinates
                var dy = _relativePositions[++i]; // all odd-numbered values of i are y co-ordinates

                var nx = tile.XPosition + dx; // absolute x co-ordinate of current neighbouring Tile
                var ny = tile.YPosition + dy; // absolute y co-ordinate of current neighbouring Tile

                if(IsValidIndex(tile.XPosition, tile.YPosition))
                {
                    tile.Neighbours.Add(_tiles[nx][ny]);
                }
            }
        }

        public void FlagTile(int x, int y, bool flag)
        {

        }

        public void OpenTile(int x, int y)
        {

        }

        private bool IsValidIndex(int x, int y)
        {
            return x >= 0 && x < _tiles.Length &&
                y >= 0 && y < _tiles[0].Length;
        }

        private void AssignBombs(int count)
        {
            if (count > _totalTiles)
                throw new ArgumentException($"Count {count} cannot be greater than total tiles {_totalTiles}");

            Random rnd = new Random();

            Flags = count;

            while(count > 0)
            {
                // get random tile, if has bomb then try again and again
                Tile tile;
                do
                {
                    tile = _tiles[rnd.Next(0, _tiles.Length)][rnd.Next(0, _tiles[0].Length)];
                }
                while (tile.HasBomb);

                tile.HasBomb = true;

                count--;
            }
        }
    }
}
