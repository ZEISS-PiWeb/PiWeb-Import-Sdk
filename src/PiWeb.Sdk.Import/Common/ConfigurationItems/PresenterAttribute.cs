#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Responsible for specifying the presenting view for a custom <see cref="IConfigurationItem"/>.
/// </summary>
/// <typeparam name="TConfigurationItem">The type of the configuration item to define the presenter for.</typeparam>
[AttributeUsage( AttributeTargets.Class )]
public sealed class PresenterAttribute<TConfigurationItem> : Attribute
	where TConfigurationItem : IConfigurationItem;