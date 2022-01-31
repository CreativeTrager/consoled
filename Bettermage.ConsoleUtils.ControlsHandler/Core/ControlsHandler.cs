using System.Linq;
using Bettermage.ConsoleUtils.ControlsHandler.Interfaces;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions;


namespace Bettermage.ConsoleUtils.ControlsHandler.Core;
public sealed class ControlsHandler<THandleable>
where THandleable : class, IControlsHandlerHandleable 
{
	private readonly THandleable _handleable;
	private readonly ControlsHandlerConfig<THandleable> _rConfig;

	public ControlsHandler(THandleable handleable, 
		ControlsHandlerConfig<THandleable> config
	) 
	{
		this._handleable = handleable;
		this._rConfig = config;
	}

	public void Handle() 
	{
		while(true) 
		{
			var answer = new PredefinedAnswersQuestion(
				question: $"Enter command",
				answers: _rConfig.Commands.Keys.ToArray()
			).Ask().Answer;

			var confirm = true;
			if(_rConfig.AskForConfirmation is true) 
			{
				confirm = new YesNoQuestion(
					question: $"Are you sure?"
				).Ask().Is(answers => answers.Yes);
			}

			if(confirm) 
			{
				_rConfig.Commands[answer]
					.Invoke(_handleable);
			}
		}
		// ReSharper disable once FunctionNeverReturns
	}
}