#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Zeiss.PiWeb.Import.Sdk.ImportData;

namespace Zeiss.PiWeb.Import.Sdk.Tests.ImportData;

[TestFixture]
public class MeasurementTest
{
	#region methods

	[Test]
	public void WhenCheckingHasMeasuredValueAndTheMeasuredValueExists_ThenTrueIsReturned()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );

		// when
		var opResult = measurement.ContainsMeasuredValue( char1 );

		// then
		opResult.Should().BeTrue();
	}

	[Test]
	public void WhenCheckingHasMeasuredValueAndTheMeasuredValueDoesNotExist_ThenFalseIsReturned()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );

		// when
		var opResult = measurement.ContainsMeasuredValue( char2 );

		// then
		opResult.Should().BeFalse();
	}

	[Test]
	public void WhenGettingMeasuredValueAndTheMeasuredValueExists_ThenTheMeasuredValueIsReturned()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		var mv1 = measurement.AddMeasuredValue( char1 );

		// when
		var opResult = measurement.TryGetMeasuredValue( char1, out var resultValue );

		// then
		opResult.Should().BeTrue();
		resultValue.Should().NotBeNull();
		resultValue.Should().BeSameAs( mv1 );
	}

	[Test]
	public void WhenGettingMeasuredValueAndTheMeasuredValueDoesNotExist_ThenTheMeasuredValueIsNotReturned()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );

		// when
		var opResult = measurement.TryGetMeasuredValue( char2, out var resultValue );

		// then
		opResult.Should().BeFalse();
		resultValue.Should().BeNull();
	}

	[Test]
	public void WhenAddingAMeasuredValue_ThenTheMeasuredValueIsAdded()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		var newMeasuredValue = new MeasuredValue();

		// when
		var opResult = measurement.AddMeasuredValue( char1, newMeasuredValue );

		// then
		opResult.Should().BeSameAs( newMeasuredValue );
		measurement.MeasuredValueCount.Should().Be( 1 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeTrue();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeTrue();
		outResult.Should().BeSameAs( newMeasuredValue );
	}

	[Test]
	public void WhenAddingAMeasuredValueForACharacteristicOfADifferentPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var part1 = new InspectionPlanPart( "part1" );
		var char1 = part1.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();

		// when, then
		measurement.Invoking( m => m.AddMeasuredValue( char1 ) )
			.Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingAMeasuredValueForACharacteristicThatAlreadyHasAMeasuredValue_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );

		// when, then
		measurement.Invoking( m => m.AddMeasuredValue( char1 ) )
			.Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenUpdatingAMeasuredValue_ThenTheMeasuredValueIsInserted()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		var newMeasuredValue = new MeasuredValue();

		// when
		var opResult = measurement.SetMeasuredValue( char1, newMeasuredValue );

		// then
		opResult.Should().BeSameAs( newMeasuredValue );
		measurement.MeasuredValueCount.Should().Be( 1 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeTrue();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeTrue();
		outResult.Should().BeSameAs( newMeasuredValue );
	}

	[Test]
	public void WhenUpdatingAMeasuredValueForACharacteristicOfADifferentPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var part1 = new InspectionPlanPart( "part1" );
		var char1 = part1.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();

		// when, then
		measurement.Invoking( m => m.SetMeasuredValue( char1 ) )
			.Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenUpdatingAMeasuredValueForACharacteristicThatAlreadyHasAMeasuredValue_ThenTheMeasuredValueIsReplaced()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );

		// when
		var newValue = new MeasuredValue();
		var opResult = measurement.SetMeasuredValue( char1, newValue );

		// then
		opResult.Should().BeSameAs( newValue );
		measurement.MeasuredValueCount.Should().Be( 1 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeTrue();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeTrue();
		outResult.Should().BeSameAs( newValue );
	}

	[Test]
	public void WhenRemovingAMeasuredValueAndTheMeasuredValueExists_ThenTheMeasuredValueIsRemoved()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );
		measurement.AddMeasuredValue( char2 );

		// when
		var opResult = measurement.RemoveMeasuredValue( char1 );

		// then
		opResult.Should().BeTrue();
		measurement.MeasuredValueCount.Should().Be( 1 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeFalse();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeFalse();
		outResult.Should().BeNull();
		measurement.ContainsMeasuredValue( char2 ).Should().BeTrue();
	}

	[Test]
	public void WhenRemovingAMeasuredValueAndTheMeasuredValueDoesNotExist_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();

		// when
		var opResult = measurement.RemoveMeasuredValue( char1 );

		// then
		opResult.Should().BeFalse();
		measurement.MeasuredValueCount.Should().Be( 0 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeFalse();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeFalse();
		outResult.Should().BeNull();
	}

	[Test]
	public void WhenClearingMeasuredValues_ThenAllMeasuredValueAreRemoved()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );

		var measurement = root.AddMeasurement();
		measurement.AddMeasuredValue( char1 );
		measurement.AddMeasuredValue( char2 );

		// when
		measurement.ClearMeasuredValues();

		// then
		measurement.MeasuredValueCount.Should().Be( 0 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeFalse();
		measurement.TryGetMeasuredValue( char1, out var outResult1 ).Should().BeFalse();
		outResult1.Should().BeNull();
		measurement.ContainsMeasuredValue( char2 ).Should().BeFalse();
		measurement.TryGetMeasuredValue( char2, out var outResult2 ).Should().BeFalse();
		outResult2.Should().BeNull();
	}

	[Test]
	public void WhenNotifyingPartChangedButPartHasNotChanged1_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var part1 = new InspectionPlanPart( "part1" );

		var measurement = root.AddMeasurement();

		// when
		measurement.NotifyPartChanged( part1 );

		// then
		measurement.Part.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingPartChangedButPartHasNotChanged2_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanPart( "root" );

		var measurement = root.AddMeasurement();

		// when
		measurement.NotifyPartChanged( null );

		// then
		measurement.Part.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingPartChangedButPartIsTheSame_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanPart( "root" );

		var measurement = root.AddMeasurement();

		// when
		measurement.NotifyPartChanged( root );

		// then
		measurement.Part.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingCharacteristicRemovedButCharacteristicHasNotBeenRemoved_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var measurement = root.AddMeasurement();
		var mv1 = measurement.AddMeasuredValue( char1 );

		// when
		measurement.NotifyCharacteristicRemoved( char1 );

		// then
		measurement.MeasuredValueCount.Should().Be( 1 );
		measurement.ContainsMeasuredValue( char1 ).Should().BeTrue();
		measurement.TryGetMeasuredValue( char1, out var outResult ).Should().BeTrue();
		outResult.Should().BeSameAs( mv1 );
	}

	[Test]
	public void WhenSettingANewAttributeValue_ThenTheValueIsSet()
	{
		// given
		var measurement = new Measurement();

		// when
		measurement.SetAttribute( 4, "Some Text" );

		// then
		measurement.AttributeCount.Should().Be( 1 );
		measurement.ContainsAttribute( 4 ).Should().Be( true );
		measurement.TryGetAttributeValue( 4, out var resultAttributeValue ).Should().BeTrue();
		resultAttributeValue.IsNull().Should().BeFalse();
		resultAttributeValue.AsString().Should().Be( "Some Text" );
		resultAttributeValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenSettingAnAttributeValueForExistingAttribute_ThenTheValueIsReplaced()
	{
		// given
		var measurement = new Measurement();
		measurement.SetAttribute( 4, 5 );

		// when
		measurement.SetAttribute( 4, "Some Text" );

		// then
		measurement.AttributeCount.Should().Be( 1 );
		measurement.ContainsAttribute( 4 ).Should().Be( true );
		measurement.TryGetAttributeValue( 4, out var resultAttributeValue ).Should().BeTrue();
		resultAttributeValue.IsNull().Should().BeFalse();
		resultAttributeValue.AsString().Should().Be( "Some Text" );
		resultAttributeValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenRemovingAnExistingAttribute_ThenTheAttributeIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.SetAttribute( 4, 5 );

		// when
		var opResult = measurement.RemoveAttribute( 4 );

		// then
		opResult.Should().BeTrue();
		measurement.AttributeCount.Should().Be( 0 );
		measurement.ContainsAttribute( 4 ).Should().Be( false );
		measurement.TryGetAttributeValue( 4, out var resultAttributeValue ).Should().BeFalse();
		resultAttributeValue.IsNull().Should().BeTrue();
		resultAttributeValue.AsString().Should().BeNull();
		resultAttributeValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenRemovingANonExistingAttribute_ThenNothingIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.SetAttribute( 4, 5 );

		// when
		var opResult = measurement.RemoveAttribute( 200 );

		// then
		opResult.Should().BeFalse();
		measurement.AttributeCount.Should().Be( 1 );
		measurement.ContainsAttribute( 4 ).Should().Be( true );
		measurement.TryGetAttributeValue( 4, out var resultAttributeValue ).Should().BeTrue();
		resultAttributeValue.IsNull().Should().BeFalse();
		resultAttributeValue.AsString().Should().BeNull();
		resultAttributeValue.AsInteger().Should().Be( 5 );
	}

	[Test]
	public void WhenGettingExistingAttributeValue_ThenTheValueIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.SetAttribute( 4, 5 );

		// when
		var opResult = measurement.TryGetAttributeValue( 4, out var resultValue );

		// then
		opResult.Should().BeTrue();
		resultValue.IsNull().Should().BeFalse();
		resultValue.AsString().Should().BeNull();
		resultValue.AsInteger().Should().Be( 5 );
	}

	[Test]
	public void WhenGettingNonExistingAttributeValue_ThenNoValueIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.SetAttribute( 4, 5 );

		// when
		var opResult = measurement.TryGetAttributeValue( 5, out var resultValue );

		// then
		opResult.Should().BeFalse();
		resultValue.IsNull().Should().BeTrue();
		resultValue.AsString().Should().BeNull();
		resultValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenGettingAllAttributes_ThenAllAttributesAreReturned()
	{
		// given
		var timestamp = DateTime.UtcNow;

		var measurement = new Measurement();
		measurement.SetAttribute( 4, "Hallo" );
		measurement.SetAttribute( 20, 5 );
		measurement.SetAttribute( 16, 5.3 );
		measurement.SetAttribute( 2025, timestamp );

		var catalogEntry = measurement.SetCatalogEntryAttribute( 3000 );
		catalogEntry.SetAttribute( 22000, 5 );

		// when
		var resultAttributes = measurement.EnumerateAttributes().ToArray();

		// then
		resultAttributes.Should().HaveCount( 5 );
		foreach( var attributePair in resultAttributes )
		{
			switch( attributePair.Key )
			{
				case 4:
					attributePair.Value.ValueType.Should().Be( AttributeValueType.String );
					attributePair.Value.GetValue().Should().Be( "Hallo" );
					attributePair.Value.AsString().Should().Be( "Hallo" );
					attributePair.Value.AsDateTime().Should().BeNull();
					attributePair.Value.AsDouble().Should().BeNull();
					attributePair.Value.AsInteger().Should().BeNull();
					break;

				case 20:
					attributePair.Value.ValueType.Should().Be( AttributeValueType.Integer );
					attributePair.Value.GetValue().Should().Be( 5 );
					attributePair.Value.AsString().Should().BeNull();
					attributePair.Value.AsInteger().Should().Be( 5 );
					attributePair.Value.AsDateTime().Should().BeNull();
					attributePair.Value.AsDouble().Should().BeNull();
					break;

				case 16:
					attributePair.Value.ValueType.Should().Be( AttributeValueType.Double );
					attributePair.Value.GetValue().Should().Be( 5.3 );
					attributePair.Value.AsString().Should().BeNull();
					attributePair.Value.AsDouble().Should().Be( 5.3 );
					attributePair.Value.AsDateTime().Should().BeNull();
					attributePair.Value.AsInteger().Should().BeNull();
					break;

				case 2025:
					attributePair.Value.ValueType.Should().Be( AttributeValueType.DateTime );
					attributePair.Value.GetValue().Should().Be( timestamp );
					attributePair.Value.AsString().Should().BeNull();
					attributePair.Value.AsDateTime().Should().Be( timestamp );
					attributePair.Value.AsDouble().Should().BeNull();
					attributePair.Value.AsInteger().Should().BeNull();
					break;

				case 3000:
					attributePair.Value.ValueType.Should().Be( AttributeValueType.CatalogEntry );
					attributePair.Value.GetValue().Should().BeSameAs( catalogEntry );
					attributePair.Value.AsString().Should().BeNull();
					attributePair.Value.AsDateTime().Should().BeNull();
					attributePair.Value.AsDouble().Should().BeNull();
					attributePair.Value.AsInteger().Should().BeNull();
					attributePair.Value.AsCatalogEntry().Should().BeSameAs( catalogEntry );
					attributePair.Value.AsCatalogEntry().Should().NotBeNull();
					attributePair.Value.AsCatalogEntry()!.AttributeCount.Should().Be( 1 );
					attributePair.Value.AsCatalogEntry()!.ContainsAttribute( 22000 ).Should().BeTrue();
					attributePair.Value.AsCatalogEntry()!.TryGetAttributeValue( 22000, out var resultValue ).Should().BeTrue();
					resultValue.AsInteger().Should().Be( 5 );
					break;
			}
		}
	}

	[Test]
	public void WhenSettingANewVariableValue_ThenTheValueIsSet()
	{
		// given
		var measurement = new Measurement();

		// when
		measurement.SetVariable( "foo", "Some Text" );

		// then
		measurement.VariableCount.Should().Be( 1 );
		measurement.ContainsVariable( "foo" ).Should().Be( true );
		measurement.TryGetVariableValue( "foo", out var resultVariableValue ).Should().BeTrue();
		resultVariableValue.IsNull().Should().BeFalse();
		resultVariableValue.AsString().Should().Be( "Some Text" );
		resultVariableValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenSettingAVariableValueForAnExistingVariable_ThenTheValueIsReplaced()
	{
		// given
		var measurement = new Measurement();
		measurement.SetVariable( "foo", 5 );

		// when
		measurement.SetVariable( "foo", "Some Text" );

		// then
		measurement.VariableCount.Should().Be( 1 );
		measurement.ContainsVariable( "foo" ).Should().Be( true );
		measurement.TryGetVariableValue( "foo", out var resultVariableValue ).Should().BeTrue();
		resultVariableValue.IsNull().Should().BeFalse();
		resultVariableValue.AsString().Should().Be( "Some Text" );
		resultVariableValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenRemovingAnExistingVariable_ThenTheVariableIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.SetVariable( "foo", 5 );

		// when
		var opResult = measurement.RemoveVariable( "foo" );

		// then
		opResult.Should().BeTrue();
		measurement.VariableCount.Should().Be( 0 );
		measurement.ContainsVariable( "foo" ).Should().Be( false );
		measurement.TryGetVariableValue( "foo", out var resultVariableValue ).Should().BeFalse();
		resultVariableValue.IsNull().Should().BeTrue();
		resultVariableValue.AsString().Should().BeNull();
		resultVariableValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenRemovingANonExistingVariable_ThenNothingIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.SetVariable( "foo", 5 );

		// when
		var opResult = measurement.RemoveVariable( "bar" );

		// then
		opResult.Should().BeFalse();
		measurement.VariableCount.Should().Be( 1 );
		measurement.ContainsVariable( "foo" ).Should().Be( true );
		measurement.TryGetVariableValue( "foo", out var resultVariableValue ).Should().BeTrue();
		resultVariableValue.IsNull().Should().BeFalse();
		resultVariableValue.AsString().Should().BeNull();
		resultVariableValue.AsInteger().Should().Be( 5 );
	}

	[Test]
	public void WhenGettingExistingVariableValue_ThenTheValueIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.SetVariable( "foo", 5 );

		// when
		var opResult = measurement.TryGetVariableValue( "foo", out var resultValue );

		// then
		opResult.Should().BeTrue();
		resultValue.IsNull().Should().BeFalse();
		resultValue.AsString().Should().BeNull();
		resultValue.AsInteger().Should().Be( 5 );
	}

	[Test]
	public void WhenGettingNonExistingVariableValue_ThenNoValueIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.SetVariable( "foo", 5 );

		// when
		var opResult = measurement.TryGetVariableValue( "bar", out var resultValue );

		// then
		opResult.Should().BeFalse();
		resultValue.IsNull().Should().BeTrue();
		resultValue.AsString().Should().BeNull();
		resultValue.AsInteger().Should().BeNull();
	}

	[Test]
	public void WhenGettingAllVariables_ThenAllVariablesAreReturned()
	{
		// given
		var timestamp = DateTime.UtcNow;

		var measurement = new Measurement();
		measurement.SetVariable( "foo1", "Hallo" );
		measurement.SetVariable( "foo3", 5 );
		measurement.SetVariable( "foo4", 5.3 );
		measurement.SetVariable( "foo5", timestamp );

		var catalogEntry = measurement.SetCatalogEntryVariable( "foo6" );
		catalogEntry.SetAttribute( 22000, 5 );

		// when
		var resultVariables = measurement.EnumerateVariables().ToArray();

		// then
		resultVariables.Should().HaveCount( 5 );
		foreach( var variablePair in resultVariables )
		{
			switch( variablePair.Key )
			{
				case "foo1":
					variablePair.Value.ValueType.Should().Be( VariableValueType.String );
					variablePair.Value.GetValue().Should().Be( "Hallo" );
					variablePair.Value.AsString().Should().Be( "Hallo" );
					variablePair.Value.AsDateTime().Should().BeNull();
					variablePair.Value.AsDouble().Should().BeNull();
					variablePair.Value.AsInteger().Should().BeNull();
					variablePair.Value.AsCatalogEntry().Should().BeNull();
					break;

				case "foo3":
					variablePair.Value.ValueType.Should().Be( VariableValueType.Integer );
					variablePair.Value.GetValue().Should().Be( 5 );
					variablePair.Value.AsString().Should().BeNull();
					variablePair.Value.AsInteger().Should().Be( 5 );
					variablePair.Value.AsDateTime().Should().BeNull();
					variablePair.Value.AsDouble().Should().BeNull();
					variablePair.Value.AsCatalogEntry().Should().BeNull();
					break;

				case "foo4":
					variablePair.Value.ValueType.Should().Be( VariableValueType.Double );
					variablePair.Value.GetValue().Should().Be( 5.3 );
					variablePair.Value.AsString().Should().BeNull();
					variablePair.Value.AsDouble().Should().Be( 5.3 );
					variablePair.Value.AsDateTime().Should().BeNull();
					variablePair.Value.AsInteger().Should().BeNull();
					variablePair.Value.AsCatalogEntry().Should().BeNull();
					break;

				case "foo5":
					variablePair.Value.ValueType.Should().Be( VariableValueType.DateTime );
					variablePair.Value.GetValue().Should().Be( timestamp );
					variablePair.Value.AsString().Should().BeNull();
					variablePair.Value.AsDateTime().Should().Be( timestamp );
					variablePair.Value.AsDouble().Should().BeNull();
					variablePair.Value.AsInteger().Should().BeNull();
					variablePair.Value.AsCatalogEntry().Should().BeNull();
					break;

				case "foo6":
					variablePair.Value.ValueType.Should().Be( VariableValueType.CatalogEntry );
					variablePair.Value.GetValue().Should().BeSameAs( catalogEntry );
					variablePair.Value.AsString().Should().BeNull();
					variablePair.Value.AsDateTime().Should().BeNull();
					variablePair.Value.AsDouble().Should().BeNull();
					variablePair.Value.AsInteger().Should().BeNull();
					variablePair.Value.AsCatalogEntry().Should().BeSameAs( catalogEntry );
					variablePair.Value.AsCatalogEntry().Should().NotBeNull();
					variablePair.Value.AsCatalogEntry()!.AttributeCount.Should().Be( 1 );
					variablePair.Value.AsCatalogEntry()!.ContainsAttribute( 22000 ).Should().BeTrue();
					variablePair.Value.AsCatalogEntry()!.TryGetAttributeValue( 22000, out var resultValue ).Should().BeTrue();
					resultValue.AsInteger().Should().Be( 5 );
					break;
			}
		}
	}

	[Test]
	public void WhenDeterminingAdditionalDataCount_ThenTheCorrectNumberIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		var count = measurement.AdditionalDataCount;

		// then
		count.Should().Be( 2 );
	}

	[Test]
	public void WhenDeterminingWhetherAdditionalDataExistsAndTheDataExists_ThenTrueIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		var result = measurement.ContainsAdditionalData( "A.txt" );

		// then
		result.Should().BeTrue();
	}

	[Test]
	public void WhenDeterminingWhetherAdditionalDataExistsAndTheDataDoesNotExist_ThenFalseIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		var result = measurement.ContainsAdditionalData( "Z.txt" );

		// then
		result.Should().BeFalse();
	}

	[Test]
	public void WhenTryingToGetAdditionalDataAndTheDataExists_ThenTrueIsReturned()
	{
		// given
		var measurement = new Measurement();
		var aData = measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		var result = measurement.TryGetAdditionalData( "A.txt", out var existingAdditionalData );

		// then
		result.Should().BeTrue();
		existingAdditionalData.Should().BeSameAs( aData );
	}

	[Test]
	public void WhenTryingToGetAdditionalDataAndTheDataDoesNotExist_ThenFalseIsReturned()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		var result = measurement.TryGetAdditionalData( "Z.txt", out var existingAdditionalData );

		// then
		result.Should().BeFalse();
		existingAdditionalData.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 3 )]
	[TestCase( 20 )]
	public void WhenEnumeratingAdditionalData_ThenAllDataIsReturned( int additionalDataCount )
	{
		// given
		var measurement = new Measurement();
		var expectedData = new List<AdditionalDataItem>();
		for( var i = 1; i <= additionalDataCount; ++i )
			expectedData.Add( measurement.AddAdditionalData( i + ".txt", new MemoryStream() ) );

		// when
		var allData = measurement.EnumerateAdditionalData().ToList();

		// then
		allData.Should().HaveSameCount( expectedData );
		allData.Should().BeSubsetOf( expectedData );
	}

	[Test]
	public void WhenAddingAdditionalDataAndTheDataDoesNotExist_ThenTheDataIsAdded()
	{
		// given
		var measurement = new Measurement();

		// when
		var aData = measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		var bData = measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// then
		measurement.AdditionalDataCount.Should().Be( 2 );
		measurement.ContainsAdditionalData( "A.txt" ).Should().BeTrue();
		measurement.TryGetAdditionalData( "A.txt", out var receivedAData ).Should().BeTrue();
		receivedAData.Should().BeSameAs( aData );
		measurement.ContainsAdditionalData( "B.txt" ).Should().BeTrue();
		measurement.TryGetAdditionalData( "B.txt", out var receivedBData ).Should().BeTrue();
		receivedBData.Should().BeSameAs( bData );
	}

	[Test]
	public void WhenAddingAdditionalDataAndTheDataExists_ThenAnExceptionIsThrown()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );

		// when, then
		measurement.Invoking( m => m.AddAdditionalData( "A.txt", new MemoryStream() ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenSettingAdditionalDataAndTheDataDoesNotExist_ThenTheDataIsAdded()
	{
		// given
		var measurement = new Measurement();

		// when
		var aData = measurement.SetAdditionalData( "A.txt", new MemoryStream(), true, out var replacedAData );
		var bData = measurement.SetAdditionalData( "B.txt", new MemoryStream(), true, out var replacedBData );

		// then
		replacedAData.Should().BeNull();
		measurement.ContainsAdditionalData( aData.Name ).Should().BeTrue();
		measurement.TryGetAdditionalData( aData.Name, out var receivedAData ).Should().BeTrue();
		receivedAData.Should().BeSameAs( aData );

		replacedBData.Should().BeNull();
		measurement.ContainsAdditionalData( bData.Name ).Should().BeTrue();
		measurement.TryGetAdditionalData( bData.Name, out var receivedBData ).Should().BeTrue();
		receivedBData.Should().BeSameAs( bData );
	}

	[Test]
	public void WhenSettingAdditionalDataAndTheDataExists_ThenExistingDataIsReplaced()
	{
		// given
		var measurement = new Measurement();
		var aData = measurement.AddAdditionalData( "A.txt", new MemoryStream() );

		// when
		var newAData = measurement.SetAdditionalData( "A.txt", new MemoryStream(), true, out var replacedAData );

		// then
		newAData.Should().NotBeSameAs( aData );
		replacedAData.Should().BeSameAs( aData );
		measurement.ContainsAdditionalData( newAData.Name ).Should().BeTrue();
		measurement.TryGetAdditionalData( newAData.Name, out var receivedAData ).Should().BeTrue();
		receivedAData.Should().BeSameAs( newAData );
	}

	[Test]
	public void WhenRemovingAdditionalDataAndTheDataExists_ThenExistingDataIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );

		// when
		var result = measurement.RemoveAdditionalData( "A.txt" );

		// then
		result.Should().BeTrue();
		measurement.ContainsAdditionalData( "A.txt" ).Should().BeFalse();
	}

	[Test]
	public void WhenRemovingAdditionalDataAndTheDataDoesNotExist_ThenNothingHappens()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );

		// when
		var result = measurement.RemoveAdditionalData( "Z.txt" );

		// then
		result.Should().BeFalse();
		measurement.ContainsAdditionalData( "A.txt" ).Should().BeTrue();
	}

	[Test]
	public void WhenClearingAdditionalData_ThenAllAdditionalDataIsRemoved()
	{
		// given
		var measurement = new Measurement();
		measurement.AddAdditionalData( "A.txt", new MemoryStream() );
		measurement.AddAdditionalData( "B.txt", new MemoryStream() );

		// when
		measurement.ClearAdditionalData();

		// then
		measurement.AdditionalDataCount.Should().Be( 0 );
		measurement.ContainsAdditionalData( "A.txt" ).Should().BeFalse();
		measurement.ContainsAdditionalData( "B.txt" ).Should().BeFalse();
	}

	#endregion
}