# commander
A simple and intuitive console library to control console applications via commander objects like `CommonCommander` and `ObjectCommander`.

This repository contains the `Rumble.Commander` class library and `Rumble.Commander.Demo` demo project. All the content in the repository is an original work created to simplify the control of console applications in the .NET ecosystem.

[![License](https://img.shields.io/github/license/rumrunner0/commander?label=license)](https://github.com/rumrunner0/commander/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Rumble.Commander?label=nuget)](https://www.nuget.org/packages/Rumble.Commander)

## Description
The `Rumble.Commander` is incredibly simple but highly configurable console command control program. Designed as a command-based utility to provide an intuitive interface to control console applications, `Rumble.Commander` allows developers to define specific actions, aliases, confirmation prompts, and more. The library is quite simple so it can be improved in many ways.

The `Rumble.Commander.Demo.Runnable` is a console application demonstrating the usage of the `Rumble.Commander` library.

## Usage

### Rumble.Commander Library
```csharp
using Rumble.Commander.Commanders;
using Rumble.Commander.Commands;
using Rumble.Commander.Demo.Runnable;

using var commonCommander = new ObjectCommander<DummyServer>(new DummyServer())
{
    Settings = new ()
    {
        // Default values can be overridden...
        InputPrompt = "Please, enter the command",

        // ...or left as they are (can be omitted).
        ConfirmationPrompt = default!,
        UnknownInputHint = default!,

        // Extra tweaks also have default values
        // but available for override even for specific command.
        UseAliases = true,
        MatchCase = true,
        AskForConfirmation = false
    },
    CustomCommands = new ()
    {
        new ()
        {
            Action = commandable => commandable.Initialize(),
            Settings = new ()
            {
                Name = "initialize",
                Aliases = new () { "init" },

                // Commander will NOT use the aliases of this specific command.
                UseAliases = false
            }
        },
        new ()
        {
            Action = commandable => commandable.Start(),
            Settings = new ()
            {
                Name = "start",
                Aliases = new () { "begin", "run" },

                // Commander WILL treat "Start", "rUn" etc. as valid command names.
                MatchCase = false
            }
        },
        new ()
        {
            Action = commandable => commandable.Stop(),
            Settings = new ()
            {
                Name = "stop",
                Aliases = new () { "terminate" },

                // Commander WILL ask for confirmation.
                // This overrides behaviour specified on Commander level.
                AskForConfirmation = true,
                ConfirmationPrompt = "This command will STOP the server. Are you sure?"
            }
        }
    },

    // System commands such as "help" or "quit" can be overridden.
    // All parameters are available for override EXCEPT Action and Name.
    SystemCommandsOverrides = new ()
    {
        [Command.System.Name.Quit] = new ()
        {
            Aliases = new () { "release", "exit", "qq" },
            ConfirmationPrompt = "You are about to QUIT the current commander. Do you confirm?"
        }
    }

// Commander has to be run after the configuration.
}.Run();
```

### Rumble.Commander.Demo.Runnable Console Application
To understand how to utilize the `Rumble.Commander` library, the`Rumble.Commander.Demo` demo project provides comprehensive examples for various use cases. Simply browse the demo project to get started.

## History
The Commander project is developed with an emphasis on flexibility and robustness, aiming to provide developers with fine-grained control over console applications in a .NET environment.

## Contributing
If you have any suggestions, ideas, or feedback to enhance the project, please feel free to create an issue. Your collaboration is welcomed to make this project more powerful and efficient.

## Note
The code is 100% original.
