using System.Linq;
using Bettermage.ConsoleUtils.ControlsHandler.Interfaces;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions;


// ReSharper disable MissingIndent
// ReSharper disable ReturnTypeCanBeEnumerable.Local


namespace Bettermage.ConsoleUtils.ControlsHandler.Core 
{
	public sealed class ControlsHandler
	<THandleable, THandlerBundle>
	
	where THandleable : class, IControlsHandlerHandleable
	where THandlerBundle : class, IControlsHandlerBundle

	
	{
		private readonly ControlsHandlerConfig
			<THandleable, THandlerBundle> _rConfig;
		
		public ControlsHandler(ControlsHandlerConfig
			<THandleable, THandlerBundle> config) 
		{
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

				var confirm = new YesNoQuestion(
					question: $"Are you sure?"
				).Ask().Is(answers => answers.Yes);

				if(confirm) 
				{
					_rConfig.Commands[answer].Invoke(
						_rConfig.Handleable,
						_rConfig.Bundle
					);
				}
			}
			// ReSharper disable once FunctionNeverReturns
		}
	}
}
