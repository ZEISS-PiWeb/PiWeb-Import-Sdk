#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents an inspection plan characteristic to be imported.
/// </summary>
[DebuggerDisplay("{ToString(), nq}")]
public sealed class InspectionPlanCharacteristic : InspectionPlanEntity
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="InspectionPlanCharacteristic"/> class.
    /// </summary>
    /// <param name="name">The name of the characteristic.</param>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public InspectionPlanCharacteristic(string name) : base(Guid.NewGuid())
    {
        if (string.IsNullOrEmpty(name))
            throw new ImportDataException("The name of an inspection plan characteristic must be non-empty.");

        _Name = name;
    }

    #endregion

    #region members

    // Used to manage characteristics
    private EmbeddedEntityCollection<InspectionPlanCharacteristic> _Characteristics =
        new EmbeddedEntityCollection<InspectionPlanCharacteristic>();

    private string _Name;
    private InspectionPlanEntity? _Parent;

    #endregion

    #region properties

    /// <inheritdoc />
    public override string Name => _Name;

    /// <inheritdoc />
    public override InspectionPlanEntityType InspectionPlanEntityType => InspectionPlanEntityType.Characteristic;

    /// <inheritdoc />
    public override InspectionPlanEntity? Parent => _Parent;

    /// <inheritdoc />
    public override int PartCount => 0;

    /// <inheritdoc />
    public override int CharacteristicCount => _Characteristics.Count;

    /// <inheritdoc />
    public override int EntityCount => _Characteristics.Count;

    /// <inheritdoc />
    public override EntityType EntityType => EntityType.Characteristic;

    #endregion

    #region methods

    private void NotifyMeasurementsIfNecessary(InspectionPlanCharacteristic removedCharacteristic)
    {
        // if a characteristic was removed and this characteristic was not moved within the same part,
        // we need to remove all measured values of this characteristic from all measurements to stay consistent.

        if (this.HasSameMeasurementRootAs(removedCharacteristic, out var measurementRoot) || measurementRoot == null)
            return;

        foreach (var measurement in measurementRoot.EnumerateMeasurements())
            measurement.NotifyCharacteristicRemoved(removedCharacteristic);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{InspectionPlanEntityType} '{Name}'";
    }

    /// <summary>
    /// Validates that the given entity is not an ancestor of this entity.
    /// </summary>
    private void ValidateAcyclic(InspectionPlanEntity entityToValidate)
    {
        InspectionPlanEntity? currentEntity = this;
        while (currentEntity != null)
        {
            if (ReferenceEquals(currentEntity, entityToValidate))
            {
                throw new ImportDataException(
                    "Cannot add characteristic because the new characteristic would be an ancestor of itself " +
                    "(cyclic insertion).");
            }

            currentEntity = currentEntity.Parent;
        }
    }

    /// <inheritdoc />
    public override void Rename(string newName)
    {
        if (Parent?.TryGetEntity(newName, out var existingEntity) == true)
        {
            if (existingEntity.InspectionPlanEntityType is InspectionPlanEntityType.Characteristic)
            {
                throw new ImportDataException(
                    "Cannot rename characteristic because a characteristic with the new name already " +
                    "exists on the parent entity.");
            }
            else
            {
                throw new ImportDataException(
                    "Cannot rename characteristic because a part with the new name already " +
                    "exists on the parent entity.");
            }
        }

        var previousName = Name;
        _Name = newName;

        Parent?.NotifyChildRenamed(this, previousName);
    }

    /// <inheritdoc />
    public override bool ContainsPart(string name)
    {
        return false;
    }

    /// <inheritdoc />
    public override bool ContainsCharacteristic(string name)
    {
        return _Characteristics.HasEntity(name);
    }

    /// <inheritdoc />
    public override bool ContainsEntity(string name)
    {
        return ContainsCharacteristic(name);
    }

    /// <inheritdoc />
    public override bool TryGetPart(string name, [MaybeNullWhen(false)] out InspectionPlanPart resultPart)
    {
        resultPart = null;
        return false;
    }

    /// <inheritdoc />
    public override bool TryGetCharacteristic(
        string name,
        [MaybeNullWhen(false)] out InspectionPlanCharacteristic resultCharacteristic)
    {
        return _Characteristics.TryGetEntity(name, out resultCharacteristic);
    }

    /// <inheritdoc />
    public override bool TryGetEntity(string name, [MaybeNullWhen(false)] out InspectionPlanEntity resultEntity)
    {
        if (TryGetCharacteristic(name, out var resultCharacteristic))
        {
            resultEntity = resultCharacteristic;
            return true;
        }

        resultEntity = null;
        return false;
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic GetOrSetCharacteristic(string name,
        out InspectionPlanPart? replacedPart)
    {
        // Note: This is not fully optimized yet. TryGetCharacteristic() and SetCharacteristic() both check whether a characteristic
        // with the given name exists. We could reduce this to one check by not reusing these methods.

        if (TryGetCharacteristic(name, out var existingCharacteristic))
        {
            replacedPart = null;
            return existingCharacteristic;
        }

        var newCharacteristic = SetCharacteristic(name, out var replacedEntity);
        replacedPart = replacedEntity as InspectionPlanPart;

        return newCharacteristic;
    }

    /// <inheritdoc />
    public override IEnumerable<InspectionPlanPart> EnumerateParts()
    {
        return [];
    }

    /// <inheritdoc />
    public override IEnumerable<InspectionPlanCharacteristic> EnumerateCharacteristics()
    {
        return _Characteristics.GetEntities();
    }

    /// <inheritdoc />
    public override IEnumerable<InspectionPlanEntity> EnumerateEntities()
    {
        return _Characteristics.GetEntities();
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic AddCharacteristic(InspectionPlanCharacteristic characteristic)
    {
        if (ContainsCharacteristic(characteristic.Name))
        {
            throw new ImportDataException(
                "Cannot add characteristic because a characteristic with this name already exists.");
        }

        ValidateAcyclic(characteristic);

        _Characteristics.AddUnsafe(characteristic);
        characteristic.NotifyParentChanged(this);

        return characteristic;
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic AddCharacteristic(string name)
    {
        return AddCharacteristic(new InspectionPlanCharacteristic(name));
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic SetCharacteristic(
        InspectionPlanCharacteristic characteristic,
        out InspectionPlanEntity? replacedEntity)
    {
        var (indexToReplace, characteristicToReplace) = _Characteristics.GetIndexEntityPair(characteristic.Name);
        if (ReferenceEquals(characteristicToReplace, characteristic))
        {
            replacedEntity = null;
            return characteristic;
        }

        replacedEntity = characteristicToReplace;

        ValidateAcyclic(characteristic);

        if (characteristicToReplace is null)
        {
            _Characteristics.AddUnsafe(characteristic);
        }
        else
        {
            _Characteristics.ReplaceUnsafe(indexToReplace, characteristic);
            characteristicToReplace.NotifyParentChanged(null);
        }

        characteristic.NotifyParentChanged(this);
        return characteristic;
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic SetCharacteristic(string name,
        out InspectionPlanEntity? replacedEntity)
    {
        return SetCharacteristic(new InspectionPlanCharacteristic(name), out replacedEntity);
    }

    /// <inheritdoc />
    public override bool RemoveEntity(InspectionPlanEntity entity)
    {
        if (entity is not InspectionPlanCharacteristic characteristic)
            return false;

        if (!_Characteristics.RemoveEntity(characteristic))
            return false;

        entity.NotifyParentChanged(null);
        NotifyMeasurementsIfNecessary(characteristic);

        return true;
    }

    /// <inheritdoc />
    public override bool RemoveEntity(string entityName)
    {
        if (!_Characteristics.RemoveEntity(entityName, out var removedEntity))
            return false;

        removedEntity.NotifyParentChanged(null);
        NotifyMeasurementsIfNecessary(removedEntity);
        return true;
    }

    /// <inheritdoc />
    public override InspectionPlanPart? GetParentPart()
    {
        var currentParentEntity = Parent;
        while (currentParentEntity is not null)
        {
            if (currentParentEntity is InspectionPlanPart resultPart)
                return resultPart;

            currentParentEntity = currentParentEntity.Parent;
        }

        return null;
    }

    /// <inheritdoc />
    public override void PrintTree(StringBuilder builder, int level = 0, int indentation = 4)
    {
        var indentationString = new string(' ', level * indentation);
        builder.Append(indentationString).AppendLine(ToString());

        foreach (var childEntity in EnumerateCharacteristics())
            childEntity.PrintTree(builder, level + 1, indentation);
    }

    /// <inheritdoc />
    internal override void NotifyParentChanged(InspectionPlanEntity? newParentEntity)
    {
        if (ReferenceEquals(Parent, newParentEntity))
            return;

        if (newParentEntity is null)
        {
            // Make sure this entity was actually removed from its parent
            if (Parent?.TryGetCharacteristic(Name, out var existingEntity) == true &&
                ReferenceEquals(existingEntity, this))
                return;

            _Parent = null;
            return;
        }

        // Make sure entity was actually moved to the new part
        if (!newParentEntity.TryGetCharacteristic(Name, out var newChildEntity) ||
            !ReferenceEquals(newChildEntity, this))
            return;

        var previousParent = Parent;
        _Parent = newParentEntity;

        previousParent?.NotifyChildRemoved(this);
    }

    /// <inheritdoc />
    internal override void NotifyChildRenamed(InspectionPlanEntity inspectionPlanEntity, string previousName)
    {
        // If this entity is not indexed yet, we do not need to react to renaming.
        if (!_Characteristics.IsIndexed)
            return;

        // Characteristics can only contain other characteristics
        if (inspectionPlanEntity is not InspectionPlanCharacteristic)
            return;

        // Check integrity of the rename
        if (!_Characteristics.TryGetEntity(previousName, out var previousEntity))
            return;
        if (!ReferenceEquals(inspectionPlanEntity, previousEntity)
            || !ReferenceEquals(inspectionPlanEntity.Parent, this))
            return;

        _Characteristics.ReIndex(previousName, previousEntity);
    }

    /// <inheritdoc />
    internal override void NotifyChildRemoved(InspectionPlanEntity removedEntity)
    {
        if (ReferenceEquals(removedEntity.Parent, this))
            return;

        // Characteristics can only contain other characteristics
        if (removedEntity is not InspectionPlanCharacteristic removedCharacteristic)
            return;

        _Characteristics.RemoveEntity(removedCharacteristic);
        NotifyMeasurementsIfNecessary(removedCharacteristic);
    }

    #endregion
}