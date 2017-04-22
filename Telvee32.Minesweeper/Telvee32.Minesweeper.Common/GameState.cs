using Telvee32.Minesweeper.Common.Communication;
using Telvee32.Minesweeper.Common.Exceptions;
using Telvee32.Minesweeper.Common.Model;
using System.Linq;

namespace Telvee32.Minesweeper.Common
{
    public class GameState
    {
        public Board Board { get; private set; }

        public GameStatus Status { get; private set; }

        public int Flags => Board.Flags;

        public void NewGame(NewGameRequest request)
        {
            Board = new Board();
            Status = GameStatus.Ok;
            Board.Build(request.XLength, request.YLength, request.Bombs);
        }

        public void QuitGame()
        {

        }

        public void EndGame()
        {
            Board = null;
        }

        // TODO - add tile manipulation methods

        public void OpenTile(int x, int y)
        {
            try
            {
                Board.OpenTile(x, y);
            }
            catch (BombException)
            {
                Status = GameStatus.Fail;
                throw;
            }
            CheckWin();
        }

        public void FlagTile(int x, int y, bool flag)
        {
            Board.FlagTile(x, y, flag);
            CheckWin();
        }

        private void CheckWin()
        {
            if (Flags == 0 && Board.Tiles.Cast<Tile>()
                .All(t => (t.IsOpen && !t.HasBomb) || (!t.IsOpen && t.HasBomb && t.HasFlag)))
            {
                Status = GameStatus.Win;
            }
        }
    }

    public enum GameStatus
    {
        Undefined, Win, Fail, Ok
    }
}
