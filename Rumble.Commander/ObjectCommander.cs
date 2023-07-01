using System;
using System.Linq;
using Rumble.Commander.Questions.HardcodedAnswersQuestions;
using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander;

///
/// <inheritdoc />
///
/// <typeparam name="TCommandable">Type of the commandable object over which the <see cref="ObjectCommander{TCommandable}"/> operates</typeparam>
public sealed class ObjectCommander<TCommandable> : ICommander
where TCommandable : class, IDisposable
{
	private readonly TCommandable _commandable;

	/// <summary>
	/// Settings that define the behavior of the <see cref="ObjectCommander{TCommandable}"/>.
	/// </summary>
	private readonly CommanderSettings<TCommandable> _settings;

	/// <summary>
	/// Constructor of the <see cref="CommonCommander"/> instance.
	/// </summary>
	/// <param name="commandable">Commandable object over which the <see cref="ObjectCommander{TCommandable}"/> operates</param>
	/// <param name="settings">Settings that provides the configuration</param>
	public ObjectCommander(TCommandable commandable, CommanderSettings<TCommandable> settings)
	{
		this._commandable = commandable;
		this._settings = settings;
	}

	///
	/// <inheritdoc />
	///
	public ICommander RunSelf()
	{
		while(true)
		{
			var answer = new PredefinedAnswersQuestion
			(
				question: $"Enter command",
				answers: this._settings.Commands.Keys.ToArray()
			).Ask().Answer;

			var confirm = true;
			if(this._settings.AskForConfirmation is true)
			{
				confirm = new YesNoQuestion(question: $"Are you sure?").Ask().Is(answers => answers.Yes);
			}

			if(confirm)
			{
				_settings.Commands[answer].Invoke(_commandable);
			}
		}

		return this;
	}

	///
	/// <inheritdoc />
	///
	public void Dispose()
	{
		this._commandable.Dispose();
	}
}