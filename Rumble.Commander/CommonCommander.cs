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
	/// Settings that define the behavior of the <see cref="CommonCommander"/>.
	/// </summary>
	private readonly CommanderSettings _settings;

	/// <summary>
	/// Constructor of the <see cref="CommonCommander"/> instance.
	/// </summary>
	/// <param name="settings">Settings that provides the configuration</param>
	public CommonCommander(CommanderSettings settings)
	{
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
				_settings.Commands[answer].Invoke();
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