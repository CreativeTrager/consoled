using System;
using System.Collections.Generic;
using System.Linq;
using Rumble.Commander.Questions.HardcodedAnswersQuestions;
using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander;

///
/// <inheritdoc />
///
public sealed class CommonCommander : ICommander
{
	/// <summary>
	/// Default command input prompt.
	/// </summary>
	private const string _defaultCommandInputPrompt = "Enter command";

	/// <summary>
	/// Default command confirmation prompt.
	/// </summary>
	private const string _defaultCommandConfirmationPrompt = "Are you sure?";

	/// <summary>
	/// Flag that indicates whether the aliases of the command should be used.
	/// </summary>
	private readonly bool _useAliases;

	/// <summary>
	/// Flag that indicates whether the case of the command name or alias should match.
	/// </summary>
	private readonly bool _matchCase;

	/// <summary>
	/// Flag that indicates whether to ask for confirmation before the command execution.
	/// </summary>
	private readonly bool _askForConfirmation;

	/// <summary>
	/// Command input prompt.
	/// </summary>
	private readonly string _commandInputPrompt;

	/// <summary>
	/// Command confirmation prompt.
	/// </summary>
	private readonly string _commandConfirmationPrompt;

	private readonly List<Command> _commands;
	private readonly List<Command> _systemCommands;
	private readonly List<Command> _customCommands;

	private readonly Dictionary<SystemCommandNameWithAliases, CommandOverride> _systemCommandsOverrides;

	private bool _exitIsNotRequested;

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

				if(commandOverride.Name is { } name)
				{
					var formattedName = name.Trim();
					if(string.IsNullOrWhiteSpace(formattedName))
					{
						throw new ApplicationException
						(
							$"System command with key \"{key}\" can't be overridden. " +
							$"The requested name can't be empty or whitespace."
						);
					}

					commandToOverride.Settings.Name = formattedName;
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
	public CommonCommander()
	{
		this._exitIsNotRequested = true;

		this._commandInputPrompt = CommonCommander._defaultCommandInputPrompt;
		this._commandConfirmationPrompt = CommonCommander._defaultCommandConfirmationPrompt;

		this._commands = new ();
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
			}
		};
	}

	///
	/// <inheritdoc />
	///
	public ICommander RunSelf()
	{
		this._commands.AddRange(this._systemCommands);
		this._commands.AddRange(this._customCommands);

		while(this._exitIsNotRequested)
		{
			var answer = new PredefinedAnswersQuestion
			(
				question: this._commandInputPrompt,
				answers: this._commands.Select(command => command.Settings.NameWithAliases)
			).Ask().Answer;

			var requestConfirmation = true;
			if(this._askForConfirmation is true)
			{
				requestConfirmation = new YesNoQuestion(question: this._commandConfirmationPrompt).Ask().Is(answers => answers.Yes.Name);
			}

			if(requestConfirmation)
			{
				if(this._commands.SingleOrDefault(command => command.Settings.NameWithAliases.Contains(answer)) is not { } command)
				{
					throw new ApplicationException();
				}

				command.Action.Invoke();
			}
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