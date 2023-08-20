using System;
using System.Collections.Generic;
using System.Linq;
using Rumble.Commander.Commands;
using Rumble.Commander.Extensions;
using Rumble.Commander.Questions;
using Rumble.Essentials;

namespace Rumble.Commander.Commanders;

///
/// <inheritdoc />
///
public abstract class Commander : ICommander
{
	///
	/// <inheritdoc cref="CommanderSettings"/>
	///
	private readonly CommanderSettings _settings;

	/// <summary>
	/// System (built-in) commands.
	/// </summary>
	private readonly List<Command> _systemCommands;

	/// <summary>
	/// Dictionary representation of the <see cref="_systemCommands"/> linked to their names.
	/// </summary>
	private readonly Dictionary<string, Command> _systemCommandsDictionary;

	/// <summary>
	/// Overrides for system commands.
	/// </summary>
	private readonly Dictionary<Command.System.Name, CommandOverride> _systemCommandsOverrides;

	/// <summary>
	/// Flag that indicates whether the commander should not be terminated.
	/// </summary>
	/// <remarks><c>True</c> by default.</remarks>
	private bool _terminationIsNotRequested;



	///
	/// <inheritdoc cref="_settings"/>
	///
	public CommanderSettings Settings
	{
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if(value is null) { return; }
			this._settings = value;
		}
	}

	///
	/// <inheritdoc cref="_systemCommandsOverrides"/>
	///
	/// <exception cref="ApplicationException">Thrown, if any override is null or not valid.</exception>
	public Dictionary<Command.System.Name, CommandOverride> SystemCommandsOverrides
	{
		init
		{
			this._systemCommandsOverrides = value ?? throw new ApplicationException
			(
				$"System commands overrides can't be applied. " +
				$"The container can't be null."
			);

			foreach(var (key, commandOverride) in this._systemCommandsOverrides)
			{
				if(this._systemCommands.SingleOrDefault(command => command.Settings.Name.Equals(key)) is not { } commandToOverride)
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

	/// <summary>
	/// Settings of the custom (user-defined) commands.
	/// </summary>
	protected abstract IReadOnlyCollection<ICommandSettingsContainer> CustomCommandsSettings { get; }



	///
	/// <inheritdoc cref="Commander"/>
	///
	protected Commander()
	{
		this._settings = new ();
		this._terminationIsNotRequested = true;

		this._systemCommandsOverrides = new ();
		this._systemCommands = new ()
		{
			Command.System.Quit(() => this._terminationIsNotRequested = false),
			Command.System.Help(() =>
			{
				this._settings.Writer.WriteLine
				(
					$"{Environment.NewLine}" +
					$"Help page{Environment.NewLine}" +
					$"System commands{Environment.NewLine}" +
					$"\t• {

						this._systemCommands!
							.Select(command => command.AvailableNamesUsingFallback(() => this._settings.UseAliases).Joined(separator: "/"))
							.Joined(separator: $"{Environment.NewLine}\t• ")

					}{Environment.NewLine}" +
					$"Custom commands{Environment.NewLine}" +
					$"\t• {

						this.CustomCommandsSettings
							.Select(command => command.AvailableNamesUsingFallback(() => this._settings.UseAliases).Joined(separator: "/"))
							.Joined(separator: $"{Environment.NewLine}\t• ")

					}{Environment.NewLine}"
				);
			})
		};

		this._systemCommandsDictionary = this._systemCommands.ToDictionary
		(
			keySelector: command => command.Settings.Name,
			elementSelector: command => command
		);
	}



	/// <summary>
	/// Executes the command action.
	/// </summary>
	/// <param name="name">Name of the command.</param>
    protected abstract void ExecuteCustomCommandByName(string name);

	///
	/// <inheritdoc />
	///
	public ICommander Run()
	{
		var commands = new List<ICommandSettingsContainer>()
		{
			this._systemCommands.Cast<ICommandSettingsContainer>().ToList(),
			this.CustomCommandsSettings
		};

		var commandsWithNames = commands.Select(command =>
		(
			Command: command,
			Names: command.AvailableNamesUsingFallback(() => this._settings.UseAliases)
		)).ToList();

		var questionCorrectAnswers = commandsWithNames.SelectMany(command => command.Names).ToList();
		var questionSettings = new QuestionSettings()
		{
			Writer = this._settings.Writer,
			Reader = this._settings.Reader,
			MatchCase = false
		};

		while (this._terminationIsNotRequested)
		{
			var questionResult = new Question
			(
				prompt: this._settings.InputPrompt,
				correctAnswers: questionCorrectAnswers,
				settings: questionSettings
			).Ask();

			if (questionResult.IsIncorrect)
			{
				if (string.IsNullOrWhiteSpace(questionResult.Answer) is false)
				{
					this._settings.Writer.WriteLine($"{this._settings.UnknownInputHint}");
				}

				continue;
			}

			var rawCommandName = questionResult.Answer;
			var commandWithNames = commandsWithNames.First
			(
				command => command.Names.Contains
				(
					rawCommandName,
					StringComparer.CurrentCultureIgnoreCase
				)
			);

			var matchCase = commandWithNames.Command.Settings.MatchCase ?? this._settings.MatchCase;
			if (matchCase && commandWithNames.Names.Contains(rawCommandName) is false)
			{
				this._settings.Writer.WriteLine($"{this._settings.UnknownInputHint}");
				continue;
			}

			var askForConfirmation = commandWithNames.Command.Settings.AskForConfirmation ?? this._settings.AskForConfirmation;
			if (askForConfirmation)
			{
				var confirmationResult = default(IQuestionResult<YesNoAnswersTable>);
				var confirmationPrompt = commandWithNames.Command.Settings.ConfirmationPrompt ?? this._settings.ConfirmationPrompt;

				while (true)
				{
					confirmationResult = new Question<YesNoAnswersTable>(confirmationPrompt, questionSettings).Ask();
					if (confirmationResult is { IsCorrect: true })
					{
						break;
					}
				}

				if (confirmationResult.Is(answers => answers.Yes) is false)
				{
					continue;
				}
			}

			var commandName = commandWithNames.Command.Settings.Name;
			if(this._systemCommandsDictionary.TryGetValue(commandName, out var systemCommand))
			{
				systemCommand.Action.Invoke();
			}
			else
			{
				this.ExecuteCustomCommandByName(commandName);
			}
		}

		return this;
	}

	///
	/// <inheritdoc />
	///
	public abstract void Dispose();
}