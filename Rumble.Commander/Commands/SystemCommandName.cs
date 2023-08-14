namespace Rumble.Commander.Commands;

///
/// <inheritdoc />
///
public partial class Command
{
	/// <summary>
	/// System command in its default state.
	/// </summary>
	public static partial class System
	{
		/// <summary>
		/// Name of the system command.
		/// </summary>
		public sealed class Name
		{
			/// <summary>
			/// Name of the command.
			/// </summary>
			private readonly string _value;

			///
			/// <inheritdoc cref="Name"/>
			///
			private Name(string value)
			{
				this._value = value;
			}

			///
			/// <inheritdoc cref="object.ToString"/>
			///
			/// <returns><see cref="string"/> representation of the system command.</returns>
			public override string ToString()
			{
				return this._value;
			}

			/// <summary>
			/// Operator that implicitly converts system command <see cref="Command.System.Name"/> to its <see cref="string"/> representation.
			/// </summary>
			/// <returns><see cref="string"/> representation of the system command.</returns>
			public static implicit operator string(Name source)
			{
				return source._value;
			}

			/// <summary>
			/// Name of the <see cref="System.Quit"/> system command.
			/// </summary>
			// ReSharper disable once MemberHidesStaticFromOuterClass
			public static Name Quit => new (value: "quit");

			/// <summary>
			/// Name of the <see cref="System.Help"/> system command.
			/// </summary>
			// ReSharper disable once MemberHidesStaticFromOuterClass
			public static Name Help => new (value: "help");
		}
	}
}