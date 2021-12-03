using System;
using System.Collections.Generic;
using Bettermage.ConsoleUtils.ControlsHandler.Interfaces;


// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MissingIndent
// ReSharper disable ClassNeverInstantiated.Global


namespace Bettermage.ConsoleUtils.ControlsHandler.Core 
{
	public sealed class ControlsHandlerConfig
	<THandleable, TControlsHandlerBundle> 
	
	where THandleable : class, IControlsHandlerHandleable
	where TControlsHandlerBundle: class, IControlsHandlerBundle 
	
	
	{
		public THandleable Handleable { get; init; }
		public TControlsHandlerBundle Bundle { get; init; }
		
		public Dictionary<string[],
			Action<THandleable, TControlsHandlerBundle>
		> Commands { get; init; }
	}
}
