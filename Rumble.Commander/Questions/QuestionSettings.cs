using System.IO;

namespace Rumble.Commander.Questions;

/// <summary>
/// Settings of the <see cref="IQuestion"/>.
/// </summary>
internal sealed record class QuestionSettings
{
	///
	/// <inheritdoc cref="QuestionSettings"/>
	///
	internal QuestionSettings()
	{
		this.MatchCase = false;
		this.InputPrefix = ">";
	}

	/// <summary>
	/// Writer of the question.
	/// </summary>
	internal required TextWriter Writer { get; set; }

	/// <summary>
	/// Reader of the answer.
	/// </summary>
	internal required TextReader Reader { get; set; }

	/// <summary>
	/// Flag that indicates whether the case of the answer should match to correct answers case, if they exist.
	/// </summary>
	/// <remarks><c>False</c> by default.</remarks>
	internal bool MatchCase { get; set; }

	/// <summary>
	/// Prefix to indicate the input request.
	/// </summary>
	public string InputPrefix { get; set; }
}