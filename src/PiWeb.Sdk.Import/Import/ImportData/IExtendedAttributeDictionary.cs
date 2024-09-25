#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents a collection of attributes extended by catalog valued attributes.
/// </summary>
public interface IExtendedAttributeDictionary : IAttributeDictionary
{
    #region methods

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="catalogEntry">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, CatalogEntry catalogEntry);

    #endregion
}