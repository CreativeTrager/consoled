using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rumble.Commander.Questions;
using Rumble.Essentials;

namespace Rumble.Commander;

///
/// <inheritdoc />
///
public sealed class CommonCommander : ICommander
{
	private readonly CommanderSettings _settings;
	/// <summary>
	/// Default command input prompt.
	/// </summary>
	private const string _defaultCommandInputPrompt = "Enter command";

	/// <summary>
	/// Default command confirmation prompt.
	/// </summary>
	private const string _defaultCommandConfirmationPrompt = "Are you sure?";

	private bool _exitIsNotRequested;

	/// <summary>
	/// Flag that indicates whether the aliases of the command should be used.
	/// </summary>
	/// <remarks><c>True</c> by default</remarks>
	private readonly bool _useAliases;

	/// <summary>
	/// Flag that indicates whether the case of the command name or alias should match.
	/// </summary>
	/// <remarks><c>True</c> by default</remarks>
	private readonly bool _matchCase;

	/// <summary>
	/// Flag that indicates whether to ask for confirmation before the command execution.
	/// </summary>
	/// <remarks><c>True</c> by default</remarks>
	private readonly bool _askForConfirmation;

	/// <summary>
	/// Command input prompt.
	/// </summary>
	private readonly string _commandInputPrompt;

	/// <summary>
	/// Command confirmation prompt.
	/// </summary>
	private readonly string _commandConfirmationPrompt;

	private readonly List<Command> _systemCommands;
	private readonly List<Command> _customCommands;

	private readonly Dictionary<SystemCommandNameWithAliases, CommandOverride> _systemCommandsOverrides;

	/// <summary>
	/// Overrides for system commands.
	/// </summary>
	/// <exception cref="ApplicationException">Thrown if overrides are not valid</exception>
	public Dictionary<SystemCommandNameWithAliases, CommandOverride> SystemCommandsOverrides
	{
		get => this._systemCommandsOverrides;
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if(value is null)
			{
				return;
			}

			this._systemCommandsOverrides = value;
			foreach(var (key, commandOverride) in this._systemCommandsOverrides)
			{
				if(this._systemCommands.SingleOrDefault(command => command.Settings.Name.Equals(key.Name)) is not { } commandToOverride)
				{
					throw new ApplicationException
					(
						$"System command with key \"{key}\" can't be overridden. " +
						$"The command with this key is not registered."
					);
				}

				if(commandOverride is null)
				{
					throw new ApplicationException
					(
						$"System command with key \"{key}\" can't be overridden. " +
						$"The command override is null."
					);
				}

				// if(commandOverride.Name is { } name)
				// {
				// 	var formattedName = name.Trim();
				// 	if(string.IsNullOrWhiteSpace(formattedName))
				// 	{
				// 		throw new ApplicationException
				// 		(
				// 			$"System command with key \"{key}\" can't be overridden. " +
				// 			$"The requested name can't be empty or whitespace."
				// 		);
				// 	}
				//
				// 	commandToOverride.Settings.Name = formattedName;
				// }

				if(commandOverride.ConfirmationPrompt is { } confirmationPrompt)
				{
					var formattedConfirmationPrompt = confirmationPrompt.Trim();
					if(string.IsNullOrWhiteSpace(formattedConfirmationPrompt))
					{
						throw new ApplicationException
						(
							$"System command with key \"{key}\" can't be overridden. " +
							$"The requested confirmation prompt can't be empty or whitespace."
						);
					}

					commandToOverride.Settings.ConfirmationPrompt = confirmationPrompt;
				}

				if(commandOverride.UseAliases is { } useAliases)
				{
					commandToOverride.Settings.UseAliases = useAliases;

					if(useAliases is false)
					{
						commandToOverride.Settings.Aliases = new ();
					}
					else if(commandOverride.Aliases is [_, ..] aliases)
					{
						commandToOverride.Settings.Aliases = aliases.ToList();
					}
				}

				if(commandOverride.MatchCase is { } matchCase)
				{
					commandToOverride.Settings.MatchCase = matchCase;
				}

				if(commandOverride.AskForConfirmation is { } askForConfirmation)
				{
					commandToOverride.Settings.AskForConfirmation = askForConfirmation;
				}
			}
		}
	}

	public required List<Command> CustomCommands
	{
		get => this._customCommands;
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if(value is null) { return; }
			this._customCommands = value;
		}
	}

	///
	/// <inheritdoc cref="_useAliases"/>
	///
	public bool UseAliases
	{
		init => this._useAliases = value;
	}

	///
	/// <inheritdoc cref="_matchCase"/>
	///
	public bool MatchCase
	{
		init => this._matchCase = value;
	}

	///
	/// <inheritdoc cref="_askForConfirmation"/>
	///
	public bool AskForConfirmation
	{
		init => this._askForConfirmation = value;
	}

	///
	/// <inheritdoc cref="_commandInputPrompt"/>
	///
	public string? CommandInputPrompt
	{
		init
		{
			if(string.IsNullOrWhiteSpace(value)) { return; }
			this._commandInputPrompt = value;
		}
	}

	///
	/// <inheritdoc cref="_commandConfirmationPrompt"/>
	///
	public string? ConfirmationPrompt
	{
		init
		{
			if(string.IsNullOrWhiteSpace(value)) { return; }
			this._commandConfirmationPrompt = value;
		}
	}

	///
	/// <inheritdoc cref="CommonCommander" />
	///
	public CommonCommander(CommanderSettings settings)
	{
		this._settings = settings;
		this._exitIsNotRequested = true;

		this._matchCase = true;
		this._useAliases = true;
		this._askForConfirmation = true;

		this._commandInputPrompt = CommonCommander._defaultCommandInputPrompt;
		this._commandConfirmationPrompt = CommonCommander._defaultCommandConfirmationPrompt;

		this._customCommands = new ();
		this._systemCommandsOverrides = new ();
		this._systemCommands = new ()
		{
			new ()
			{
				Action = () => this._exitIsNotRequested = false,
				Settings = new ()
				{
					Name = SystemCommandNameWithAliases.Exit.Name,
					Aliases = SystemCommandNameWithAliases.Exit.Aliases
				}
			},
			new ()
			{
				Action = () =>
				{
					settings.Writer.WriteLine
					(
						$"{Environment.NewLine}" +
						$"Help page{Environment.NewLine}" +
						$"System commands{Environment.NewLine}" +
						$"\t• {

							this._systemCommands!
								.Select(command => command.Settings.NameWithAliases.Joined(separator: "/"))
								.Joined(separator: $"{Environment.NewLine}\t• ")

						}{Environment.NewLine}" +
						$"Custom commands{Environment.NewLine}" +
						$"\t• {

							this._customCommands!
								.Select(command => command.Settings.NameWithAliases.Joined(separator: "/"))
								.Joined(separator: $"{Environment.NewLine}\t• ")

						}{Environment.NewLine}"
					);
				},
				Settings = new ()
				{
					Name = SystemCommandNameWithAliases.Help.Name,
					Aliases = SystemCommandNameWithAliases.Help.Aliases,
					AskForConfirmation = false
				}
			}
		};
	}

	///
	/// <inheritdoc />
	///
	public ICommander Run()
	{
		var commands = new List<Command>() { this._systemCommands, this._customCommands };
		var commandsWithNames = commands.Select(command => (Command: command, Names: new List<string>()
		{
			command.Settings.Name,
			command.Settings.UseAliases ?? this._useAliases
				? command.Settings.Aliases : ArraySegment<string>.Empty
		})).ToList();

		while(this._exitIsNotRequested)
		{
			var commandName = new Question
			(
				prompt: this._commandInputPrompt,
				correctAnswers: commandsWithNames.SelectMany(commandWithNames => commandWithNames.Names),
				settings: new ()
				{
					Reader = this._settings.Reader,
					Writer = this._settings.Writer,
					ShowHint = true,
					Hint = $"Unknown command. Type \"{SystemCommandNameWithAliases.Help.Name}\" for help."
				}
			).Ask().Answer;

			var commandWithNames = commandsWithNames.FirstOrDefault(commandWithNames => commandWithNames.Names.Contains(commandName));
			if (commandWithNames is { Command: null })
			{
				continue;
			}

			if(commandWithNames.Command.Settings.AskForConfirmation ?? this._askForConfirmation)
			{
				var confirmationPrompt =
				(
					commandWithNames.Command.Settings.ConfirmationPrompt
					?? this._commandConfirmationPrompt
				);

				if(new YesNoQuestion(confirmationPrompt, new ()
				{
					Reader = this._settings.Reader,
					Writer = this._settings.Writer

				}).Ask().Is(answers => answers.YesFlat) is false)
				{
					continue;
				}
			}

			commandWithNames.Command.Action.Invoke();
		}

		return this;
	}

	///
	/// <inheritdoc />
	///
	public void Dispose()
	{
		// Empty
	}
}

public sealed class CommanderSettings
{
	public TextReader Reader { get; init; }
	public TextWriter Writer { get; init; }
}