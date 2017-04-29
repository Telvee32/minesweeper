using Telvee32.Minesweeper.Common.Communication.Request;
using Telvee32.Minesweeper.Common.Exceptions;
using Telvee32.Minesweeper.Common.Model;
using System.Linq;
using Telvee32.Minesweeper.Common.Communication.Response;
using System;

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

        public OpenTileResponse OpenTile(OpenTileRequest request)
        {
            var response = new OpenTileResponse
            {
                XPosition = request.XPosition,
                YPosition = request.YPosition
            };

            try
            {
                Board.OpenTile(request.XPosition, request.YPosition);
            }
            catch (BombException e)
            {
                Status = GameStatus.Fail;
                response.Message = e.Message;
            }
            catch(Exception e)
            {
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
            }

            CheckWin();
            response.GameStatus = Status;

            return response;
        }

        public FlagTileResponse FlagTile(FlagTileRequest request)
        {
            var response = new FlagTileResponse
            {
                XPosition = request.XPosition,
                YPosition = request.YPosition
            };

            try
            {
                Board.FlagTile(request.XPosition, request.YPosition, true);
            }
            catch(Exception e)
            {
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
            }

            CheckWin();
            response.GameStatus = Status;

            return response;
        }

        public UnflagTileResponse UnflagTile(UnflagTileRequest request)
        {
            var response = new UnflagTileResponse
            {
                XPosition = request.XPosition,
                YPosition = request.YPosition
            };

            try
            {
                Board.FlagTile(request.XPosition, request.YPosition, false);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
            }

            CheckWin();
            response.GameStatus = Status;

            return response;
        }

        public TileStatusResponse TileStatus(TileStatusRequest request)
        {
            var response = new TileStatusResponse
            {
                XPosition = request.XPosition,
                YPosition = request.YPosition
            };

            try
            {
                var tile = Board.GetTile(request.XPosition, request.YPosition);
                response.HasBomb = tile.HasBomb;
                response.HasFlag = tile.HasFlag;
                response.IsOpen = tile.IsOpen;
                response.Bombs = tile.Bombs;
            }
            catch(Exception e)
            {
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
            }

            return response;
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
