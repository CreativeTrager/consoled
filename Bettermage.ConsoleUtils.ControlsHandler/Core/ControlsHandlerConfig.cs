using System;
using System.Collections.Generic;
using Bettermage.ConsoleUtils.ControlsHandler.Interfaces;


// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Bettermage.ConsoleUtils.ControlsHandler.Core;
public sealed class ControlsHandlerConfig <THandleable> 
where THandleable : class, IControlsHandlerHandleable
{
	public bool AskForConfirmation { get; init; }
	public Dictionary<string[], Action<THandleable>> Commands { get; init; }
}