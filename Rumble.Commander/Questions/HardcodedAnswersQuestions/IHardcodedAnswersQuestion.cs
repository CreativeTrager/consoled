using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander.Questions.HardcodedAnswersQuestions;

internal interface IHardcodedAnswersQuestion<TResultCheckTable> : IPredefinedAnswersQuestionBehaviour
where TResultCheckTable : ICheckTable
{
	new IHardcodedAnswersQuestionResult<TResultCheckTable> Ask();
}