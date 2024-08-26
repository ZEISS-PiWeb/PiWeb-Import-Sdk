#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

// This is a modification of
// https://github.com/dotnet/corefxlab/blob/8cd063a1e163a0db906343bb5ba1b53c02eb83d6/src/System.Collections.Sequences/System/Collections/Sequences/ResizableArray.cs

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// https://github.com/dotnet/corefxlab/blob/8cd063a1e163a0db906343bb5ba1b53c02eb83d6/LICENSE


using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Zeiss.PiWeb.Sdk.Common.Collections;

/// <summary>
/// Represents a List{T} like type designed to be embedded in other types. This may save indirections and heap allocations.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
internal struct ResizableArray<T>(T[] array, int count = 0)
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ResizableArray{T}"/> class.
    /// </summary>
    /// <param name="capacity">The initial capacity.</param>
    public ResizableArray(int capacity) : this(capacity == 0 ? [] : new T[capacity])
    {
    }

    #endregion

    #region properties

    public T[] Items { get; private set; } = array;

    public int Count { get; private set; } = count;

    public int Capacity => Items.Length;

    public T this[int index]
    {
        get
        {
            if (index > Count - 1) throw new IndexOutOfRangeException();
            return Items[index];
        }
        set
        {
            if (index > Count - 1) throw new IndexOutOfRangeException();
            Items[index] = value;
        }
    }

    public Span<T> Span => new Span<T>(Items, 0, Count);

    public ArraySegment<T> Full => new ArraySegment<T>(Items, 0, Count);
    public ArraySegment<T> Free => new ArraySegment<T>(Items, Count, Items.Length - Count);

    public Span<T> FreeSpan => new Span<T>(Items, Count, Items.Length - Count);

    public Memory<T> FreeMemory => new Memory<T>(Items, Count, Items.Length - Count);

    public int FreeCount => Items.Length - Count;

    #endregion

    #region methods

    public void Add(T item)
    {
        if (Items.Length == Count)
            Resize();

        Items[Count++] = item;
    }

    public void AddAll(T[] items)
    {
        if (items.Length > Items.Length - Count)
        {
            Resize(items.Length + Count);
        }

        items.CopyTo(Items, Count);
        Count += items.Length;
    }

    public void AddAll(ReadOnlySpan<T> items)
    {
        if (items.Length > Items.Length - Count)
        {
            Resize(items.Length + Count);
        }

        items.CopyTo(new Span<T>(Items)[Count..]);
        Count += items.Length;
    }

    public void Insert(int index, T item)
    {
        // Note that insertions at the end are legal.
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (Items.Length == Count)
            Resize(Count + 1);

        if (index < Count)
            Array.Copy(Items, index, Items, index + 1, Count - index);

        Items[index] = item;
        ++Count;
    }

    public void Clear()
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            var size = Count;
            Count = 0;
            if (size > 0)
            {
                // Clear the elements so that the gc can reclaim the references.
                Array.Clear(Items, 0, size);
            }
        }
        else
        {
            Count = 0;
        }
    }

    public T[] Resize(int newSize = -1)
    {
        var oldArray = Items;
        if (newSize == -1)
        {
            if (Items == null || Items.Length == 0)
                newSize = 1;
            else if (Items.Length == 1)
                newSize = 4;
            else
                newSize = Items.Length << 1;
        }

        var newArray = new T[newSize];
        Items.AsSpan(0, Count).CopyTo(newArray); // CopyTo will throw if newArray.Length < _count
        Items = newArray;
        return oldArray;
    }

    public T[] Resize(T[] newArray)
    {
        var oldArray = Items;
        Items.AsSpan(0, Count).CopyTo(newArray); // CopyTo will throw if newArray.Length < _count
        Items = newArray;
        return oldArray;
    }

    public bool TryGet(ref SequencePosition position, [MaybeNullWhen(false)] out T item, bool advance = true)
    {
        var index = position.GetInteger();
        if (index < Count)
        {
            item = Items[index];
            if (advance)
                position = new SequencePosition(null, index + 1);

            return true;
        }

        item = default;
        position = default;
        return false;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index > Count - 1)
            throw new ArgumentOutOfRangeException(nameof(index));

        var removedItem = Items[index];

        --Count;
        if (index < Count)
            Array.Copy(Items, index + 1, Items, index, Count - index);

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            Items[Count] = default!;

        return removedItem;
    }

    #endregion
}