﻿using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander.Questions.HardcodedAnswersQuestions.Interfaces;
internal interface IHardcodedAnswersQuestionResult<TCheckTable> : IPredefinedAnswersQuestionResult, ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable 
{
	// empty
}