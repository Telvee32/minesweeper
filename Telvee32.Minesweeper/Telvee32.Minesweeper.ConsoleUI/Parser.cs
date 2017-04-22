using System;
using System.Collections.Generic;
using System.Linq;
using Telvee32.Minesweeper.ConsoleUI.Commands;

namespace Telvee32.Minesweeper.ConsoleUI
{
    public static class Parser
    {
        public static Command Parse(string input)
        {
            string[] values = input.Split(' ');

            if (!values.Any())
            {
                throw new ArgumentException("No command specified.");
            }

            // command should be first
            if (!Enum.TryParse(values[0].ToPascalCase(), out CommandType type))
            {
                throw new ArgumentException($"Invalid command {values[0]}");
            }
            if(type == CommandType.Undefined)
            {
                throw new ArgumentException($"Invalid command {type}");
            }

            string[] args = values.Skip(1).ToArray();

            Command command = null;

            switch (type)
            {
                case CommandType.End:
                {
                    command = new EndCommand();
                    break;
                }
                case CommandType.Flag:
                {
                    command = new FlagCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();

                    if (args.Length != requiredArgs.Count)
                    {
                        throw new ArgumentException($"Command type {type} requires {requiredArgs.Count} arguments.");
                    }

                    if (!int.TryParse(args[0], out int x))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.XPosition} must be an integer.");
                    }
                    if (!int.TryParse(args[1], out int y))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.YPosition} must be an integer.");
                    }

                    requiredArgs[CommandKeys.XPosition] = x;
                    requiredArgs[CommandKeys.YPosition] = y;

                    command.Build(requiredArgs);

                    break;
                }
                case CommandType.Help:
                {
                    command = new HelpCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();

                    if (args.Length > 0)
                    {
                        if (args.Length > 1)
                        {
                            throw new ArgumentException($"Command type {type} requires 1 or 0 arguments.");
                        }

                        if (!Enum.TryParse(args[0].ToPascalCase(), out CommandType commandType))
                        {
                            throw new ArgumentException($"Invalid command type {args[0]}");
                        }

                        requiredArgs[CommandKeys.CommandName] = commandType;

                        command.Build(requiredArgs);
                    }

                    break;
                }
                case CommandType.New:
                {
                    command = new NewCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();

                    if (args.Length != requiredArgs.Count)
                    {
                        throw new ArgumentException($"Command type {type} requires {requiredArgs.Count} arguments.");
                    }

                    if (!int.TryParse(args[0], out int x))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.XLength} must be an integer.");
                    }
                    if (!int.TryParse(args[1], out int y))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.YLength} must be an integer.");
                    }
                    if (!int.TryParse(args[2], out int b))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.Bombs} must be an integer.");
                    }

                    requiredArgs[CommandKeys.XLength] = x;
                    requiredArgs[CommandKeys.YLength] = y;
                    requiredArgs[CommandKeys.Bombs] = b;

                    command.Build(requiredArgs);

                    break;
                }
                case CommandType.Open:
                {
                    command = new OpenCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();

                    if (args.Length != requiredArgs.Count)
                    {
                        throw new ArgumentException($"Command type {type} requires {requiredArgs.Count} arguments.");
                    }

                    if (!int.TryParse(args[0], out int x))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.XPosition} must be an integer.");
                    }
                    if (!int.TryParse(args[1], out int y))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.YPosition} must be an integer.");
                    }

                    requiredArgs[CommandKeys.XPosition] = x;
                    requiredArgs[CommandKeys.YPosition] = y;

                    command.Build(requiredArgs);

                    break;
                }
                case CommandType.Quit:
                {
                    command = new QuitCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();
                    break;
                }
                case CommandType.Unflag:
                {
                    command = new UnflagCommand();
                    Dictionary<string, object> requiredArgs = command.GetRequiredArguments();

                    if (args.Length != requiredArgs.Count)
                    {
                        throw new ArgumentException($"Command type {type} requires {requiredArgs.Count} arguments.");
                    }

                    if (!int.TryParse(args[0], out int x))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.XPosition} must be an integer.");
                    }
                    if (!int.TryParse(args[1], out int y))
                    {
                        throw new ArgumentException($"Parameter {CommandKeys.YPosition} must be an integer.");
                    }

                    requiredArgs[CommandKeys.XPosition] = x;
                    requiredArgs[CommandKeys.YPosition] = y;

                    command.Build(requiredArgs);

                    break;
                }
            }

            return command;
        }
    }
}
