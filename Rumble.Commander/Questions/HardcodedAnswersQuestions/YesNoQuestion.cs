using Rumble.Commander.Questions.HardcodedAnswersQuestions.Interfaces;
using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander.Questions.HardcodedAnswersQuestions;

internal sealed class YesNoQuestion : IHardcodedAnswersQuestion<YesNoQuestion.YesNoAnswersTable>
{
	internal sealed class YesNoAnswersTable : ICheckTable
	{
		internal string[] Yes { get; } = { "yes", "y" };
		internal string[] No  { get; } = { "no", "n" };
	}

	private readonly PredefinedAnswersQuestion _rPredefinedAnswersQuestion;
	internal YesNoQuestion(string question)
	{
		var answerTable = new YesNoAnswersTable();
		this._rPredefinedAnswersQuestion = new
		(
			question,
			answerTable.Yes,
			answerTable.No
		);
	}

	public IHardcodedAnswersQuestionResult<YesNoAnswersTable> Ask()
	{
		return new HardcodedAnswersQuestionResult<YesNoAnswersTable>()
		{
			Answer = _rPredefinedAnswersQuestion.Ask().Answer
		};
	}

	IPredefinedAnswersQuestionResult IPredefinedAnswersQuestionBehaviour.Ask()
	{
		return Ask();
	}
}