# commander
A simple and intuitive console library to control console applications via commander objects like `CommonCommander` and `ObjectCommander`.

This repository contains the `Rumble.Commander` class library and `Rumble.Commander.Demo` demo project. All the content in the repository is an original work created to simplify the control of console applications in the .NET ecosystem.

[![NuGet Package: Rumble.Commander](https://img.shields.io/nuget/vpre/Rumble.Commander?label=nuget%3A%20Rumble.Commander)](https://www.nuget.org/packages/Rumble.Commander)

## Description
The `Rumble.Commander` is a configurable console command control program. Designed to provide an intuitive interface to control console applications, `Rumble.Commander` allows developers to define specific actions, aliases, confirmation prompts, and more. The library is quite simple so it can be improved in many ways.

The `Rumble.Commander.Demo.Runnable` is a console application demonstrating the usage of the `Rumble.Commander` library.

## Usage

### Rumble.Commander Library
```csharp
using Rumble.Commander.Commanders;
using Rumble.Commander.Commands;

// Usage of the CommonCommander.
using var commonCommander = new CommonCommander()
{
    // Configuration code here...
}.Run();

// Usage of the ObjectCommander.
// Replase the object with your real object to "control" it.
// See the Demo project for an extended example.
using var commonCommander = new ObjectCommander<object>(new object())
{
    // Configuration code here...
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
