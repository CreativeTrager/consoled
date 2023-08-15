using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

/// <summary>
/// Question result which answer can be checked for check group.
/// </summary>
/// <typeparam name="TCheckTable">Type of the check table used to check the answer of the question result.</typeparam>
internal interface ICheckableResult<TCheckTable> where TCheckTable : ICheckTable
{
	/// <summary>
	/// Determines if the answer of the question result
	/// matches one of the elements in the specified check group.
	/// </summary>
	/// <param name="checkGroupProvider">Provider of the check group.</param>
	/// <returns>
	/// Result of the check operation.
	/// <c>True</c> if the answer is contained within the specified check group;
	/// otherwise, <c>false</c>
	/// </returns>
	bool Is(Expression<Func<TCheckTable, IEnumerable<string>>> checkGroupProvider);
}