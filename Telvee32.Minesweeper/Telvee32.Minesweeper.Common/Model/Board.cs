using System;
using System.Collections.Generic;

namespace Telvee32.Minesweeper.Common.Model
{
    public class Board
    {
        /*
         * A list of (x,y) coordinates of tiles
         * relative to a particular tile X.
         * 
         * t t t
         * t X t
         * t t t
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

        public Tile[,] Tiles { get; private set; }

        public int XLength { get; private set; }
        public int YLength { get; private set; }

        public int Flags { get; set; }

        public void Build(int xLength, int yLength, int bombs)
        {            
            XLength = xLength;
            YLength = yLength;

            // initialise Tiles
            Tiles = new Tile[XLength, YLength];

            for(int i = 0; i < XLength; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    Tiles[i, j] = new Tile
                    {
                        XPosition = i,
                        YPosition = j
                    };
                }
            }

            for (int i = 0; i < XLength; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    SetNeighbours(Tiles[i, j]);
                }
            }

            AssignBombs(bombs);
        }

        public void Build(int xLength, int yLength, double bombChance)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates and populates the list of neighbours for a Tile.
        /// Assumes Tiles matrix has been fully populated.
        /// </summary>
        /// <param name="tile">A Tile</param>
        private void SetNeighbours(Tile tile)
        {
            tile.Neighbours = new List<Tile>();

            for(int i = 0; i < _relativePositions.Length; i++)
            {
                var dx = _relativePositions[i]; // all even-numbered values of i are x co-ordinates
                var dy = _relativePositions[++i]; // all odd-numbered values of i are y co-ordinates

                var nx = tile.XPosition + dx; // absolute x co-ordinate of current neighbouring Tile
                var ny = tile.YPosition + dy; // absolute y co-ordinate of current neighbouring Tile

                if(IsValidIndex(nx, ny))
                {
                    tile.Neighbours.Add(Tiles[nx, ny]);
                }
            }
        }

        public void FlagTile(int x, int y, bool flag)
        {
            // TODO - validate
            var tile = Tiles[x, y];

            if (flag != tile.HasFlag)
            {
                if (flag)
                {
                    Flags--;
                }
                else
                {
                    Flags++;
                }
            }

            tile.HasFlag = flag;
        }

        public void OpenTile(int x, int y)
        {
            if (IsValidIndex(x, y))
            {
                try
                {
                    Tiles[x, y].Open();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        private bool IsValidIndex(int x, int y)
        {
            return x >= 0 && x < XLength &&
                y >= 0 && y < YLength;
        }

        private void AssignBombs(int count)
        {
            if (count >= Tiles.Length)
                throw new ArgumentException($"Count {count} cannot be greater than or equal to total tiles {Tiles.Length}");

            Random rnd = new Random();

            Flags = count;

            while(count > 0)
            {
                // get random tile, if has bomb then try again and again
                Tile tile;
                do
                {
                    tile = Tiles[rnd.Next(0, XLength), rnd.Next(0, YLength)];
                }
                while (tile.HasBomb);

                tile.HasBomb = true;

                count--;
            }
        }
    }
}
