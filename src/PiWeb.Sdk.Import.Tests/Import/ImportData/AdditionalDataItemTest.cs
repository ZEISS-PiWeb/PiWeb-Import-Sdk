#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.IO;
using AwesomeAssertions;
using NUnit.Framework;
using Zeiss.PiWeb.Sdk.Import.ImportData;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace Zeiss.PiWeb.Sdk.Import.Tests.Import.ImportData;

[TestFixture]
public class AdditionalDataItemTest
{
	[Test]
	public void WhenGettingDataFromABufferItem_ThenEachStreamReadsTheOriginalBuffer()
	{
		var buffer = new byte[] { 1, 2, 3 };
		var item = new AdditionalBufferDataItem( "data.bin", buffer );

		item.TryGetDataStream( out var firstStream ).Should().BeTrue();
		firstStream.Should().BeOfType<MemoryStream>();
		firstStream.CanWrite.Should().BeFalse();
		var firstBuffer = new byte[buffer.Length];
		firstStream.Read( firstBuffer, 0, firstBuffer.Length ).Should().Be( buffer.Length );
		firstBuffer.Should().Equal( buffer );
		item.DisposeStream( firstStream );
		firstStream.CanRead.Should().BeFalse();

		item.TryGetDataStream( out var secondStream ).Should().BeTrue();
		secondStream.Should().NotBeSameAs( firstStream );
		secondStream.ReadByte().Should().Be( 1 );
		item.DisposeStream( secondStream );
	}

	[Test]
	public void WhenDisposingTheOwnedStream_ThenTheItemNoLongerReturnsIt()
	{
		var sourceStream = new MemoryStream( [1, 2, 3] );
		var otherStream = new MemoryStream();
		var item = new AdditionalStreamDataItem( "data.bin", sourceStream );

		item.TryGetDataStream( out var dataStream ).Should().BeTrue();
		dataStream.Should().BeSameAs( sourceStream );
		item.DisposeStream( otherStream );
		otherStream.CanRead.Should().BeTrue();

		item.DisposeStream( dataStream );
		sourceStream.CanRead.Should().BeFalse();
		item.TryGetDataStream( out var disposedStream ).Should().BeFalse();
		disposedStream.Should().BeNull();

		otherStream.Dispose();
	}

	[Test]
	public void WhenTheStreamItemDoesNotOwnTheStream_ThenDisposeLeavesItAvailable()
	{
		var sourceStream = new MemoryStream( [1, 2, 3] );
		var item = new AdditionalStreamDataItem( "data.bin", sourceStream, false );

		item.TryGetDataStream( out var dataStream ).Should().BeTrue();
		dataStream.Should().BeSameAs( sourceStream );
		item.DisposeStream( dataStream );

		sourceStream.CanRead.Should().BeTrue();
		item.TryGetDataStream( out var sameStream ).Should().BeTrue();
		sameStream.Should().BeSameAs( sourceStream );
		sourceStream.Dispose();
	}

	[Test]
	public void WhenAddingAnAdditionalDataItem_ThenNamesAreComparedWithoutCaseSensitivity()
	{
		var measurement = new Measurement();
		var existingItem = new AdditionalBufferDataItem( "Data.bin", [1] );
		measurement.AddAdditionalData( existingItem );

		measurement.ContainsAdditionalData( "data.BIN" ).Should().BeTrue();
		measurement.Invoking( m => m.AddAdditionalData(
				new AdditionalBufferDataItem( "data.bin", [2] ) ) )
			.Should().Throw<ImportDataException>();

		var replacement = new AdditionalBufferDataItem( "DATA.BIN", [3] );
		measurement.SetAdditionalData( replacement, out var replacedItem ).Should().BeSameAs( replacement );
		replacedItem.Should().BeSameAs( existingItem );
		measurement.TryGetAdditionalData( "data.bin", out var storedItem ).Should().BeTrue();
		storedItem.Should().BeSameAs( replacement );
	}

	[Test]
	public void WhenWrappingAnImportFile_ThenTheFileNameAndStreamAreUsed()
	{
		var sourceStream = new MemoryStream( [1, 2, 3] );
		var importFile = new TestImportFile( "source.bin", () => sourceStream );
		var item = new ImportFileAdditionalDataItem( importFile );

		item.Name.Should().Be( "source.bin" );
		item.TryGetDataStream( out var dataStream ).Should().BeTrue();
		dataStream.Should().BeSameAs( sourceStream );
		item.DisposeStream( dataStream );
		sourceStream.CanRead.Should().BeFalse();
	}

	[Test]
	public void WhenTheImportFileCannotProvideAStream_ThenFalseAndNullAreReturned()
	{
		var importFile = new TestImportFile( "missing.bin", () => throw new IOException() );
		var item = new ImportFileAdditionalDataItem( "renamed.bin", importFile );

		item.Name.Should().Be( "renamed.bin" );
		item.TryGetDataStream( out var dataStream ).Should().BeFalse();
		dataStream.Should().BeNull();
	}

	[Test]
	public void WhenCreatingAnAdditionalDataItemWithAnEmptyName_ThenAnExceptionIsThrown()
	{
		Assert.Throws<ImportDataException>( () => { _ = new AdditionalBufferDataItem( string.Empty, [] ); } );
	}

	private sealed class TestImportFile : IImportFile
	{
		private readonly Func<Stream> _getStream;

		public TestImportFile( string name, Func<Stream> getStream )
		{
			Name = name;
			_getStream = getStream;
		}

		public string Name { get; }
		public string BaseName => Name;
		public string Extension => string.Empty;
		public DateTimeOffset? LastWriteTime => null;
		public DateTimeOffset? CreationTime => null;
		public DateTimeOffset? DiscoveryTime => null;
		public long QueueCount => 1;
		public object? Payload { get; set; }

		public Stream GetDataStream()
		{
			return _getStream();
		}
	}
}
