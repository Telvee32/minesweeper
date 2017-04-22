using Telvee32.Minesweeper.Common.Communication;
using Telvee32.Minesweeper.Common.Exceptions;
using Telvee32.Minesweeper.Common.Model;

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
        }

        public void FlagTile(int x, int y, bool flag)
        {
            Board.FlagTile(x, y, flag);
        }
    }

    public enum GameStatus
    {
        Win, Fail, Ok
    }
}
