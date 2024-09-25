#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents a measured value to be imported.
/// </summary>
public sealed class MeasuredValue() : Entity(Guid.NewGuid())
{
    #region properties

    /// <inheritdoc />
    public override EntityType EntityType => EntityType.MeasuredValue;

    #endregion
}