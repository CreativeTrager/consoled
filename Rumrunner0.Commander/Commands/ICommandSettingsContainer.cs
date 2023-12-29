namespace Rumrunner0.Commander.Commands;

/// <summary>
/// Container of the <see cref="CommandSettings"/>.
/// </summary>
/// <remarks>
/// Typically, the implementations of the <see cref="ICommand"/>
/// are also the <see cref="ICommandSettingsContainer"/>.
/// </remarks>
public interface ICommandSettingsContainer
{
	///
	/// <inheritdoc cref="CommandSettings"/>
	///
	CommandSettings Settings { get; init; }
}