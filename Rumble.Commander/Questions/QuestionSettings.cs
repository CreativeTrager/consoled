using System.IO;

namespace Rumble.Commander.Questions;

internal sealed record class QuestionSettings
{
	internal required TextReader Reader { get; init; }
	internal required TextWriter Writer { get; init; }

	internal bool MatchCase { get; init; }

	public string InputPrefix { get; set; }

	internal QuestionSettings()
	{
		this.MatchCase = false;
		this.InputPrefix = ">";
	}
}