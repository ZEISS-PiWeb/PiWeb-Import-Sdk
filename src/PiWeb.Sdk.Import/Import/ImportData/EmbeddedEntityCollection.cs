#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Zeiss.PiWeb.Sdk.Common.Collections;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

internal struct EmbeddedEntityCollection<T> where T : InspectionPlanEntity
{
    #region members

    // Used to manage children only in entities with more than 8 children. It is instantiated lazily when necessary.
    private Dictionary<string, T>? _EntityMap = null;

    // Most entities will have no children or just a few children so we use a simple list here.
    // For the few entities that have a larger list of children we instantiate and use the child map instead.
    private ResizableArray<T> _Entities = new ResizableArray<T>(0);

    #endregion

    #region constructors

    /// Initializes a new instance of the <see cref="EmbeddedEntityCollection{T}"/> class.
    public EmbeddedEntityCollection()
    {
    }

    #endregion

    #region properties

    public int Count => _EntityMap?.Count ?? _Entities.Count;
    public bool IsIndexed => _EntityMap != null;

    #endregion

    #region methods

    public bool HasEntity(string entityName)
    {
        if (_EntityMap is not null)
            return _EntityMap.ContainsKey(entityName);

        var index = IndexOf(entityName);
        return index >= 0;
    }

    public bool TryGetEntity(string entityName, [MaybeNullWhen(false)] out T entity)
    {
        if (_EntityMap is not null)
            return _EntityMap.TryGetValue(entityName, out entity);

        var index = IndexOf(entityName);
        if (index < 0)
        {
            entity = null;
            return false;
        }

        entity = _Entities[index];
        return true;
    }

    public IEnumerable<T> GetEntities()
    {
        // Note that the items array can be larger than the actual amount of items so we need to take the actual
        // item count.

        return _EntityMap?.Values ?? _Entities.Items.Take(Count);
    }

    public (int, T?) GetIndexEntityPair(string entityName)
    {
        var index = -1;
        T? entity;

        if (_EntityMap is not null)
        {
            _EntityMap.TryGetValue(entityName, out entity);
        }
        else
        {
            index = IndexOf(entityName);
            entity = index < 0
                ? null
                : _Entities[index];
        }

        return (index, entity);
    }

    public void AddUnsafe(T newEntity)
    {
        // We assume the entity is not already in this collection, caller must be sure of this.

        if (_EntityMap is null && _Entities.Count >= 8)
            MakeIndex();

        if (_EntityMap is not null)
            _EntityMap.Add(newEntity.Name, newEntity);
        else
            _Entities.Add(newEntity);
    }

    public void ReplaceUnsafe(int index, T newEntity)
    {
        // We do not check whether the index is valid and correct here, the caller must be sure of this.

        if (_EntityMap is not null)
            _EntityMap[newEntity.Name] = newEntity;
        else
            _Entities[index] = newEntity;
    }

    public bool RemoveEntity(T entityToRemove)
    {
        if (_EntityMap is not null)
        {
            if (!_EntityMap.TryGetValue(entityToRemove.Name, out var existingEntity) ||
                !ReferenceEquals(existingEntity, entityToRemove))
                return false;

            return _EntityMap.Remove(entityToRemove.Name);
        }
        else
        {
            var removalIndex = IndexOf(entityToRemove);
            if (removalIndex < 0)
                return false;

            _Entities.RemoveAt(removalIndex);
            return true;
        }
    }

    public bool RemoveEntity(string entityName, [MaybeNullWhen(false)] out T removedEntity)
    {
        removedEntity = null;

        if (_EntityMap is not null)
            return _EntityMap.Remove(entityName, out removedEntity);

        var removalIndex = IndexOf(entityName);
        if (removalIndex < 0)
            return false;

        removedEntity = _Entities.RemoveAt(removalIndex);
        return true;
    }

    public void ReIndex(string previousName, T entity)
    {
        _EntityMap?.Remove(previousName);
        _EntityMap?.Add(entity.Name, entity);
    }

    private int IndexOf(T entity)
    {
        for (var i = 0; i < _Entities.Count; ++i)
        {
            if (!ReferenceEquals(_Entities[i], entity))
                continue;

            return i;
        }

        return -1;
    }

    private int IndexOf(string entityName)
    {
        for (var i = 0; i < _Entities.Count; ++i)
        {
            if (!string.Equals(_Entities[i].Name, entityName, StringComparison.OrdinalIgnoreCase))
                continue;

            return i;
        }

        return -1;
    }

    private void MakeIndex()
    {
        _EntityMap = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < _Entities.Count; ++i)
        {
            _EntityMap.Add(_Entities[i].Name, _Entities[i]);
        }

        _Entities = new ResizableArray<T>(0);
    }

    #endregion
}