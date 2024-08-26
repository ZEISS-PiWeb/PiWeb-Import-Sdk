#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Globalization;
using System.Numerics;

namespace Zeiss.PiWeb.Sdk.Common.PropertyStorage;

/// <summary>
/// Represents a simple key value storage.
/// </summary>
public interface IPropertyStorage : IPropertyObserver, IDisposable
{
	/// <summary>
	/// Gets a value indicating whether the storage can be updated.
	/// </summary>
	public bool IsReadOnly { get; }

	#region methods

	/// <summary>
	/// Writes the specified <paramref name="key"/> and <paramref name="value"/> to the property storage overwriting existing values with
	/// the same <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to write.</param>
	/// <param name="value">The value of the property to write.</param>
	public bool WriteString( string key, string value );

	/// <summary>
	/// Writes the specified <paramref name="key"/> and numeric <paramref name="value"/> to the property storage overwriting existing values
	/// with the same <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to write.</param>
	/// <param name="value">The numeric value of the property to write.</param>
	/// <typeparam name="TNumber">The type of the numeric value to write.</typeparam>
	public bool WriteNumber<TNumber>( string key, TNumber value )
		where TNumber : INumber<TNumber>
	{
		return WriteString( key, value.ToString( null, CultureInfo.InvariantCulture ) );
	}

	/// <summary>
	/// Writes the specified <paramref name="key"/> and enum <paramref name="value"/> to the property storage overwriting existing values
	/// with the same <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to write.</param>
	/// <param name="value">The enum value of the property to write.</param>
	/// <typeparam name="TEnum">The type of the enum value to write.</typeparam>
	public bool WriteEnum<TEnum>( string key, TEnum value )
		where TEnum : struct, Enum
	{
		return WriteString( key, value.ToString() );
	}

	/// <summary>
	/// Writes the specified <paramref name="key"/> and boolean <paramref name="value"/> to the property storage overwriting existing values
	/// with the same <paramref name="key"/>.
	/// </summary>
	/// <param name="key">The key of the property to write.</param>
	/// <param name="value">The boolean value of the property to write.</param>
	public bool WriteBool( string key, bool value )
	{
		return WriteString( key, value.ToString() );
	}

	/// <summary>
	/// Removes the property associated with the specified <paramref name="key"/> from the property storage.
	/// </summary>
	/// <param name="key">The key of the property to remove.</param>
	public bool Remove( string key );

	#endregion
}