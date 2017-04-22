using Telvee32.Minesweeper.Common.Model;

namespace Telvee32.Minesweeper.Common
{
    public class GameState
    {
        public Board Board { get; private set; }

        public void NewGame(int xLength, int yLength, int bombs)
        {
            Board = new Board();
            Board.Build();
        }

        public void QuitGame()
        {

        }

        public void EndGame()
        {

        }
    }

    public enum EndGameReason
    {
        Win, Fail, Quit
    }
}
