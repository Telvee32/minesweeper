namespace Telvee32.Minesweeper.Common.Communication.Response
{
    public class TileStatusResponse : BaseResponse
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool HasBomb { get; set; }
        public bool HasFlag { get; set; }
        public bool IsOpen { get; set; }
        public int Bombs { get; set; }
    }
}
