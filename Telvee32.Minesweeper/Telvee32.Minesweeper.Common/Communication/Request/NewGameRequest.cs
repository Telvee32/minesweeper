namespace Telvee32.Minesweeper.Common.Communication.Request
{
    public class NewGameRequest
    {
        public int XLength { get; set; }
        public int YLength { get; set; }
        public int Bombs { get; set; }
        public int BombPercentage { get; set; }
        public double BombChance => BombPercentage / 100;
    }
}
