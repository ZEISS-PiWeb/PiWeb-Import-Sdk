#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.PropertyStorage;

/// <summary>
/// Represents a simple reader for a key value storage with change notifications.
/// </summary>
public interface IPropertyObserver : IPropertyReader
{
    #region events

    /// <summary>
    /// Event that is raised if any property has changed.
    /// </summary>
    public event EventHandler? Changed;

    #endregion
}