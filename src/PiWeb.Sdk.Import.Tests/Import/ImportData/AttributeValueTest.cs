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
public class AttributeValueTest
{
	[Test]
	public void WhenUsingTheDefaultOrStaticNullValue_ThenTheValueIsNull()
	{
		var defaultValue = default( AttributeValue );
		var nullValue = AttributeValue.Null;

		defaultValue.ValueType.Should().Be( AttributeValueType.Null );
		defaultValue.IsNull().Should().BeTrue();
		defaultValue.GetValue().Should().BeNull();
		defaultValue.AsInteger().Should().BeNull();
		defaultValue.AsDouble().Should().BeNull();
		defaultValue.AsString().Should().BeNull();
		defaultValue.AsDateTime().Should().BeNull();
		defaultValue.AsCatalogEntry().Should().BeNull();

		nullValue.ValueType.Should().Be( AttributeValueType.Null );
		nullValue.IsNull().Should().BeTrue();
		nullValue.GetValue().Should().BeNull();
	}

	[Test]
	public void WhenCreatingAnIntegerAttributeValue_ThenOnlyTheIntegerAccessorReturnsAValue()
	{
		var value = AttributeValue.From( 42 );

		value.ValueType.Should().Be( AttributeValueType.Integer );
		value.IsNull().Should().BeFalse();
		value.GetValue().Should().Be( 42 );
		value.AsInteger().Should().Be( 42 );
		value.AsDouble().Should().BeNull();
		value.AsString().Should().BeNull();
		value.AsDateTime().Should().BeNull();
		value.AsCatalogEntry().Should().BeNull();
	}

	[Test]
	public void WhenCreatingADoubleAttributeValue_ThenTheDoubleAccessorReturnsTheValue()
	{
		var value = AttributeValue.From( 12.5 );

		value.ValueType.Should().Be( AttributeValueType.Double );
		value.GetValue().Should().Be( 12.5 );
		value.AsDouble().Should().Be( 12.5 );
		value.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenCreatingAStringAttributeValue_ThenTheStringAccessorReturnsTheValue()
	{
		var value = AttributeValue.From( "sample" );

		value.ValueType.Should().Be( AttributeValueType.String );
		value.GetValue().Should().Be( "sample" );
		value.AsString().Should().Be( "sample" );
		value.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenCreatingADateTimeAttributeValue_ThenItsKindAndValueArePreserved()
	{
		var dateTime = new DateTime( 2024, 10, 13, 12, 30, 0, DateTimeKind.Utc );
		var value = AttributeValue.From( dateTime );

		value.ValueType.Should().Be( AttributeValueType.DateTime );
		value.GetValue().Should().Be( dateTime );
		value.AsDateTime().Should().Be( dateTime );
		value.AsDateTime()!.Value.Kind.Should().Be( DateTimeKind.Utc );
	}

	[Test]
	public void WhenCreatingAnAttributeValueFromAnUnspecifiedDateTime_ThenAnExceptionIsThrown()
	{
		var dateTime = new DateTime( 2024, 10, 13 );

		Assert.Throws<ImportDataException>( () => AttributeValue.From( dateTime ) );
	}

	[Test]
	public void WhenCreatingACatalogEntryAttributeValue_ThenTheCatalogEntryIsPreserved()
	{
		var catalogEntry = new CatalogEntry();
		var value = AttributeValue.From( catalogEntry );

		value.ValueType.Should().Be( AttributeValueType.CatalogEntry );
		value.GetValue().Should().BeSameAs( catalogEntry );
		value.AsCatalogEntry().Should().BeSameAs( catalogEntry );
		value.AsString().Should().BeNull();
	}
}
