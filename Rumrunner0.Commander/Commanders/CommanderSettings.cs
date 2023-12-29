using System;
using System.IO;
using Rumrunner0.Commander.Commands;

namespace Rumrunner0.Commander.Commanders;

/// <summary>
/// Settings of the <see cref="ICommander"/>.
/// </summary>
public sealed record class CommanderSettings
{
	/// <summary>
	/// Default input prompt.
	/// </summary>
	private const string _defaultInputPrompt = "Enter the command";

	/// <summary>
	/// Default confirmation prompt.
	/// </summary>
	private const string _defaultConfirmationPrompt = "Are you sure?";

	/// <summary>
	/// Default unknown input prompt.
	/// </summary>
	private const string _defaultUnknownInputHintTemplate = "Unknown command. Type \"{0}\" for help.";

	/// <summary>
	/// Writer of the replies.
	/// </summary>
	private readonly TextWriter _writer;

	/// <summary>
	/// Reader of the commands.
	/// </summary>
	private readonly TextReader _reader;

	/// <summary>
	/// Flag that indicates whether the aliases of the command should be used.
	/// </summary>
	/// <remarks><c>True</c> by default.</remarks>
	private readonly bool _useAliases;

	/// <summary>
	/// Flag that indicates whether the case of the command name or alias should match.
	/// </summary>
	/// <remarks><c>True</c> by default.</remarks>
	private readonly bool _matchCase;

	/// <summary>
	/// Flag that indicates whether to ask for confirmation before the command execution.
	/// </summary>
	/// <remarks><c>True</c> by default.</remarks>
	private readonly bool _askForConfirmation;

	/// <summary>
	/// Input prompt.
	/// </summary>
	private readonly string _inputPrompt;

	/// <summary>
	/// Confirmation prompt.
	/// </summary>
	private readonly string _confirmationPrompt;

	/// <summary>
	/// Unknown input hint.
	/// </summary>
	private readonly string _unknownInputHint;

	///
	/// <inheritdoc cref="CommanderSettings"/>
	///
	public CommanderSettings()
	{
		this._writer = Console.Out;
		this._reader = Console.In;

		this._useAliases = true;
		this._matchCase = true;
		this._askForConfirmation = false;

		this._inputPrompt = CommanderSettings._defaultInputPrompt;
		this._confirmationPrompt = CommanderSettings._defaultConfirmationPrompt;
		this._unknownInputHint = string.Format
		(
			CommanderSettings._defaultUnknownInputHintTemplate,
			Command.System.Name.Help
		);
	}

	/// <summary>
	/// Provider for the <see cref="Writer"/>.
	/// </summary>
	/// <exception cref="ArgumentNullException">Thrown, if "<paramref name="value"/>" is null.</exception>
	public Func<TextWriter> WriterProvider
	{
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (value is null)
			{
				throw new ArgumentNullException
				(
					paramName: nameof(value),
					message: $"Commander settings can't be composed. Writer provider is null."
				);
			}

			this._writer = value.Invoke();
		}
	}

	///
	/// <inheritdoc cref="_writer"/>
	///
	internal TextWriter Writer
	{
		get => this._writer;
		init => this._writer = value;
	}

	/// <summary>
	/// Provider for the <see cref="Reader"/>.
	/// </summary>
	/// <exception cref="ArgumentNullException">Thrown, if "<paramref name="value"/>" is null.</exception>
	public Func<TextReader> ReaderProvider
	{
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (value is null)
			{
				throw new ArgumentNullException
				(
					paramName: nameof(value),
					message: $"Commander settings can't be composed. Reader provider is null."
				);
			}

			this._reader = value.Invoke();
		}
	}

	///
	/// <inheritdoc cref="_reader"/>
	///
	internal TextReader Reader
	{
		get => this._reader;
		init => this._reader = value;
	}

	///
	/// <inheritdoc cref="_useAliases"/>
	///
	public bool UseAliases
	{
		get => this._useAliases;
		init => this._useAliases = value;
	}

	///
	/// <inheritdoc cref="_matchCase"/>
	///
	public bool MatchCase
	{
		get => this._matchCase;
		init => this._matchCase = value;
	}

	///
	/// <inheritdoc cref="_askForConfirmation"/>
	///
	public bool AskForConfirmation
	{
		get => this._askForConfirmation;
		init => this._askForConfirmation = value;
	}

	///
	/// <inheritdoc cref="_inputPrompt"/>
	///
	public string InputPrompt
	{
		get => this._inputPrompt;
		init
		{
			if (string.IsNullOrWhiteSpace(value)) { return; }
			this._inputPrompt = value;
		}
	}

	///
	/// <inheritdoc cref="_confirmationPrompt"/>
	///
	public string ConfirmationPrompt
	{
		get => this._confirmationPrompt;
		init
		{
			if (string.IsNullOrWhiteSpace(value)) { return; }
			this._confirmationPrompt = value;
		}
	}

	///
	/// <inheritdoc cref="_unknownInputHint"/>
	///
	public string UnknownInputHint
	{
		get => this._unknownInputHint;
		init
		{
			if (string.IsNullOrWhiteSpace(value)) { return; }
			this._unknownInputHint = value;
		}
	}
}