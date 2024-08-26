#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents the different types of inspection plan entities.
/// </summary>
public enum InspectionPlanEntityType
{
    /// <summary>
    /// An inspection plan part.
    /// </summary>
    Part,

    /// <summary>
    /// An inspection plan characteristic.
    /// </summary>
    Characteristic
}