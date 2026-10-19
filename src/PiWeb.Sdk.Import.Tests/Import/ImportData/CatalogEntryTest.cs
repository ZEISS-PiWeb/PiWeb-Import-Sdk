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
public class CatalogEntryTest
{
	[Test]
	public void WhenSettingCatalogEntryAttributes_ThenAllPrimitiveValuesAreStored()
	{
		var catalogEntry = new CatalogEntry();
		var dateTime = new DateTime( 2024, 10, 13, 12, 30, 0, DateTimeKind.Utc );

		catalogEntry.SetAttribute( 1, 42 );
		catalogEntry.SetAttribute( 2, 12.5 );
		catalogEntry.SetAttribute( 3, "sample" );
		catalogEntry.SetAttribute( 4, dateTime );

		catalogEntry.AttributeCount.Should().Be( 4 );
		catalogEntry.TryGetAttributeValue( 1, out var integerValue ).Should().BeTrue();
		integerValue.AsInteger().Should().Be( 42 );
		catalogEntry.TryGetAttributeValue( 2, out var doubleValue ).Should().BeTrue();
		doubleValue.AsDouble().Should().Be( 12.5 );
		catalogEntry.TryGetAttributeValue( 3, out var stringValue ).Should().BeTrue();
		stringValue.AsString().Should().Be( "sample" );
		catalogEntry.TryGetAttributeValue( 4, out var dateTimeValue ).Should().BeTrue();
		dateTimeValue.AsDateTime().Should().Be( dateTime );
		catalogEntry.EnumerateAttributes().Should().HaveCount( 4 );
	}

	[Test]
	public void WhenUpdatingAndRemovingCatalogEntryAttributes_ThenTheCountAndValuesStayConsistent()
	{
		var catalogEntry = new CatalogEntry();
		catalogEntry.SetAttribute( 7, 42 );

		catalogEntry.SetAttribute( 7, AttributeValue.From( "updated" ) );

		catalogEntry.AttributeCount.Should().Be( 1 );
		catalogEntry.TryGetAttributeValue( 7, out var updatedValue ).Should().BeTrue();
		updatedValue.AsString().Should().Be( "updated" );
		catalogEntry.RemoveAttribute( 7 ).Should().BeTrue();
		catalogEntry.RemoveAttribute( 7 ).Should().BeFalse();
		catalogEntry.AttributeCount.Should().Be( 0 );
	}

	[Test]
	public void WhenSettingAnotherCatalogEntryAsAnAttribute_ThenAnExceptionIsThrown()
	{
		var catalogEntry = new CatalogEntry();
		var nestedCatalogEntry = new CatalogEntry();

		Assert.Throws<ImportDataException>(
			() => catalogEntry.SetAttribute( 7, AttributeValue.From( nestedCatalogEntry ) ) );

		catalogEntry.AttributeCount.Should().Be( 0 );
	}
}
