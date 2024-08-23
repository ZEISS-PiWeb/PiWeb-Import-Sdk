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
public class InspectionPlanPartTest
{
	#region methods

	[Test]
	public void WhenCreatingParts_ThenPropertiesAreInitializedCorrectly()
	{
		// when
		var root = new InspectionPlanPart( "root" );

		// then
		root.Should().BeAssignableTo<InspectionPlanPart>();
		root.Name.Should().Be( "root" );
		root.Parent.Should().BeNull();
		root.EntityType.Should().Be( EntityType.Part );
		root.InspectionPlanEntityType.Should().Be( InspectionPlanEntityType.Part );
		root.CharacteristicCount.Should().Be( 0 );
	}

	[Test]
	public void WhenAddingPartsAsChild_ThenEntityStructureIsCorrect()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var p1 = new InspectionPlanPart( "p1" );
		var p2 = new InspectionPlanPart( "p2" );
		var p3 = new InspectionPlanPart( "p3" );

		// when
		root.AddPart( p1 );
		root.AddPart( p2 );
		root.AddPart( p3 );

		// then
		root.PartCount.Should().Be( 3 );

		p1.Parent.Should().BeSameAs( root );
		root.ContainsPart( p1.Name ).Should().BeTrue();
		root.TryGetPart( p1.Name, out var resultEntityP1 ).Should().BeTrue();
		resultEntityP1.Should().BeSameAs( p1 );

		p2.Parent.Should().BeSameAs( root );
		root.ContainsPart( p2.Name ).Should().BeTrue();
		root.TryGetPart( p2.Name, out var resultEntityP2 ).Should().BeTrue();
		resultEntityP2.Should().BeSameAs( p2 );

		p3.Parent.Should().BeSameAs( root );
		root.ContainsPart( p3.Name ).Should().BeTrue();
		root.TryGetPart( p3.Name, out var resultEntityP3 ).Should().BeTrue();
		resultEntityP3.Should().BeSameAs( p3 );
	}

	[Test]
	public void WhenAddingCharacteristicsAsChild_ThenEntityStructureIsCorrect()
	{
		// given
		var root = new InspectionPlanPart( "root" );
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
		var root = new InspectionPlanPart( "root" );
		root.AddCharacteristic( "conflict" );
		var char1B = new InspectionPlanCharacteristic( "conflict" );

		// when, then
		root.Invoking( c => c.AddCharacteristic( char1B ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACharacteristic_ThenCaseIsIgnoredAndAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddPart( "conflict" );
		var part1B = new InspectionPlanPart( "cOnFlIcT" );

		// when, then
		root.Invoking( c => c.AddPart( part1B ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACharacteristicWithTheSameNameAsAnExistingChildPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddPart( "conflict" );
		var char1 = new InspectionPlanCharacteristic( "conflict" );

		// when, then
		root.Invoking( c => c.AddCharacteristic( char1 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingAPartWithTheSameNameAsAnExistingChildCharacteristic_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddCharacteristic( "conflict" );
		var part1 = new InspectionPlanPart( "conflict" );

		// when, then
		root.Invoking( c => c.AddPart( part1 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingAPartWithTheSameNameAsAnExistingChildPart_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddPart( "conflict" );
		var part1 = new InspectionPlanPart( "conflict" );

		// when, then
		root.Invoking( c => c.AddPart( part1 ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenAddingACharacteristicThatIsAlreadyAChild_ThenAnExceptionIsThrown( int additionalCharacteristics )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			rootPart.AddCharacteristic( "characteristic" + i );

		var char0 = rootPart.AddCharacteristic( "conflict" );

		// when, then
		rootPart.Invoking( c => c.AddCharacteristic( char0 ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenAddingAPartThatIsAlreadyAChild_ThenAnExceptionIsThrown( int additionalParts )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			rootPart.AddPart( "part" + i );

		var part0 = rootPart.AddPart( "conflict" );

		// when, then
		rootPart.Invoking( c => c.AddPart( part0 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingACyclicPart_ThenAnExceptionIsThrown()
	{
		// given
		var p1 = new InspectionPlanPart( "p1" );
		var p2 = p1.AddPart( "p2" );
		var p3 = p2.AddPart( "p3" );

		// when, then
		p3.Invoking( p => p.AddPart( p1 ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenAddingAPartThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var partA = rootPartA.AddPart( "partA" );

		var rootPartB = new InspectionPlanPart( "rootB" );

		// when
		rootPartB.AddPart( partA );

		// when, then
		rootPartA.ContainsPart( partA.Name ).Should().BeFalse();
		rootPartA.PartCount.Should().Be( 0 );

		rootPartB.ContainsPart( partA.Name ).Should().BeTrue();
		rootPartB.PartCount.Should().Be( 1 );

		partA.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenAddingAPartThatIsAlreadyAChildOfAnotherPartWithMoreThen10Children_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var partA = rootPartA.AddPart( "partA" );
		for( var i = 0; i < 20; ++i )
			rootPartA.AddPart( "part" + i );

		var rootPartB = new InspectionPlanPart( "rootB" );

		// when
		rootPartB.AddPart( partA );

		// when, then
		rootPartA.ContainsPart( partA.Name ).Should().BeFalse();
		rootPartA.PartCount.Should().Be( 20 );

		rootPartB.ContainsPart( partA.Name ).Should().BeTrue();
		rootPartB.PartCount.Should().Be( 1 );

		partA.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenRemovingAPart_ThenCaseIsIgnored()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddPart( "p1" );
		root.AddPart( "p2" );

		// when
		root.RemoveEntity( "P1" );

		// then
		root.PartCount.Should().Be( 1 );
		root.ContainsPart( "p1" ).Should().BeFalse();
		root.ContainsPart( "P1" ).Should().BeFalse();
	}

	[Test]
	public void WhenAddingACharacteristicThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var characteristic = rootPartA.AddCharacteristic( "char1" );

		var rootPartB = new InspectionPlanPart( "rootB" );

		// when
		rootPartB.AddCharacteristic( characteristic );

		// when, then
		rootPartA.ContainsCharacteristic( characteristic.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 0 );

		rootPartB.ContainsCharacteristic( characteristic.Name ).Should().BeTrue();
		rootPartB.CharacteristicCount.Should().Be( 1 );

		characteristic.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenAddingACharacteristicThatIsAlreadyAChildOfAnotherPartWithMoreThen10Children_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var characteristic = rootPartA.AddCharacteristic( "char" );
		for( var i = 0; i < 20; ++i )
			rootPartA.AddCharacteristic( "characteristic" + i );

		var rootPartB = new InspectionPlanPart( "rootB" );

		// when
		rootPartB.AddCharacteristic( characteristic );

		// when, then
		rootPartA.ContainsCharacteristic( characteristic.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 20 );

		rootPartB.ContainsCharacteristic( characteristic.Name ).Should().BeTrue();
		rootPartB.CharacteristicCount.Should().Be( 1 );

		characteristic.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenDeterminingParentPart_ThenTheCorrectPartIsFound1()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );

		// when
		var parentPart = rootPart.GetParentPart();

		// then
		parentPart.Should().BeNull();
	}

	[Test]
	public void WhenDeterminingParentPart_ThenTheCorrectPartIsFound2()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		// when
		var parentPart = part1.GetParentPart();

		// then
		parentPart.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound1()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );

		// when
		var result = rootPart.HasSameMeasurementRootAs( rootPart, out var measurementRoot );

		// then
		result.Should().BeTrue();
		measurementRoot.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound2()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = new InspectionPlanPart( "part1" );

		// when
		var result = part1.HasSameMeasurementRootAs( rootPart, out var measurementRoot );

		// then
		result.Should().BeFalse();
		measurementRoot.Should().BeSameAs( part1 );
	}

	[Test]
	public void WhenCheckingSameMeasurementRoot_ThenTheCorrectResultIsFound3()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		// when
		var result = part1.HasSameMeasurementRootAs( rootPart, out var measurementRoot );

		// then
		result.Should().BeFalse();
		measurementRoot.Should().BeSameAs( part1 );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRenamingPart_ThenParentIsUpdated( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		var part0 = root.AddPart( "part0" );

		// when
		part0.Rename( "foo" );

		// then
		part0.Name.Should().Be( "foo" );
		root.PartCount.Should().Be( additionalParts + 1 );
		root.ContainsPart( "foo" ).Should().BeTrue();
		root.TryGetPart( "foo", out var result ).Should().BeTrue();
		result.Should().BeSameAs( part0 );
	}

	[Test]
	public void WhenRenamingPartButPartWithTheNewNameAlreadyExists_ThenAnExceptionIsThrown()
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var part1 = root.AddPart( "part1" );
		root.AddPart( "part2" );

		// when, then
		part1.Invoking( c => c.Rename( "part2" ) ).Should().Throw<ImportDataException>();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingExistingCharacteristic_ThenTheCharacteristicIsFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
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
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingExistingPart_ThenThePartIsFound( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		var part0 = root.AddPart( "part0" );

		// when
		var opResult = root.TryGetPart( "part0", out var resultEntry );

		// then
		opResult.Should().BeTrue();
		resultEntry.Should().BeSameAs( part0 );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingNonExistingCharacteristic_ThenTheCharacteristicIsNotFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
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
	public void WhenGettingNonExistingPart_ThenThePartIsNotFound( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= 21; ++i )
			root.AddPart( "part" + i );

		// when
		var opResult = root.TryGetPart( "part0", out var resultEntry );

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
		var root = new InspectionPlanPart( "root" );
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
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingExistingPartAsEntity_ThenThePartIsFound( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		var part0 = root.AddPart( "part0" );

		// when
		var opResult = root.TryGetEntity( "part0", out var resultEntry );

		// then
		opResult.Should().BeTrue();
		resultEntry.Should().BeSameAs( part0 );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingNonExistingCharacteristicAsEntity_ThenTheCharacteristicIsNotFound( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		// when
		var opResult = root.TryGetEntity( "char0", out var resultEntry );

		// then
		opResult.Should().BeFalse();
		resultEntry.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingNonExistingPartAsEntity_ThenThePartIsNotFound( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		// when
		var opResult = root.TryGetEntity( "part0", out var resultEntry );

		// then
		opResult.Should().BeFalse();
		resultEntry.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingCharacteristics_AllCharacteristicsAreReturned( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddPart( "dummy" );
		var nodeSet = new HashSet<InspectionPlanEntity>();
		for( var i = 1; i <= additionalCharacteristics; ++i )
			nodeSet.Add( root.AddCharacteristic( "char" + i ) );

		// when
		var entities = root.EnumerateCharacteristics().ToArray();

		// then
		entities.Should().HaveCount( nodeSet.Count );
		if( entities.Length > 0 )
			entities.Should().AllSatisfy( entity => nodeSet.Contains( entity ).Should().BeTrue() );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenGettingParts_AllPartsAreReturned( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		root.AddCharacteristic( "dummy" );
		var nodeSet = new HashSet<InspectionPlanPart>();
		for( var i = 1; i <= additionalParts; ++i )
			nodeSet.Add( root.AddPart( "part" + i ) );

		// when
		var entities = root.EnumerateParts().ToArray();

		// then
		entities.Should().HaveCount( nodeSet.Count );
		if( entities.Length > 0 )
			entities.Should().AllSatisfy( entity => nodeSet.Contains( entity ).Should().BeTrue() );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 4 )]
	[TestCase( 20 )]
	public void WhenGettingAllEntities_AllEntitiesAreReturned( int additionalEntities )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		var nodeSet = new HashSet<InspectionPlanEntity>();
		for( var i = 1; i <= additionalEntities; ++i )
			nodeSet.Add( root.AddPart( "part" + i ) );
		for( var i = 1; i <= additionalEntities; ++i )
			nodeSet.Add( root.AddCharacteristic( "char" + i ) );

		// when
		var entities = root.EnumerateEntities().ToArray();

		// then
		entities.Should().HaveCount( nodeSet.Count );
		if( entities.Length > 0 )
			entities.Should().AllSatisfy( entity => nodeSet.Contains( entity ).Should().BeTrue() );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingAPartWithTheSameNameAsAnExistingPart_ThenThePartIsReplaced( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		var part1A = root.AddPart( "conflict" );
		var part1B = new InspectionPlanPart( "conflict" );

		// when
		var opResult = root.SetPart( part1B, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( part1B );
		replacedEntity.Should().BeSameAs( part1A );
		replacedEntity!.Parent.Should().BeNull();
		root.PartCount.Should().Be( additionalParts + 1 );
		root.ContainsPart( "conflict" ).Should().BeTrue();
		root.TryGetPart( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( part1B );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingACharacteristicWithTheSameNameAsAnExistingCharacteristic_ThenTheCharacteristicIsReplaced(
		int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
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
	public void WhenSettingACharacteristicWithTheSameNameAsAnExistingPart_ThenThePartIsReplaced( int additionalCharacteristics )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			root.AddCharacteristic( "char" + i );

		var part = root.AddPart( "conflict" );
		var characteristic = new InspectionPlanCharacteristic( "conflict" );

		// when
		var opResult = root.SetCharacteristic( characteristic, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( characteristic );
		replacedEntity.Should().BeSameAs( part );
		replacedEntity!.Parent.Should().BeNull();
		root.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		root.PartCount.Should().Be( 0 );
		root.ContainsCharacteristic( "conflict" ).Should().BeTrue();
		root.ContainsPart( "conflict" ).Should().BeFalse();
		root.TryGetCharacteristic( "conflict", out var resultCharacteristic ).Should().BeTrue();
		root.TryGetPart( "conflict", out var resultPart ).Should().BeFalse();
		resultCharacteristic.Should().BeSameAs( characteristic );
		resultPart.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingAPartWithTheSameNameAsAnExistingCharacteristic_ThenTheCharacteristicIsReplaced( int additionalParts )
	{
		// given
		var root = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			root.AddPart( "part" + i );

		var characteristic = root.AddCharacteristic( "conflict" );
		var part = new InspectionPlanPart( "conflict" );

		// when
		var opResult = root.SetPart( part, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( part );
		replacedEntity.Should().BeSameAs( characteristic );
		replacedEntity!.Parent.Should().BeNull();
		root.PartCount.Should().Be( additionalParts + 1 );
		root.CharacteristicCount.Should().Be( 0 );
		root.ContainsCharacteristic( "conflict" ).Should().BeFalse();
		root.ContainsPart( "conflict" ).Should().BeTrue();
		root.TryGetCharacteristic( "conflict", out var resultCharacteristic ).Should().BeFalse();
		root.TryGetPart( "conflict", out var resultPart ).Should().BeTrue();
		resultCharacteristic.Should().BeNull();
		resultPart.Should().BeSameAs( part );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingAPartThatIsAlreadyAChild_ThenNothingHappens( int additionalParts )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			rootPart.AddPart( "part" + i );
		var part = rootPart.AddPart( "conflict" );

		// when
		var opResult = rootPart.SetPart( part, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( part );
		replacedEntity.Should().BeNull();
		rootPart.PartCount.Should().Be( additionalParts + 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( additionalParts + 1 );
		rootPart.ContainsPart( "conflict" ).Should().BeTrue();
		rootPart.TryGetPart( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( part );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenSettingACharacteristicThatIsAlreadyAChild_ThenNothingHappens( int additionalCharacteristics )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			rootPart.AddCharacteristic( "char" + i );
		var characteristic = rootPart.AddCharacteristic( "conflict" );

		// when
		var opResult = rootPart.SetCharacteristic( characteristic, out var replacedEntity );

		// then
		opResult.Should().BeSameAs( characteristic );
		replacedEntity.Should().BeNull();
		rootPart.PartCount.Should().Be( 0 );
		rootPart.CharacteristicCount.Should().Be( additionalCharacteristics + 1 );
		rootPart.EntityCount.Should().Be( additionalCharacteristics + 1 );
		rootPart.ContainsCharacteristic( "conflict" ).Should().BeTrue();
		rootPart.TryGetCharacteristic( "conflict", out var resultEntity ).Should().BeTrue();
		resultEntity.Should().BeSameAs( characteristic );
	}

	[Test]
	public void WhenSettingACyclicPart_ThenAnExceptionIsThrown()
	{
		// given
		var p1 = new InspectionPlanPart( "p1" );
		var p2 = p1.AddPart( "p2" );
		var p3 = p2.AddPart( "p3" );

		// when, then
		p3.Invoking( p => p.SetPart( p1, out _ ) ).Should().Throw<ImportDataException>();
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
	public void WhenSettingACyclicPartReplacingAnotherPart_ThenAnExceptionIsThrown()
	{
		// given
		var p1 = new InspectionPlanPart( "conflict" );
		var p2 = p1.AddPart( "p2" );
		var p3 = p2.AddPart( "p3" );
		p3.AddPart( "conflict" );

		// when, then
		p3.Invoking( p => p.SetPart( p1, out _ ) ).Should().Throw<ImportDataException>();
	}

	[Test]
	public void WhenSettingAPartThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var partA = rootPartA.AddPart( "partA" );

		var rootPartB = new InspectionPlanPart( "rootB" );

		// when
		var opResult = rootPartB.SetPart( partA, out var replacedEntity );

		// when, then
		opResult.Should().BeSameAs( partA );
		replacedEntity.Should().BeNull();
		rootPartA.ContainsPart( partA.Name ).Should().BeFalse();
		rootPartA.PartCount.Should().Be( 0 );

		rootPartB.ContainsPart( partA.Name ).Should().BeTrue();
		rootPartB.PartCount.Should().Be( 1 );

		partA.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenSettingACharacteristicThatIsAlreadyAChildOfAnotherPart_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var charA = rootPartA.AddCharacteristic( "charA" );

		var rootPartB = new InspectionPlanPart( "rootB" );

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
	public void WhenSettingAPartThatIsAlreadyAChildOfAnotherPartReplacingAnExistingEntity_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var partA = rootPartA.AddPart( "conflict" );

		var rootPartB = new InspectionPlanPart( "rootB" );
		var partB = rootPartB.AddPart( "conflict" );

		// when
		var opResult = rootPartB.SetPart( partA, out var replacedEntity );

		// when, then
		opResult.Should().BeSameAs( partA );
		replacedEntity.Should().BeSameAs( partB );
		rootPartA.ContainsPart( partA.Name ).Should().BeFalse();
		rootPartA.PartCount.Should().Be( 0 );

		rootPartB.ContainsPart( partA.Name ).Should().BeTrue();
		rootPartB.PartCount.Should().Be( 1 );

		partA.Parent.Should().BeSameAs( rootPartB );
	}

	[Test]
	public void WhenSettingACharacteristicThatIsAlreadyAChildOfAnotherPartReplacingAnExistingEntity_ThenItIsRemovedFromItsPreviousParent()
	{
		// given
		var rootPartA = new InspectionPlanPart( "rootA" );
		var charA = rootPartA.AddCharacteristic( "conflict" );

		var rootPartB = new InspectionPlanPart( "rootB" );
		var charB = rootPartB.AddCharacteristic( "conflict" );

		// when
		var opResult = rootPartB.SetCharacteristic( charA, out var replacedEntity );

		// when, then
		opResult.Should().BeSameAs( charA );
		replacedEntity.Should().BeSameAs( charB );
		rootPartA.ContainsCharacteristic( charA.Name ).Should().BeFalse();
		rootPartA.CharacteristicCount.Should().Be( 0 );

		rootPartB.ContainsCharacteristic( charA.Name ).Should().BeTrue();
		rootPartB.CharacteristicCount.Should().Be( 1 );

		charA.Parent.Should().BeSameAs( rootPartB );
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRemovingPart_ThenThePartIsRemoved( int additionalParts )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			rootPart.AddPart( "part" + i );
		var part0 = rootPart.AddPart( "part0" );

		// when
		var opResult = rootPart.RemoveEntity( part0 );

		// then
		opResult.Should().BeTrue();
		rootPart.PartCount.Should().Be( additionalParts );
		rootPart.ContainsPart( "part0" ).Should().BeFalse();
		rootPart.TryGetPart( "part0", out var resultPart ).Should().BeFalse();
		resultPart.Should().BeNull();
		rootPart.EnumerateParts().Should().NotContain( part0 );
		part0.Parent.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRemovingCharacteristic_ThenTheCharacteristicIsRemoved( int additionalCharacters )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacters; ++i )
			rootPart.AddCharacteristic( "char" + i );
		var char0 = rootPart.AddCharacteristic( "char0" );

		// when
		var opResult = rootPart.RemoveEntity( char0 );

		// then
		opResult.Should().BeTrue();
		rootPart.CharacteristicCount.Should().Be( additionalCharacters );
		rootPart.ContainsCharacteristic( "char0" ).Should().BeFalse();
		rootPart.TryGetCharacteristic( "char0", out var resultCharacteristic ).Should().BeFalse();
		resultCharacteristic.Should().BeNull();
		rootPart.EnumerateCharacteristics().Should().NotContain( char0 );
		char0.Parent.Should().BeNull();
	}

	[Test]
	public void WhenRemovingNonExistingPart_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		rootPart.AddPart( "part1" );
		var part2 = new InspectionPlanPart( "part2" );

		// when
		var opResult = rootPart.RemoveEntity( part2 );

		// then
		opResult.Should().BeFalse();
		rootPart.PartCount.Should().Be( 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateParts().Should().NotContain( part2 );
		part2.Parent.Should().BeNull();
	}

	[Test]
	public void WhenRemovingNonExistingCharacteristic_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		rootPart.AddCharacteristic( "char1" );
		var char2 = new InspectionPlanCharacteristic( "char2" );

		// when
		var opResult = rootPart.RemoveEntity( char2 );

		// then
		opResult.Should().BeFalse();
		rootPart.PartCount.Should().Be( 0 );
		rootPart.CharacteristicCount.Should().Be( 1 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateCharacteristics().Should().NotContain( char2 );
		char2.Parent.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 10 )]
	public void WhenRemovingPartByName_ThenThePartIsRemoved( int additionalParts )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalParts; ++i )
			rootPart.AddPart( "part" + i );
		var part0 = rootPart.AddPart( "part0" );

		// when
		var opResult = rootPart.RemoveEntity( part0.Name );

		// then
		opResult.Should().BeTrue();
		rootPart.PartCount.Should().Be( additionalParts );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( additionalParts );
		rootPart.EnumerateParts().Should().NotContain( part0 );
		part0.Parent.Should().BeNull();
	}

	[TestCase( 0 )]
	[TestCase( 1 )]
	[TestCase( 20 )]
	public void WhenRemovingCharacteristicByName_ThenTheCharacteristicIsRemoved( int additionalCharacteristics )
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 1; i <= additionalCharacteristics; ++i )
			rootPart.AddCharacteristic( "char" + i );
		var char0 = rootPart.AddCharacteristic( "char0" );

		// when
		var opResult = rootPart.RemoveEntity( char0.Name );

		// then
		opResult.Should().BeTrue();
		rootPart.CharacteristicCount.Should().Be( additionalCharacteristics );
		rootPart.PartCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( additionalCharacteristics );
		rootPart.EnumerateCharacteristics().Should().NotContain( char0 );
		char0.Parent.Should().BeNull();
	}

	[Test]
	public void WhenRemovingNonExistingEntityByName_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		rootPart.AddCharacteristic( "foobar" );

		// when
		var opResult = rootPart.RemoveEntity( "SomePart" );

		// then
		opResult.Should().BeFalse();
		rootPart.PartCount.Should().Be( 0 );
		rootPart.CharacteristicCount.Should().Be( 1 );
		rootPart.EntityCount.Should().Be( 1 );
	}

	[Test]
	public void WhenNotifyingChildRenameWithoutRenaming_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddPart( "part" + i );
		var part21 = rootPart.AddPart( "part21" );

		// when
		rootPart.NotifyChildRenamed( part21, "part21" );

		// then
		rootPart.PartCount.Should().Be( 21 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 21 );
		rootPart.EnumerateParts().Should().Contain( part21 );
		rootPart.TryGetPart( "part21", out var childEntity ).Should().BeTrue();
		childEntity.Should().BeSameAs( part21 );
		part21.Parent.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenNotifyingChildRenameForUnknownPreviousName_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddPart( "part" + i );
		var part21 = rootPart.AddPart( "part21" );

		// when
		rootPart.NotifyChildRenamed( part21, "Unknown Name" );

		// then
		rootPart.PartCount.Should().Be( 21 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 21 );
		rootPart.EnumerateParts().Should().Contain( part21 );
		rootPart.TryGetPart( "part21", out var childEntity ).Should().BeTrue();
		childEntity.Should().BeSameAs( part21 );
		part21.Parent.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenNotifyingChildRenameForSpuriousEntity_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddPart( "part" + i );
		var part21 = rootPart.AddPart( "part21" );

		var otherPart = new InspectionPlanPart( "other" );

		// when
		rootPart.NotifyChildRenamed( otherPart, "part21" );

		// then
		rootPart.PartCount.Should().Be( 21 );
		rootPart.EnumerateParts().Should().NotContain( otherPart );
		rootPart.EnumerateParts().Should().Contain( part21 );
		rootPart.TryGetPart( "part21", out var childEntity21 ).Should().BeTrue();
		childEntity21.Should().BeSameAs( part21 );
		rootPart.TryGetPart( "other", out var otherChildEntity ).Should().BeFalse();
		otherChildEntity.Should().BeNull();
		part21.Parent.Should().BeSameAs( rootPart );
		otherPart.Parent.Should().BeNull();
	}

	[Test]
	public void WhenNotifyingChildRemovedForSpuriousEntity_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		var otherPart = new InspectionPlanPart( "other" );

		// when
		rootPart.NotifyChildRemoved( otherPart );

		// then
		rootPart.PartCount.Should().Be( 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateParts().Should().NotContain( otherPart );
		rootPart.EnumerateParts().Should().Contain( part1 );
		rootPart.TryGetPart( "part1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( part1 );
		rootPart.TryGetPart( "other", out var otherChildEntity ).Should().BeFalse();
		otherChildEntity.Should().BeNull();
		part1.Parent.Should().BeSameAs( rootPart );
		otherPart.Parent.Should().BeNull();
	}

	[Test]
	public void WhenNotifyingParentChangedForSpuriousNewParent1_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		var otherPart = new InspectionPlanPart( "other" );

		// when
		part1.NotifyParentChanged( otherPart );

		// then
		rootPart.PartCount.Should().Be( 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateParts().Should().Contain( part1 );
		rootPart.TryGetPart( "part1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( part1 );
		part1.Parent.Should().BeSameAs( rootPart );
		otherPart.PartCount.Should().Be( 0 );
	}

	[Test]
	public void WhenNotifyingParentChangedForSpuriousNewParent2_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		// when
		part1.NotifyParentChanged( null );

		// then
		rootPart.PartCount.Should().Be( 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateParts().Should().Contain( part1 );
		rootPart.TryGetPart( "part1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( part1 );
		part1.Parent.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenNotifyingParentChangedWithoutChangedParent_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var part1 = rootPart.AddPart( "part1" );

		// when
		part1.NotifyParentChanged( rootPart );

		// then
		rootPart.PartCount.Should().Be( 1 );
		rootPart.CharacteristicCount.Should().Be( 0 );
		rootPart.EntityCount.Should().Be( 1 );
		rootPart.EnumerateParts().Should().Contain( part1 );
		rootPart.TryGetPart( "part1", out var childEntity1 ).Should().BeTrue();
		childEntity1.Should().BeSameAs( part1 );
		part1.Parent.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenCheckingContainsMeasurementAndMeasurementExists_ThenTheResultIsTrue()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = rootPart.AddMeasurement();

		// when
		var opResult = rootPart.ContainsMeasurement( measurement1 );

		// then
		opResult.Should().BeTrue();
	}

	[Test]
	public void WhenCheckingContainsMeasurementAndMeasurementExistsAndMeasurementsAreIndexed_ThenTheResultIsTrue()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddMeasurement();
		var measurement21 = rootPart.AddMeasurement();

		// when
		var opResult = rootPart.ContainsMeasurement( measurement21 );

		// then
		opResult.Should().BeTrue();
	}

	[Test]
	public void WhenAddingAMeasurementToAPart_ThenTheMeasurementIsAdded()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = new Measurement();

		// when
		rootPart.AddMeasurement( measurement1 );

		// then
		rootPart.MeasurementCount.Should().Be( 1 );
		rootPart.EnumerateMeasurements().Should().Contain( measurement1 );
		measurement1.Part.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenAddingAMeasurementToAPartAndTheMeasurementsAreIndexed_ThenTheMeasurementIsAdded()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddMeasurement();
		var measurement21 = new Measurement();

		// when
		rootPart.AddMeasurement( measurement21 );

		// then
		rootPart.MeasurementCount.Should().Be( 21 );
		rootPart.EnumerateMeasurements().Should().Contain( measurement21 );
		measurement21.Part.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenAddingAMeasurementToAPartAndThePartAlreadyHasThisMeasurement_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = rootPart.AddMeasurement();

		// when
		rootPart.AddMeasurement( measurement1 );

		// then
		rootPart.MeasurementCount.Should().Be( 1 );
		rootPart.EnumerateMeasurements().Should().Contain( measurement1 );
		measurement1.Part.Should().BeSameAs( rootPart );
	}

	[Test]
	public void WhenAddingAMeasurementToAPartAndAnotherPartAlreadyHasThisMeasurement_ThenTheMeasurementIsMoved()
	{
		// given
		var rootPart1 = new InspectionPlanPart( "root1" );
		var measurement1 = rootPart1.AddMeasurement();

		var rootPart2 = new InspectionPlanPart( "root" );

		// when
		rootPart2.AddMeasurement( measurement1 );

		// then
		rootPart1.MeasurementCount.Should().Be( 0 );
		rootPart1.EnumerateMeasurements().Should().NotContain( measurement1 );

		rootPart2.MeasurementCount.Should().Be( 1 );
		rootPart2.EnumerateMeasurements().Should().Contain( measurement1 );

		measurement1.Part.Should().BeSameAs( rootPart2 );
	}

	[Test]
	public void WhenRemovingAMeasurementFromAPart_ThenTheMeasurementIsRemoved()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = rootPart.AddMeasurement();

		// when
		var opResult = rootPart.RemoveMeasurement( measurement1 );

		// then
		opResult.Should().BeTrue();
		rootPart.MeasurementCount.Should().Be( 0 );
		rootPart.EnumerateMeasurements().Should().NotContain( measurement1 );
		measurement1.Part.Should().BeNull();
	}

	[Test]
	public void WhenRemovingAMeasurementFromAPartAndThePartDoesNotHaveThisMeasurement_ThenNothingHappens()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = new Measurement();

		// when
		var opResult = rootPart.RemoveMeasurement( measurement1 );

		// then
		opResult.Should().BeFalse();
		rootPart.MeasurementCount.Should().Be( 0 );
		rootPart.EnumerateMeasurements().Should().NotContain( measurement1 );
		measurement1.Part.Should().BeNull();
	}

	[Test]
	public void WhenClearingMeasurementsFromAPart_ThenTheMeasurementsAreRemoved()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = rootPart.AddMeasurement();
		var measurement2 = rootPart.AddMeasurement();

		// when
		rootPart.ClearMeasurements();

		// then
		rootPart.MeasurementCount.Should().Be( 0 );
		rootPart.EnumerateMeasurements().Should().NotContain( measurement1 );
		rootPart.EnumerateMeasurements().Should().NotContain( measurement2 );
		measurement1.Part.Should().BeNull();
		measurement2.Part.Should().BeNull();
	}

	[Test]
	public void WhenMovingCharacteristicWithinSamePart_ThenMeasuredValuesAreKept()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var char1 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char1" ) );
		var char2 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char2" ) );
		var char2A = char2.AddCharacteristic( new InspectionPlanCharacteristic( "char2A" ) );

		var measurement1 = rootPart.AddMeasurement();
		var m1V1 = measurement1.AddMeasuredValue( char1 );
		var m1V2 = measurement1.AddMeasuredValue( char2 );
		var m1V2A = measurement1.AddMeasuredValue( char2A );

		var measurement2 = rootPart.AddMeasurement();
		var m2V2 = measurement2.AddMeasuredValue( char2 );
		var m2V2A = measurement2.AddMeasuredValue( char2A );

		// when
		rootPart.AddCharacteristic( char2A );

		// then
		rootPart.CharacteristicCount.Should().Be( 3 );
		measurement1.MeasuredValueCount.Should().Be( 3 );
		measurement1.TryGetMeasuredValue( char1, out var fetchedValueM1V1 ).Should().BeTrue();
		fetchedValueM1V1.Should().BeSameAs( m1V1 );
		measurement1.TryGetMeasuredValue( char2, out var fetchedValueM1V2 ).Should().BeTrue();
		fetchedValueM1V2.Should().BeSameAs( m1V2 );
		measurement1.TryGetMeasuredValue( char2A, out var fetchedValueM1V2A ).Should().BeTrue();
		fetchedValueM1V2A.Should().BeSameAs( m1V2A );
		measurement2.TryGetMeasuredValue( char2, out var fetchedValueM2V2 ).Should().BeTrue();
		fetchedValueM2V2.Should().BeSameAs( m2V2 );
		measurement2.TryGetMeasuredValue( char2A, out var fetchedValueM2V2A ).Should().BeTrue();
		fetchedValueM2V2A.Should().BeSameAs( m2V2A );
	}

	[Test]
	public void WhenMovingCharacteristicToAnotherPart_ThenMeasuredValuesAreRemoved()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var char1 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char1" ) );
		var char2 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char2" ) );
		var char2A = char2.AddCharacteristic( new InspectionPlanCharacteristic( "char2A" ) );

		var measurement1 = rootPart.AddMeasurement();
		var m1V1 = measurement1.AddMeasuredValue( char1 );
		measurement1.AddMeasuredValue( char2 );
		measurement1.AddMeasuredValue( char2A );

		var measurement2 = rootPart.AddMeasurement();
		measurement2.AddMeasuredValue( char2 );
		measurement2.AddMeasuredValue( char2A );

		var newRootPart = new InspectionPlanPart( "newRoot" );

		// when
		newRootPart.AddCharacteristic( char2 );

		// then
		rootPart.CharacteristicCount.Should().Be( 1 );
		measurement1.MeasuredValueCount.Should().Be( 1 );
		measurement1.TryGetMeasuredValue( char1, out var fetchedValueM1V1 ).Should().BeTrue();
		fetchedValueM1V1.Should().BeSameAs( m1V1 );
		measurement1.TryGetMeasuredValue( char2, out var fetchedValueM1V2 ).Should().BeFalse();
		fetchedValueM1V2.Should().BeNull();
		measurement1.TryGetMeasuredValue( char2A, out var fetchedValueM1V2A ).Should().BeFalse();
		fetchedValueM1V2A.Should().BeNull();

		measurement2.MeasuredValueCount.Should().Be( 0 );
		measurement2.TryGetMeasuredValue( char2, out var fetchedValueM2V2 ).Should().BeFalse();
		fetchedValueM2V2.Should().BeNull();
		measurement2.TryGetMeasuredValue( char2A, out var fetchedValueM2V2A ).Should().BeFalse();
		fetchedValueM2V2A.Should().BeNull();
	}

	[Test]
	public void WhenRemovingCharacteristic_ThenMeasuredValuesAreRemoved()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var char1 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char1" ) );
		var char2 = rootPart.AddCharacteristic( new InspectionPlanCharacteristic( "char2" ) );
		var char2A = char2.AddCharacteristic( new InspectionPlanCharacteristic( "char2A" ) );

		var measurement1 = rootPart.AddMeasurement();
		var m1V1 = measurement1.AddMeasuredValue( char1 );
		measurement1.AddMeasuredValue( char2 );
		measurement1.AddMeasuredValue( char2A );

		var measurement2 = rootPart.AddMeasurement();
		measurement2.AddMeasuredValue( char2 );
		measurement2.AddMeasuredValue( char2A );

		// when
		rootPart.RemoveEntity( char2 );

		// then
		rootPart.CharacteristicCount.Should().Be( 1 );
		measurement1.MeasuredValueCount.Should().Be( 1 );
		measurement1.TryGetMeasuredValue( char1, out var fetchedValueM1V1 ).Should().BeTrue();
		fetchedValueM1V1.Should().BeSameAs( m1V1 );
		measurement1.TryGetMeasuredValue( char2, out var fetchedValueM1V2 ).Should().BeFalse();
		fetchedValueM1V2.Should().BeNull();
		measurement1.TryGetMeasuredValue( char2A, out var fetchedValueM1V2A ).Should().BeFalse();
		fetchedValueM1V2A.Should().BeNull();

		measurement2.MeasuredValueCount.Should().Be( 0 );
		measurement2.TryGetMeasuredValue( char2, out var fetchedValueM2V2 ).Should().BeFalse();
		fetchedValueM2V2.Should().BeNull();
		measurement2.TryGetMeasuredValue( char2A, out var fetchedValueM2V2A ).Should().BeFalse();
		fetchedValueM2V2A.Should().BeNull();
	}

	[Test]
	public void WhenCheckingContainsMeasurementAndMeasurementDoesNotExist_ThenTheResultIsFalse()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		var measurement1 = new Measurement();

		// when
		var opResult = rootPart.ContainsMeasurement( measurement1 );

		// then
		opResult.Should().BeFalse();
	}

	[Test]
	public void WhenCheckingContainsMeasurementAndMeasurementDoesNotExistAndMeasurementsAreIndexed_ThenTheResultIsFalse()
	{
		// given
		var rootPart = new InspectionPlanPart( "root" );
		for( var i = 0; i < 20; ++i )
			rootPart.AddMeasurement();
		var measurement21 = new Measurement();

		// when
		var opResult = rootPart.ContainsMeasurement( measurement21 );

		// then
		opResult.Should().BeFalse();
	}

	#endregion
}