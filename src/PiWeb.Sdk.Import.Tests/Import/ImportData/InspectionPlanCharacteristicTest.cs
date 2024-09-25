#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Zeiss.PiWeb.Sdk.Import.ImportData;

namespace Zeiss.PiWeb.Sdk.Import.Tests.Import.ImportData;

[TestFixture]
public class InspectionPlanCharacteristicTest
{
	#region methods

	[Test]
	public void WhenCreatingCharacteristics_ThenPropertiesAreInitializedCorrectly()
	{
		// when
		var char1 = new InspectionPlanCharacteristic( "char1" );

		// then
		char1.Should().BeAssignableTo<InspectionPlanCharacteristic>();
		char1.Name.Should().Be( "char1" );
		char1.Parent.Should().BeNull();
		char1.EntityType.Should().Be( EntityType.Characteristic );
		char1.InspectionPlanEntityType.Should().Be( InspectionPlanEntityType.Characteristic );
		char1.CharacteristicCount.Should().Be( 0 );
	}

	[Test]
	public void WhenAddingPartsAsChild_ThenEntityStructureIsCorrect()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var c1 = new InspectionPlanCharacteristic( "c1" );
		var c2 = new InspectionPlanCharacteristic( "c2" );
		var c3 = new InspectionPlanCharacteristic( "c3" );

		// when
		root.AddCharacteristic( c1 );
		root.AddCharacteristic( c2 );
		root.AddCharacteristic( c3 );

		// then
		root.CharacteristicCount.Should().Be( 3 );

		c1.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c1.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c1.Name, out var resultEntityC1 ).Should().BeTrue();
		resultEntityC1.Should().BeSameAs( c1 );

		c2.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c2.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c2.Name, out var resultEntityC2 ).Should().BeTrue();
		resultEntityC2.Should().BeSameAs( c2 );

		c3.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c3.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c3.Name, out var resultEntityC3 ).Should().BeTrue();
		resultEntityC3.Should().BeSameAs( c3 );
	}

	[Test]
	public void WhenAddingCharacteristicsAsChild_ThenEntityStructureIsCorrect()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var c1 = new InspectionPlanCharacteristic( "c1" );
		var c2 = new InspectionPlanCharacteristic( "p2" );
		var c3 = new InspectionPlanCharacteristic( "p3" );

		// when
		root.AddCharacteristic( c1 );
		root.AddCharacteristic( c2 );
		root.AddCharacteristic( c3 );

		// then
		root.CharacteristicCount.Should().Be( 3 );

		c1.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c1.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c1.Name, out var resultEntityC1 ).Should().BeTrue();
		resultEntityC1.Should().BeSameAs( c1 );

		c2.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c2.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c2.Name, out var resultEntityC2 ).Should().BeTrue();
		resultEntityC2.Should().BeSameAs( c2 );

		c3.Parent.Should().BeSameAs( root );
		root.ContainsCharacteristic( c3.Name ).Should().BeTrue();
		root.TryGetCharacteristic( c3.Name, out var resultEntityP3 ).Should().BeTrue();
		resultEntityP3.Should().BeSameAs( c3 );
	}

	[Test]
	public void WhenAddingACharacteristicWithTheSameNameAsAnExistingChildCharacteristic_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		root.AddCharacteristic( "conflict" );
		var char1B = new InspectionPlanCharacteristic( "conflict" );

		// when, then
		root.Invoking( c => c.AddCharacteristic( char1B ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACharacteristic_ThenCaseIsIgnoredAndAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		root.AddCharacteristic( "conflict" );
		var char1B = new InspectionPlanCharacteristic( "cOnFlIcT" );

		// when, then
		root.Invoking( c => c.AddCharacteristic( char1B ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenAddingACharacteristicThatIsAlreadyAChild_ThenAnExceptionIsThrown( int additionalChildCount )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 0; i < additionalChildCount; ++i )
			root.AddCharacteristic( "characteristic" + i );
		var char1 = root.AddCharacteristic( "conflict" );

		// when, then
		root.Invoking( c => c.AddCharacteristic( char1 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACyclicCharacteristic_ThenAnExceptionIsThrown()
	{
		// given
		var c1 = new InspectionPlanCharacteristic( "c1" );
		var c2 = c1.AddCharacteristic( "c2" );
		var c3 = c2.AddCharacteristic( "c3" );

		// when, then
		c3.Invoking( p => p.AddCharacteristic( c1 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACharacteristicThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var characteristic = rootPartA.AddCharacteristic( "char1" );

		var rootCharacteristic = new InspectionPlanCharacteristic( "rootB" );

		// when
		rootCharacteristic.AddCharacteristic( characteristic );

		// then
		rootPartA.ContainsCharacteristic( characteristic.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 0 );

		rootCharacteristic.ContainsCharacteristic( characteristic.Name ).Should().BeTrue();
		rootCharacteristic.CharacteristicCount.Should().Be( 1 );

		characteristic.Parent.Should().BeSameAs( rootCharacteristic );
	}

	[Test]
	public void WhenAddingACharacteristicThatIsAlreadyAChildOfAnotherPartWithMoreThen10Children_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var characteristic = rootPartA.AddCharacteristic( "char" );
		for( var i = 0; i < 20; ++i )
			rootPartA.AddCharacteristic( "characteristic" + i );

		var rootCharacteristic = new InspectionPlanCharacteristic( "rootB" );

		// when
		rootCharacteristic.AddCharacteristic( characteristic );

		// when
		rootPartA.ContainsCharacteristic( characteristic.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 20 );

		rootCharacteristic.ContainsCharacteristic( characteristic.Name ).Should().BeTrue();
		rootCharacteristic.CharacteristicCount.Should().Be( 1 );

		characteristic.Parent.Should().BeSameAs( rootCharacteristic );
	}

	[Test]
	public void WhenDeterminingParentPart_ThenTheCorrectPartIsFound1()
	{
		// given
		var rootCharacteristic = new InspectionPlanCharacteristic( "root" );

		// when
		var parentPart = rootCharacteristic.GetParentPart();

		// then
		parentPart.Should().BeNull();
	}

	[Test]
	public void WhenDeterminingParentPart_ThenTheCorrectPartIsFound2()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var characteristic = rootPart.AddCharacteristic( "char1" );

		// when
		var parentPart = characteristic.GetParentPart();

		// then
		parentPart.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenDeterminingParentPart_ThenTheCorrectPartIsFound3()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var char1 = rootPart.AddCharacteristic( "char1" );
		var char2 = char1.AddCharacteristic( "char2" );

		// when
		var parentPart = char2.GetParentPart();

		// then
		parentPart.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound1()
	{
		// given
		var char1 = new InspectionPlanCharacteristic( "char1" );
		var char2 = new InspectionPlanCharacteristic( "char2" );

		// when
		var result = char1.HasSameMeasurementRootAs( char2, out var measurementRoot );

		// then
		result.Should().BeFalse();
		measurementRoot.Should().BeNull();
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound2()
	{
		// given
		var char1 = new InspectionPlanCharacteristic( "char1" );
		var char2 = char1.AddCharacteristic( "char2" );

		// when
		var result = char1.HasSameMeasurementRootAs( char2, out var measurementRoot );

		// then
		result.Should().BeFalse();
		measurementRoot.Should().BeNull();
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound3()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var char2 = new InspectionPlanCharacteristic( "char2" );

		// when
		var result = char1.HasSameMeasurementRootAs( char2, out var measurementRoot );

		// then
		result.Should().BeFalse();
		measurementRoot.Should().BeSameAs( root );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound4()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );

		// when
		var result = char1.HasSameMeasurementRootAs( char2, out var measurementRoot );

		// then
		result.Should().BeTrue();
		measurementRoot.Should().BeSameAs( root );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound8()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = char1.AddCharacteristic( "char2" );

		// when
		var result = char1.HasSameMeasurementRootAs( char2, out var measurementRoot );

		// then
		result.Should().BeTrue();
		measurementRoot.Should().BeSameAs( root );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound9()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		var char2 = root.AddCharacteristic( "char2" );
		var char3 = char2.AddCharacteristic( "char3" );

		// when
		var result = char1.HasSameMeasurementRootAs( char3, out var measurementRoot );

		// then
		result.Should().BeTrue();
		measurementRoot.Should().BeSameAs( root );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRenamingCharacteristic_ThenParentPartIsUpdated( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var char0 = root.AddCharacteristic( "char0" );

		// when
		char0.Rename( "foo" );

		// then
		char0.Name.Should().Be( "foo" );
		root.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		root.ContainsCharacteristic( "foo" ).Should().BeTrue();
		root.TryGetCharacteristic( "foo", out var result ).Should().BeTrue();
		result.Should().BeSameAs( char0 );
	}

	[Test]
	public void WhenRenamingCharacteristicButCharacteristicWithTheNewNameAlreadyExists_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		root.AddCharacteristic( "char2" );

		// when, then
		char1.Invoking( c => c.Rename( "char2" ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRenamingCharacteristic_ThenParentCharacteristicIsUpdated( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var char0 = root.AddCharacteristic( "char0" );

		// when
		char0.Rename( "foo" );

		// then
		char0.Name.Should().Be( "foo" );
		root.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		root.ContainsCharacteristic( "foo" ).Should().BeTrue();
		root.TryGetCharacteristic( "foo", out var result ).Should().BeTrue();
		result.Should().BeSameAs( char0 );
		root.ContainsCharacteristic( "char0" ).Should().BeFalse();
	}

	[Test]
	public void WhenRenamingCharacteristicButPartWithTheNewNameAlreadyExistsOnParentPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		root.AddPart( "part2" );

		// when, then
		char1.Invoking( c => c.Rename( "part2" ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenRenamingCharacteristicButCharacteristicWithTheNewNameAlreadyExistsOnParentPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var char1 = root.AddCharacteristic( "char1" );
		root.AddCharacteristic( "char2" );

		// when, then
		char1.Invoking( c => c.Rename( "char2" ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingExistingCharacteristic_ThenTheCharacteristicIsFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var char0 = root.AddCharacteristic( "char0" );

		// when
		var opResult = root.TryGetCharacteristic( "char0", out var resultEntry );

		// then
		opResult.Should().BeTrue();
		resultEntry.Should().BeSameAs( char0 );
	}

	[TestCase( 0 )]
	[TestCase( 2 )]
	[TestCase( 20 )]
	public void WhenGettingNonExistingCharacteristic_ThenTheCharacteristicIsNotFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		// when
		var opResult = root.TryGetCharacteristic( "char0", out var resultEntry );

		// then
		opResult.Should().BeFalse();
		resultEntry.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingExistingCharacteristicAsEntity_ThenTheCharacteristicIsFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var char0 = root.AddCharacteristic( "char0" );

		// when
		var opResult = root.TryGetEntity( "char0", out var resultEntry );

		// then
		opResult.Should().BeTrue();
		resultEntry.Should().BeSameAs( char0 );
	}

	[TestCase( 0 )]
	[TestCase( 2 )]
	[TestCase( 20 )]
	public void WhenGettingNonExistingCharacteristicAsEntity_ThenTheCharacteristicIsNotFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		// when
		var opResult = root.TryGetEntity( "char0", out var resultEntry );

		// then
		opResult.Should().BeFalse();
		resultEntry.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 5 )]
	[TestCase( 20 )]
	public void WhenGettingCharacteristics_AllCharacteristicsAreReturned( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var nodeSet = new HashSet<InspectionPlanEntity>();
		for( var i = 1; i <= 20; ++i )
			nodeSet.Add( root.AddCharacteristic( "char" + i ) );

		// when
		var entities = root.EnumerateCharacteristics().ToArray();

		// then
		entities.Should().HaveCount( nodeSet.Count );
		entities.Should().AllSatisfy( entity => nodeSet.Contains( entity ).Should().BeTrue() );
	}

	[TestCase( 0 )]
	[TestCase( 4 )]
	[TestCase( 20 )]
	public void WhenGettingAllEntities_AllEntitiesAreReturned( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var nodeSet = new HashSet<InspectionPlanEntity>();
		for( var i = 1; i <= additionalCharacteristics; ++i )
			nodeSet.Add( root.AddCharacteristic( "char" + i ) );

		// when
		var entities = root.EnumerateEntities().ToArray();

		// then
		entities.Should().HaveCount( nodeSet.Count );
		if( entities.Length != 0 )
			entities.Should().AllSatisfy( entity => nodeSet.Contains( entity ).Should().BeTrue() );
	}

	[Test]
	public void WhenSettingACharacteristicWithTheSameNameAsAnExistingCharacteristic_ThenTheCharacteristicIsReplaced()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var characteristic1 = root.AddCharacteristic( "conflict" );
		var characteristic2 = new InspectionPlanCharacteristic( "conflict" );

		// when
		var opResult = root.SetCharacteristic( characteristic2, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( characteristic2 );
		replacedEntity.Should().BeSameAs( characteristic1 );
		replacedEntity!.Parent.Should().BeNull();
		root.CharacteristicCount.Should().Be( 1 );
		root.ContainsCharacteristic( "conflict" ).Should().BeTrue();
		root.TryGetCharacteristic( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( characteristic2 );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingACharacteristicWithTheSameNameAsAnExistingCharacteristic_ThenTheCharacteristicIsReplaced(
		int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var characteristicA = root.AddCharacteristic( "conflict" );
		var characteristicB = new InspectionPlanCharacteristic( "conflict" );

		// when
		var opResult = root.SetCharacteristic( characteristicB, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( characteristicB );
		replacedEntity.Should().BeSameAs( characteristicA );
		replacedEntity!.Parent.Should().BeNull();
		root.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		root.ContainsCharacteristic( "conflict" ).Should().BeTrue();
		root.TryGetCharacteristic( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( characteristicB );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingACharacteristicThatIsAlreadyAChild_ThenNothingHappens( int additionalCharacteristics )
	{
		// given
		var rootPart = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			rootPart.AddCharacteristic( "char" + i );
		var characteristic = rootPart.AddCharacteristic( "conflict" );

		// when
		var opResult = rootPart.SetCharacteristic( characteristic, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( characteristic );
		replacedEntity.Should().BeNull();
		rootPart.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		rootPart.EntityCount.Should().Be( additionalCharacteristics + 1 );
		rootPart.ContainsCharacteristic( "conflict" ).Should().BeTrue();
		rootPart.TryGetCharacteristic( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( characteristic );
	}

	[Test]
	public void WhenSettingACyclicCharacteristic_ThenAnExceptionIsThrown()
	{
		// given
		var c1 = new InspectionPlanCharacteristic( "c1" );
		var c2 = c1.AddCharacteristic( "c2" );
		var c3 = c2.AddCharacteristic( "c3" );

		// when, then
		c3.Invoking( p => p.SetCharacteristic( c1, out _ ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenSettingACyclicCharacteristicReplacingAnotherCharacteristic_ThenAnExceptionIsThrown()
	{
		// given
		var c1 = new InspectionPlanCharacteristic( "conflict" );
		var c2 = c1.AddCharacteristic( "p2" );
		var c3 = c2.AddCharacteristic( "p3" );
		c3.AddCharacteristic( "conflict" );

		// when, then
		c3.Invoking( p => p.SetCharacteristic( c1, out _ ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenSettingACharacteristicThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanCharacteristic( "rootA" );
		var charA = rootPartA.AddCharacteristic( "charA" );

		var rootPartB = new InspectionPlanCharacteristic( "rootB" );

		// when
		var opResult = rootPartB.SetCharacteristic( charA, out var replacedEntity );

		// when, then
		opResult.Should().BeSameAs( charA );
		replacedEntity.Should().BeNull();
		rootPartA.ContainsCharacteristic( charA.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 0 );

		rootPartB.ContainsCharacteristic( charA.Name ).Should().BeTrue();
		rootPartB.CharacteristicCount.Should().Be( 1 );

		charA.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenSettingACharacteristicThatIsAlreadyAChildOfAnotherPartReplacingAnExistingEntity_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var charA = rootPartA.AddCharacteristic( "conflict" );

		var rootCharacteristicB = new InspectionPlanCharacteristic( "rootB" );
		var charB = rootCharacteristicB.AddCharacteristic( "conflict" );

		// when
		var opResult = rootCharacteristicB.SetCharacteristic( charA, out var replacedEntity );

		// when, then
		opResult.Should().BeSameAs( charA );
		replacedEntity.Should().BeSameAs( charB );
		rootPartA.ContainsCharacteristic( charA.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 0 );

		rootCharacteristicB.ContainsCharacteristic( charA.Name ).Should().BeTrue();
		rootCharacteristicB.CharacteristicCount.Should().Be( 1 );

		charA.Parent.Should().BeSameAs( rootCharacteristicB );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRemovingCharacteristic_ThenTheCharacteristicIsRemoved( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );
		var char0 = root.AddCharacteristic( "char0" );

		// when
		var opResult = root.RemoveEntity( char0 );

		// then
		opResult.Should().BeTrue();
		root.CharacteristicCount.Should().Be( additionalCharacteristics );
		root.ContainsCharacteristic( "char0" ).Should().BeFalse();
		root.TryGetCharacteristic( "char0", out var resultCharacteristic ).Should().BeFalse();
		resultCharacteristic.Should().BeNull();
		root.EnumerateCharacteristics().Should().NotContain( char0 );
		char0.Parent.Should().BeNull();
	}

	[Test]
	public void WhenRemovingNonExistingCharacteristic_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		root.AddCharacteristic( "char1" );
		var char2 = new InspectionPlanCharacteristic( "char2" );

		// when
		var opResult = root.RemoveEntity( char2 );

		// then
		opResult.Should().BeFalse();
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().NotContain( char2 );
		char2.Parent.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRemovingCharacteristicByName_ThenTheCharacteristicIsRemoved( int additionalCharacteristics )
	{
		// given
		var rootCharacteristic = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			rootCharacteristic.AddCharacteristic( "char" + i );
		var char0 = rootCharacteristic.AddCharacteristic( "char0" );

		// when
		var opResult = rootCharacteristic.RemoveEntity( char0.Name );

		// then
		opResult.Should().BeTrue();
		rootCharacteristic.CharacteristicCount.Should().Be( additionalCharacteristics );
		rootCharacteristic.EntityCount.Should().Be( additionalCharacteristics );
		rootCharacteristic.EnumerateCharacteristics().Should().NotContain( char0 );
		char0.Parent.Should().BeNull();
	}

	[Test]
	public void WhenRemovingNonExistingEntityByName_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );

		// when
		var opResult = root.RemoveEntity( "SomeCharacteristic" );

		// then
		opResult.Should().BeFalse();
		root.CharacteristicCount.Should().Be( 0 );
		root.EntityCount.Should().Be( 0 );
	}

	[Test]
	public void WhenRemovingACharacteristic_ThenCaseIsIgnored()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		root.AddCharacteristic( "char1" );
		root.AddCharacteristic( "char2" );

		// when
		root.RemoveEntity( "ChaR1" );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.ContainsCharacteristic( "char1" ).Should().BeFalse();
		root.ContainsCharacteristic( "ChaR1" ).Should().BeFalse();
	}

	[Test]
	public void WhenNotifyingChildRenameWithoutRenaming_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 0; i < 20; ++i )
			root.AddCharacteristic( "char" + i );
		var char21 = root.AddCharacteristic( "char21" );

		// when
		root.NotifyChildRenamed( char21, "char21" );

		// then
		root.CharacteristicCount.Should().Be( 21 );
		root.EntityCount.Should().Be( 21 );
		root.EnumerateCharacteristics().Should().Contain( char21 );
		root.TryGetCharacteristic( "char21", out var childEntity ).Should().BeTrue();
		childEntity.Should().BeSameAs( char21 );
		char21.Parent.Should().BeSameAs( root );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenNotifyingChildRenameForKnownPreviousEntity_ThenNothingHappens( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );
		var char0 = root.AddCharacteristic( "char0" );
		var other = root.AddCharacteristic( "other" );

		// when
		root.NotifyChildRenamed( char0, "other" );

		// then
		root.CharacteristicCount.Should().Be( additionalCharacteristics + 2 );
		root.EnumerateCharacteristics().Should().Contain( char0 );
		root.TryGetCharacteristic( "char0", out var childCharacteristic0 ).Should().BeTrue();
		childCharacteristic0.Should().BeSameAs( char0 );
		char0.Parent.Should().BeSameAs( root );
		root.EnumerateCharacteristics().Should().Contain( other );
		root.TryGetCharacteristic( "other", out var otherChildEntity ).Should().BeTrue();
		otherChildEntity.Should().BeSameAs( other );
		other.Parent.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingChildRenameForUnknownPreviousName_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 0; i < 20; ++i )
			root.AddCharacteristic( "char" + i );
		var char21 = root.AddCharacteristic( "char21" );

		// when
		root.NotifyChildRenamed( char21, "Unknown Name" );

		// then
		root.CharacteristicCount.Should().Be( 21 );
		root.EntityCount.Should().Be( 21 );
		root.EnumerateCharacteristics().Should().Contain( char21 );
		root.TryGetCharacteristic( "char21", out var childEntity ).Should().BeTrue();
		childEntity.Should().BeSameAs( char21 );
		char21.Parent.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingChildRenameForSpuriousEntity_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		for( var i = 0; i < 20; ++i )
			root.AddCharacteristic( "char" + i );
		var char21 = root.AddCharacteristic( "char21" );

		var other = new InspectionPlanCharacteristic( "other" );

		// when
		root.NotifyChildRenamed( other, "char21" );

		// then
		root.CharacteristicCount.Should().Be( 21 );
		root.EnumerateCharacteristics().Should().NotContain( other );
		root.EnumerateCharacteristics().Should().Contain( char21 );
		root.TryGetCharacteristic( "char21", out var childEntity21 ).Should().BeTrue();
		childEntity21.Should().BeSameAs( char21 );
		root.TryGetCharacteristic( "other", out var otherChildEntity ).Should().BeFalse();
		otherChildEntity.Should().BeNull();
		char21.Parent.Should().BeSameAs( root );
		other.Parent.Should().BeNull();
	}

	[Test]
	public void WhenNotifyingChildRemovedForSpuriousEntity_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var otherCharacteristic = new InspectionPlanCharacteristic( "other" );

		// when
		root.NotifyChildRemoved( otherCharacteristic );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().NotContain( otherCharacteristic );
		root.EnumerateCharacteristics().Should().Contain( char1 );
		root.TryGetCharacteristic( "char1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( char1 );
		root.TryGetCharacteristic( "other", out var otherChildEntity ).Should().BeFalse();
		otherChildEntity.Should().BeNull();
		char1.Parent.Should().BeSameAs( root );
		otherCharacteristic.Parent.Should().BeNull();
	}

	[Test]
	public void WhenNotifyingChildRemovedForKnownEntity_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		// when
		root.NotifyChildRemoved( char1 );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().Contain( char1 );
		root.TryGetCharacteristic( "char1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( char1 );
		char1.Parent.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingParentChangedForSpuriousNewParent1_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		var otherCharacteristic = new InspectionPlanCharacteristic( "other" );

		// when
		char1.NotifyParentChanged( otherCharacteristic );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().Contain( char1 );
		root.TryGetCharacteristic( "char1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( char1 );
		char1.Parent.Should().BeSameAs( root );
		otherCharacteristic.CharacteristicCount.Should().Be( 0 );
	}

	[Test]
	public void WhenNotifyingParentChangedForSpuriousNewParent2_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		// when
		char1.NotifyParentChanged( null );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().Contain( char1 );
		root.TryGetCharacteristic( "char1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( char1 );
		char1.Parent.Should().BeSameAs( root );
	}

	[Test]
	public void WhenNotifyingParentChangedWithoutChangedParent_ThenNothingHappens()
	{
		// given
		var root = new InspectionPlanCharacteristic( "root" );
		var char1 = root.AddCharacteristic( "char1" );

		// when
		char1.NotifyParentChanged( root );

		// then
		root.CharacteristicCount.Should().Be( 1 );
		root.EntityCount.Should().Be( 1 );
		root.EnumerateCharacteristics().Should().Contain( char1 );
		root.TryGetCharacteristic( "char1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( char1 );
		char1.Parent.Should().BeSameAs( root );
	}

	#endregion
}