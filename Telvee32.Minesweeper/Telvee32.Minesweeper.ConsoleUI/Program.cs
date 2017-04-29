using System;
using Telvee32.Minesweeper.Common;
using Telvee32.Minesweeper.Common.Communication.Request;
using Telvee32.Minesweeper.Common.Communication.Response;
using Telvee32.Minesweeper.Common.Model;
using Telvee32.Minesweeper.ConsoleUI.Commands;

namespace Telvee32.Minesweeper.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner();
            runner.Run();
        }
    }

    public class Runner
    {
        private GameState _gameState;
        private bool _active;

        public void Run()
        {
            Console.WriteLine("C# Mines");
            Console.WriteLine("Kyle Puttifer, 2017");
            Console.WriteLine("Type new to start a new game, or type help for a list of commands.");
            Console.WriteLine();
            _active = true;

            while (_active)
            {
                Console.Write("csharp mines > ");
                var input = Console.ReadLine();

                try
                {
                    var command = Parser.Parse(input);
                    ExecuteCommand(command);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine();
            }
        }

        private void ExecuteCommand(Command command)
        {
            switch (command)
            {
                case EndCommand e:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    _gameState.EndGame();
                    _gameState = null;

                    Console.WriteLine("Game ended.");

                    break;
                }
                case FlagCommand f:
                {
                    if(_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }
                    
                    var response = _gameState.FlagTile(new FlagTileRequest
                    {
                        XPosition = f.XPos,
                        YPosition = f.YPos
                    });
                    
                    ClearBoard();
                    PrintBoard(_gameState.Board);

                    if (response.Status == ResponseStatus.Error)
                    {
                        Console.WriteLine();
                        Console.WriteLine(response.Message);
                    }

                    break;
                }
                case HelpCommand h:
                {
                    if(h.RequiredCommand == CommandType.Undefined)
                    {
                        Console.WriteLine("List of Valid Commands. Please type help followed by a command to see more information.");
                        Console.WriteLine();

                        foreach(var d in CommandKeys.CommandDescriptions)
                        {
                            Console.WriteLine($"{d.Key}");
                        }
                    }
                    else
                    {
                        Console.WriteLine(CommandKeys.CommandDescriptions[h.RequiredCommand]);
                    }

                    break;
                }
                case NewCommand n:
                {
                    if ((n.XLength > 100 || n.XLength <= 0) || (n.YLength > 100 || n.YLength <= 0))
                        throw new IndexOutOfRangeException("Board X and Y size must be no less than 1 and no greater than 100.");
                    _gameState = new GameState();

                    _gameState.NewGame(new NewGameRequest
                    {
                        XLength = n.XLength,
                        YLength = n.YLength,
                        Bombs = n.Bombs
                    });

                    ClearBoard();
                    PrintBoard(_gameState.Board);

                    break;
                }
                case OpenCommand o:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }
                                        
                    var response = _gameState.OpenTile(new OpenTileRequest
                    {
                        XPosition = o.XPos,
                        YPosition = o.YPos
                    });

                    ClearBoard();
                    PrintBoard(_gameState.Board);

                    if(response.Status == ResponseStatus.Error)
                    {
                        Console.WriteLine();
                        Console.WriteLine(response.Message);
                    }

                    if (_gameState.Status == GameStatus.Fail)
                    {
                        _gameState.EndGame();
                        _gameState = null;
                        Console.WriteLine();
                        Console.WriteLine(response.Message);
                        Console.WriteLine("You lose. Type new to start again.");
                    }                      
                    
                    break;
                }
                case QuitCommand q:
                {
                    _active = false;
                    break;
                }
                case UnflagCommand u:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    var response = _gameState.UnflagTile(new UnflagTileRequest
                    {
                        XPosition = u.XPos,
                        YPosition = u.YPos
                    });

                    ClearBoard();
                    PrintBoard(_gameState.Board);

                    if (response.Status == ResponseStatus.Error)
                    {
                        Console.WriteLine();
                        Console.WriteLine(response.Message);
                    }

                    break;
                }
                case StatusCommand s:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    var response = _gameState.TileStatus(new TileStatusRequest
                    {
                        XPosition = s.XPos,
                        YPosition = s.YPos
                    });

                    if(response.Status == ResponseStatus.Ok)
                    {
                        Console.WriteLine($"Status of tile at ({s.XPos}, {s.YPos}):");
                        Console.WriteLine($"Open: {response.IsOpen}");
                        Console.WriteLine($"Flagged: {response.HasFlag}");

                        if (response.IsOpen)
                        {
                            Console.WriteLine($"Has bomb: {response.HasBomb}");
                            Console.WriteLine($"Bombs in surrounding 8 tiles: {response.Bombs}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(response.Message);
                    }

                    break;
                }
            }

            if(_gameState != null && _gameState.Status == GameStatus.Win)
            {
                _gameState.EndGame();
                _gameState = null;

                Console.WriteLine("You win. Type new to start again.");
            }
        }

        private void PrintBoard(Board board)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Flags: ");
            Console.ResetColor();
            Console.WriteLine(_gameState.Flags);
            Console.WriteLine();

            // top line
            Console.Write("* * ");
            for (int x = 0; x < board.XLength; x++)
            {
                if(x < 10)
                {
                    Console.Write($"{x} ");
                }
                else
                {
                    Console.Write(x);
                }
            }
            Console.Write("* *");
            Console.WriteLine();

            // second line
            Console.Write("*   ");
            for (int x = 0; x < board.XLength; x++)
            {
                Console.Write("  ");
            }
            Console.Write("  *");
            Console.WriteLine();

            // actual board
            for (int y = 0; y < board.YLength; y++)
            {
                if (y < 10)
                {
                    Console.Write($"{y}   ");
                }
                else
                {
                    Console.Write($"{y}  ");
                }
                for (int x = 0; x < board.XLength; x++)
                {
                    var tile = board.Tiles[x, y];
                    var details = GetTileLabel(tile);
                    Console.ForegroundColor = details.colour;
                    Console.Write($"{details.label} ");
                    Console.ResetColor();
                }
                if (y < 10)
                {
                    Console.Write($"  {y}");
                }
                else
                {
                    Console.Write($" {y}");
                }
                Console.WriteLine();
            }

            // penultimate line
            Console.Write("*   ");
            for (int x = 0; x < board.XLength; x++)
            {
                Console.Write("  ");
            }
            Console.Write("  *");
            Console.WriteLine();

            // final line
            Console.Write("* * ");
            for (int x = 0; x < board.XLength; x++)
            {
                if (x < 10)
                {
                    Console.Write($"{x} ");
                }
                else
                {
                    Console.Write(x);
                }
            }
            Console.Write("* *");
            Console.WriteLine();
        }

        private (string label, ConsoleColor colour) GetTileLabel(Tile tile)
        {
            if (tile.IsOpen)
            {
                if (!tile.HasBomb)
                {
                    return tile.Bombs == 0 ? ("_", ConsoleColor.White) : (tile.Bombs.ToString(), ConsoleColor.Blue);
                }
                else
                {
                    return ("X", ConsoleColor.Red);
                }
            }
            else
            {
                return tile.HasFlag ? ("F", ConsoleColor.Yellow) : ("#", ConsoleColor.Gray);
            }
        }

        private void ClearBoard()
        {
            int pLeft = Console.CursorLeft;
            int pTop = Console.CursorTop;

            for (int i = 4; i < pTop; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth - Console.CursorLeft));
            }
            Console.SetCursorPosition(0, 4);
        }
    }
}
