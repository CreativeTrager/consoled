# commander
A simple and intuitive console library to control console applications via commander objects like `CommonCommander` and `ObjectCommander`.

This repository contains the `Rumrunner0.Commander` class library and `Rumrunner0.Commander.Demo` demo project. All the content in the repository is an original work created to simplify the control of console applications in the .NET ecosystem.

[![License](https://img.shields.io/github/license/rumrunner0/commander?label=license)](https://github.com/rumrunner0/commander/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Rumrunner0.Commander?label=nuget)](https://www.nuget.org/packages/Rumrunner0.Commander)

## Description
The `Rumrunner0.Commander` is incredibly simple but highly configurable console command control program. Designed as a command-based utility to provide an intuitive interface to control console applications, `Rumrunner0.Commander` allows developers to define specific actions, aliases, confirmation prompts, and more. The library is quite simple so it can be improved in many ways.

The `Rumrunner0.Commander.Demo.Runnable` is a console application demonstrating the usage of the `Rumrunner0.Commander` library.

## Usage

### Rumrunner0.Commander Library
```csharp
using Rumrunner0.Commander.Commanders;
using Rumrunner0.Commander.Commands;
using Rumrunner0.Commander.Demo.Runnable;

// There are 2 different commanders for now: CommonCommander and ObjectCommander.
// Here is usage example of one of the commanders – ObjectCommander.
using var commander = new ObjectCommander<DummyServer>(new DummyServer())
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
    CustomCommands =
    [
        new ()
        {
            Action = commandable => commandable.Initialize(),
            Settings = new ()
            {
                Name = "initialize",
                Aliases = [ "init" ],

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
                Aliases = [ "begin", "run" ],

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
                Aliases = [ "terminate" ],

                // Commander WILL ask for confirmation.
                // This overrides behaviour specified on Commander level.
                AskForConfirmation = true,
                ConfirmationPrompt = "This command will STOP the server. Are you sure?"
            }
        }
    ],

    // System commands such as "help" or "quit" can be overridden.
    // All parameters are available for override EXCEPT Action and Name.
    SystemCommandsOverrides = new ()
    {
        [Command.System.Name.Quit] = new ()
        {
            Aliases = [ "release", "exit", "qq" ],
            ConfirmationPrompt = "You are about to QUIT the commander. Are you sure?"
        }
    }

// Commander has to be run after the configuration.
}.Run();
```

### Rumrunner0.Commander.Demo.Runnable Console Application
To understand how to utilize the `Rumrunner0.Commander` library, the`Rumrunner0.Commander.Demo` demo project provides comprehensive examples for various use cases. Simply browse the demo project to get started.

## History
The Commander project is developed with an emphasis on flexibility and robustness, aiming to provide developers with fine-grained control over console applications in a .NET environment.

## Contributing
If you have any suggestions, ideas, or feedback to enhance the project, please feel free to create an issue. Your collaboration is welcomed to make this project more powerful and efficient.