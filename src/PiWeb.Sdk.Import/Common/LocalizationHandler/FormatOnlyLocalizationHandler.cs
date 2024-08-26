#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Globalization;

namespace Zeiss.PiWeb.Sdk.Common.LocalizationHandler;

/// <summary>
/// <inheritdoc />
/// This implementation does not translate the input text at all and only applies
/// <see cref="string.Format(IFormatProvider,string,object[])"/>
/// formatting using the invariant culture. 
/// </summary>
public class FormatOnlyLocalizationHandler : ILocalizationHandler
{
    /// <inheritdoc />
    public string LocalizeAndFormatText(string text, object[] args, ILocalizationContext context)
    {
        return string.Format(CultureInfo.InvariantCulture, text, args);
    }
}