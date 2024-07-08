#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Specifies which catalog values are used to map a value to a catalog entry.
/// </summary>
public enum CatalogMappingType
{
    /// <summary>The catalog index is used to map a value to a catalog entry.</summary>
    Index,
    
    /// <summary>The values of a specific catalog column are used to map a value to a catalog entry.</summary>
    Column
}