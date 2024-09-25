#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents the different types of entities.
/// </summary>
public enum EntityType
{
    /// <summary>
    /// An inspection plan part.
    /// </summary>
    Part,

    /// <summary>
    /// An inspection plan characteristic.
    /// </summary>
    Characteristic,

    /// <summary>
    /// A measurement.
    /// </summary>
    Measurement,

    /// <summary>
    /// A measured value.
    /// </summary>
    MeasuredValue
}