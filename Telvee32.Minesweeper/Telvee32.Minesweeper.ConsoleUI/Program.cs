using System;
using Telvee32.Minesweeper.Common;
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

        public void ExecuteCommand(Command command)
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

                    _gameState.Board.FlagTile(f.XPos, f.YPos, true);

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
                    _gameState.NewGame(n.XLength, n.YLength, n.Bombs);
                    break;
                }
                case OpenCommand o:
                {
                    if (_gameState == null || _gameState.Board == null)
                    {
                        throw new InvalidOperationException("No game active. Please use the new command to start a game.");
                    }

                    _gameState.Board.OpenTile(o.XPos, o.YPos);
                        
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

                    _gameState.Board.FlagTile(u.XPos, u.YPos, false);

                    break;
                }
            }
        }
    }
}
