#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// The possible mapping targets of a mapping rule.
/// </summary>
public enum MappingTarget
{
    /// <summary>
    /// Any inspection plan part.
    /// </summary>
    Part,
    
    /// <summary>
    /// An inspection plan part with child parts.
    /// </summary>
    GroupPart,
    
    /// <summary>
    /// An inspection plan part with child characteristics.
    /// </summary>
    PartWithCharacteristics,
    
    /// <summary>
    /// Any inspection plan characteristic.
    /// </summary>
    Characteristic,
    
    /// <summary>
    /// An inspection plan characteristic with a parent part and no children.
    /// </summary>
    DirectCharacteristic,

    /// <summary>
    /// An inspection plan characteristic with child characteristics.
    /// </summary>
    GroupCharacteristic,

    /// <summary>
    /// An inspection plan characteristic with a parent characteristic and no children.
    /// </summary>
    SubCharacteristic,
    
    /// <summary>
    /// Any measurement.
    /// </summary>
    Measurement,
    
    /// <summary>
    /// Any measured values.
    /// </summary>
    MeasuredValue
}