#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using AwesomeAssertions;
using NUnit.Framework;
using Zeiss.PiWeb.Sdk.Import.ImportData;

namespace Zeiss.PiWeb.Sdk.Import.Tests.Import.ImportData;

[TestFixture]
public class VariableValueTest
{
	[Test]
	public void WhenUsingTheDefaultOrStaticNullValue_ThenTheValueIsNull()
	{
		var defaultValue = default( VariableValue );
		var nullValue = VariableValue.Null;

		defaultValue.ValueType.Should().Be( VariableValueType.Null );
		defaultValue.IsNull().Should().BeTrue();
		defaultValue.GetValue().Should().BeNull();
		defaultValue.AsInteger().Should().BeNull();
		defaultValue.AsDouble().Should().BeNull();
		defaultValue.AsString().Should().BeNull();
		defaultValue.AsDateTime().Should().BeNull();
		defaultValue.AsCatalogEntry().Should().BeNull();

		nullValue.ValueType.Should().Be( VariableValueType.Null );
		nullValue.IsNull().Should().BeTrue();
		nullValue.GetValue().Should().BeNull();
	}

	[Test]
	public void WhenCreatingAnIntegerVariableValue_ThenOnlyTheIntegerAccessorReturnsAValue()
	{
		var value = VariableValue.From( 42 );

		value.ValueType.Should().Be( VariableValueType.Integer );
		value.IsNull().Should().BeFalse();
		value.GetValue().Should().Be( 42 );
		value.AsInteger().Should().Be( 42 );
		value.AsDouble().Should().BeNull();
		value.AsString().Should().BeNull();
		value.AsDateTime().Should().BeNull();
		value.AsCatalogEntry().Should().BeNull();
	}

	[Test]
	public void WhenCreatingADoubleVariableValue_ThenTheDoubleAccessorReturnsTheValue()
	{
		var value = VariableValue.From( 12.5 );

		value.ValueType.Should().Be( VariableValueType.Double );
		value.GetValue().Should().Be( 12.5 );
		value.AsDouble().Should().Be( 12.5 );
		value.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenCreatingAStringVariableValue_ThenTheStringAccessorReturnsTheValue()
	{
		var value = VariableValue.From( "sample" );

		value.ValueType.Should().Be( VariableValueType.String );
		value.GetValue().Should().Be( "sample" );
		value.AsString().Should().Be( "sample" );
		value.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenCreatingADateTimeVariableValue_ThenItsKindAndValueArePreserved()
	{
		var dateTime = new DateTime( 2024, 10, 13, 12, 30, 0, DateTimeKind.Utc );
		var value = VariableValue.From( dateTime );

		value.ValueType.Should().Be( VariableValueType.DateTime );
		value.GetValue().Should().Be( dateTime );
		value.AsDateTime().Should().Be( dateTime );
		value.AsDateTime()!.Value.Kind.Should().Be( DateTimeKind.Utc );
	}

	[Test]
	public void WhenCreatingAVariableValueFromAnUnspecifiedDateTime_ThenAnExceptionIsThrown()
	{
		var dateTime = new DateTime( 2024, 10, 13 );

		Assert.Throws<ImportDataException>( () => VariableValue.From( dateTime ) );
	}

	[Test]
	public void WhenCreatingACatalogEntryVariableValue_ThenTheCatalogEntryIsPreserved()
	{
		var catalogEntry = new CatalogEntry();
		var value = VariableValue.From( catalogEntry );

		value.ValueType.Should().Be( VariableValueType.CatalogEntry );
		value.GetValue().Should().BeSameAs( catalogEntry );
		value.AsCatalogEntry().Should().BeSameAs( catalogEntry );
		value.AsString().Should().BeNull();
	}
}
