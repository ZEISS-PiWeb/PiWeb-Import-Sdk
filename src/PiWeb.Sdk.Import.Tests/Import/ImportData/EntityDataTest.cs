#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using FluentAssertions;
using NUnit.Framework;
using Zeiss.PiWeb.Sdk.Import.ImportData;

namespace Zeiss.PiWeb.Sdk.Import.Tests.Import.ImportData;

[TestFixture]
public class EntityDataTest
{
	[Test]
	public void WhenSettingAndRemovingAttributes_ThenTheValuesAreStoredAndUpdatedByKey()
	{
		var measurement = new Measurement();
		var dateTime = new DateTime( 2024, 10, 13, 12, 30, 0, DateTimeKind.Utc );
		measurement.SetAttribute( 1, 42 );
		measurement.SetAttribute( 2, 12.5 );
		measurement.SetAttribute( 3, "sample" );
		measurement.SetAttribute( 4, dateTime );
		var catalogEntry = measurement.SetCatalogEntryAttribute( 5 );
		catalogEntry.SetAttribute( 10, 99 );

		measurement.AttributeCount.Should().Be( 5 );
		measurement.TryGetAttributeValue( 1, out var integerValue ).Should().BeTrue();
		integerValue.AsInteger().Should().Be( 42 );
		measurement.TryGetAttributeValue( 2, out var doubleValue ).Should().BeTrue();
		doubleValue.AsDouble().Should().Be( 12.5 );
		measurement.TryGetAttributeValue( 3, out var stringValue ).Should().BeTrue();
		stringValue.AsString().Should().Be( "sample" );
		measurement.TryGetAttributeValue( 4, out var dateTimeValue ).Should().BeTrue();
		dateTimeValue.AsDateTime().Should().Be( dateTime );
		measurement.TryGetAttributeValue( 5, out var catalogValue ).Should().BeTrue();
		catalogValue.AsCatalogEntry().Should().BeSameAs( catalogEntry );
		measurement.EnumerateAttributes().Should().HaveCount( 5 );

		measurement.SetAttribute( 1, 84 );
		measurement.TryGetAttributeValue( 1, out integerValue ).Should().BeTrue();
		integerValue.AsInteger().Should().Be( 84 );
		measurement.AttributeCount.Should().Be( 5 );
		measurement.RemoveAttribute( 1 ).Should().BeTrue();
		measurement.RemoveAttribute( 1 ).Should().BeFalse();
		measurement.AttributeCount.Should().Be( 4 );
	}

	[Test]
	public void WhenSettingAndRemovingVariables_ThenNamesAreComparedOrdinally()
	{
		var measurement = new Measurement();
		var dateTime = new DateTime( 2024, 10, 13, 12, 30, 0, DateTimeKind.Utc );
		measurement.SetVariable( "number", 42 );
		measurement.SetVariable( "ratio", 12.5 );
		measurement.SetVariable( "label", "sample" );
		measurement.SetVariable( "timestamp", dateTime );
		var catalogEntry = measurement.SetCatalogEntryVariable( "entry" );
		catalogEntry.SetAttribute( 10, 99 );
		measurement.SetVariable( "Name", 1 );
		measurement.SetVariable( "name", 2 );

		measurement.VariableCount.Should().Be( 7 );
		measurement.TryGetVariableValue( "number", out var integerValue ).Should().BeTrue();
		integerValue.AsInteger().Should().Be( 42 );
		measurement.TryGetVariableValue( "ratio", out var doubleValue ).Should().BeTrue();
		doubleValue.AsDouble().Should().Be( 12.5 );
		measurement.TryGetVariableValue( "label", out var stringValue ).Should().BeTrue();
		stringValue.AsString().Should().Be( "sample" );
		measurement.TryGetVariableValue( "timestamp", out var dateTimeValue ).Should().BeTrue();
		dateTimeValue.AsDateTime().Should().Be( dateTime );
		measurement.TryGetVariableValue( "entry", out var catalogValue ).Should().BeTrue();
		catalogValue.AsCatalogEntry().Should().BeSameAs( catalogEntry );
		measurement.TryGetVariableValue( "Name", out var upperCaseValue ).Should().BeTrue();
		upperCaseValue.AsInteger().Should().Be( 1 );
		measurement.TryGetVariableValue( "name", out var lowerCaseValue ).Should().BeTrue();
		lowerCaseValue.AsInteger().Should().Be( 2 );
		measurement.EnumerateVariables().Should().HaveCount( 7 );

		measurement.SetVariable( "number", 84 );
		measurement.TryGetVariableValue( "number", out integerValue ).Should().BeTrue();
		integerValue.AsInteger().Should().Be( 84 );
		measurement.VariableCount.Should().Be( 7 );
		measurement.RemoveVariable( "Name" ).Should().BeTrue();
		measurement.RemoveVariable( "Name" ).Should().BeFalse();
		measurement.ContainsVariable( "name" ).Should().BeTrue();
		measurement.VariableCount.Should().Be( 6 );
	}
}
