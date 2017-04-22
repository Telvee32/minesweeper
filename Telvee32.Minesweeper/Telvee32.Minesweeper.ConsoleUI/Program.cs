using System;
using Telvee32.Minesweeper.Common;
using Telvee32.Minesweeper.Common.Communication;
using Telvee32.Minesweeper.Common.Exceptions;
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

                    try
                    {
                        _gameState.FlagTile(f.XPos, f.YPos, true);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        PrintBoard(_gameState.Board);
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
                    _gameState = new GameState();
                    _gameState.NewGame(new NewGameRequest
                    {
                        XLength = n.XLength,
                        YLength = n.YLength,
                        Bombs = n.Bombs
                    });

                    PrintBoard(_gameState.Board);

                    break;
                }
                case OpenCommand o:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    try
                    {
                        _gameState.OpenTile(o.XPos, o.YPos);
                    }
                    catch (BombException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        PrintBoard(_gameState.Board);

                        if (_gameState.Status == GameStatus.Fail)
                        {
                            _gameState.EndGame();
                            _gameState = null;
                            Console.WriteLine("You lose. Type new to start again.");
                        }                        
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

                    _gameState.FlagTile(u.XPos, u.YPos, false);

                    PrintBoard(_gameState.Board);

                    break;
                }
                case StatusCommand s:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    var tile = _gameState.Board.Tiles[s.XPos, s.YPos];

                    Console.WriteLine($"Status of tile at ({s.XPos}, {s.YPos}):");
                    Console.WriteLine($"Open: {tile.IsOpen}");
                    Console.WriteLine($"Flagged: {tile.HasFlag}");

                    if (tile.IsOpen)
                    {
                        Console.WriteLine($"Has bomb: {tile.HasBomb}");
                        Console.WriteLine($"Bombs in surrounding 8 tiles: {tile.Bombs}");
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
            Console.WriteLine($"Flags: {_gameState.Flags}");
            Console.WriteLine();

            // top line
            Console.Write("* * ");
            for (int i = 0; i < board.XLength; i++)
            {
                Console.Write("* ");
            }
            Console.Write("* *");
            Console.WriteLine();

            // second line
            Console.Write("*   ");
            for (int i = 0; i < board.XLength; i++)
            {
                Console.Write("  ");
            }
            Console.Write("  *");
            Console.WriteLine();

            // actual board
            for (int y = 0; y < board.YLength; y++)
            {
                Console.Write("*   ");
                for (int x = 0; x < board.XLength; x++)
                {
                    var tile = board.Tiles[x, y];
                    var details = GetTileLabel(tile);
                    Console.ForegroundColor = details.colour;
                    Console.Write($"{details.label} ");
                    Console.ResetColor();
                }
                Console.Write("  *");
                Console.WriteLine();
            }

            // penultimate line
            Console.Write("*   ");
            for (int i = 0; i < board.XLength; i++)
            {
                Console.Write("  ");
            }
            Console.Write("  *");
            Console.WriteLine();

            // final line
            Console.Write("* * ");
            for (int i = 0; i < board.XLength; i++)
            {
                Console.Write("* ");
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
    }
}
