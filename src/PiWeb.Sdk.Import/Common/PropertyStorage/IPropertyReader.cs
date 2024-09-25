#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Zeiss.PiWeb.Sdk.Common.PropertyStorage;

/// <summary>
/// Represents a simple reader for a key value storage.
/// </summary>
public interface IPropertyReader
{
	/// <summary>
	/// Returns an enumeration of all stored keys.
	/// </summary>
	IEnumerable<string> GetKeys();

	/// <summary>
	/// Reads the string value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="value">
	/// Receives the value of the property associated with the specified key. If the property does not exist, this parameter receives
	/// the <c>null</c> value.
	/// </param>
	/// <returns>
	/// <code>true</code> if the property associated with the specified <paramref name="key"/> exists; otherwise, false.
	/// </returns>
	bool TryReadString( string key, [MaybeNullWhen( false )] out string value );

	/// <summary>
	/// Reads the string value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="defaultValue">The default value to return when the property does not exist.</param>
	/// <returns>
	/// The numeric value of the specified property. If the property does not exist, <paramref name="defaultValue"/> will be returned.
	/// </returns>
	[return: NotNullIfNotNull( nameof( defaultValue ) )]
	string? ReadString( string key, string? defaultValue )
	{
		return TryReadString( key, out var value )
			? value
			: defaultValue;
	}

	/// <summary>
	/// Reads the string value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <returns>
	/// The numeric value of the specified property. If the property does not exist, the empty string will be returned.
	/// </returns>
	string ReadString( string key )
	{
		return TryReadString( key, out var value )
			? value
			: string.Empty;
	}

	/// <summary>
	/// Reads the enum value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="value">
	/// Receives the value of the property associated with the specified key. If the property does not exist or has no valid enum value,
	/// this parameter receives the enum value that corresponds to 0.
	/// </param>
	/// <typeparam name="TNumber">The type of the numeric value to read.</typeparam>
	/// <returns>
	/// <c>true</c> if the property associated with the specified <paramref name="key"/> exists; otherwise, <c>false</c>.
	/// </returns>
	bool TryReadNumber<TNumber>( string key, [MaybeNullWhen(false)] out TNumber value )
		where TNumber : INumber<TNumber>
	{
		if( TryReadString( key, out var stringValue )
			&& TNumber.TryParse( stringValue, CultureInfo.InvariantCulture, out value ) )
			return true;

		value = default;
		return false;
	}

	/// <summary>
	/// Reads the numeric value of the property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="defaultValue">The default value to return when the property does not exist or has no valid numeric value.</param>
	/// <typeparam name="TNumber">The type of the numeric value to read.</typeparam>
	/// <returns>
	/// The numeric value of the specified property. If the property does not exist or has no valid numeric value,
	/// <paramref name="defaultValue"/> will be returned.
	/// </returns>
	TNumber ReadNumber<TNumber>( string key, TNumber defaultValue = default )
		where TNumber : struct, INumber<TNumber>
	{
		return TryReadNumber<TNumber>( key, out var value )
			? value
			: defaultValue;
	}

	/// <summary>
	/// Reads the enum value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="value">
	/// Receives the value of the property associated with the specified key. If the property does not exist or has no valid enum value,
	/// this parameter receives the enum value that corresponds to 0.
	/// </param>
	/// <typeparam name="TEnum">The type of the enum to read.</typeparam>
	/// <returns>
	/// <c>true</c> if the property associated with the specified <paramref name="key"/> exists; otherwise, <c>false</c>.
	/// </returns>
	bool TryReadEnum<TEnum>( string key, out TEnum value )
		where TEnum : struct, Enum
	{
		if( TryReadString( key, out var stringValue ) && Enum.TryParse( stringValue, true, out value ) )
			return true;

		value = default;
		return false;
	}

	/// <summary>
	/// Reads the enum value of the property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="defaultValue">The default value to return when the property does not exist or has no valid enum value.</param>
	/// <typeparam name="TEnum">The type of the enum to read.</typeparam>
	/// <returns>
	/// The enum value of the specified property. If the property does not exist or has no valid enum value,
	/// <paramref name="defaultValue"/> will be returned.
	/// </returns>
	TEnum ReadEnum<TEnum>( string key, TEnum defaultValue = default )
		where TEnum : struct, Enum
	{
		return TryReadEnum<TEnum>( key, out var value )
			? value
			: defaultValue;
	}

	/// <summary>
	/// Reads the boolean value of a property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="value">
	/// Receives the value of the property associated with the specified key. If the property does not exist or has no valid boolean value,
	/// this parameter receives the value <c>false</c>.
	/// </param>
	/// <returns>
	/// <c>true</c> if the property associated with the specified <paramref name="key"/> exists; otherwise, <c>false</c>.
	/// </returns>
	bool TryReadBool( string key, out bool value )
	{
		if( TryReadString( key, out var stringValue ) && bool.TryParse( stringValue, out value ) )
			return true;

		value = default;
		return false;
	}

	/// <summary>
	/// Reads the boolean value of the property associated with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to read.</param>
	/// <param name="defaultValue">The default value to return when the property does not exist or has no valid boolean value.</param>
	/// <returns>
	/// The boolean value of the specified property. If the property does not exist or has no valid boolean value,
	/// <paramref name="defaultValue"/> will be returned.
	/// </returns>
	bool ReadBool( string key, bool defaultValue = default )
	{
		return TryReadBool( key, out var value )
			? value
			: defaultValue;
	}
}