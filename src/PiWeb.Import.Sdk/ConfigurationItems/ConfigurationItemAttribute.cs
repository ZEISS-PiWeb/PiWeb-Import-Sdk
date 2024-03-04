#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System;
using Modules.ImportAutomation;

/// <summary>
/// Marks a field or a property of a class that implements <see cref="IAutomationConfiguration"/>
/// as an configuration item displayed for an import automation.
/// </summary>
[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
public sealed class ConfigurationItemAttribute : Attribute;