using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

internal interface ICheckableResult<TCheckTable> where TCheckTable : ICheckTable
{
	bool Is(Expression<Func<TCheckTable, IEnumerable<string>>> check);
}