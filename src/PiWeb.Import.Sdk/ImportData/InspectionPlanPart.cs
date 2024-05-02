#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Zeiss.PiWeb.Import.Sdk.Collections;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents an inspection plan part to be imported.
/// </summary>
[DebuggerDisplay("{ToString(), nq}")]
public sealed class InspectionPlanPart : InspectionPlanEntity
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="InspectionPlanPart"/> class.
    /// </summary>
    /// <param name="name">The name of the part.</param>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public InspectionPlanPart(string name) : base(Guid.NewGuid())
    {
        if (string.IsNullOrEmpty(name))
            throw new ImportDataException("The name of an inspection plan part must be non-empty.");

        _Name = name;
    }

    #endregion

    #region members

    // Used to manage inspection plan entities
    private EmbeddedEntityCollection<InspectionPlanPart> _Parts = new EmbeddedEntityCollection<InspectionPlanPart>();

    private EmbeddedEntityCollection<InspectionPlanCharacteristic> _Characteristics =
        new EmbeddedEntityCollection<InspectionPlanCharacteristic>();

    // Used to manage measurements.
    private ResizableArray<Measurement> _Measurements = new ResizableArray<Measurement>(0);
    private HashSet<Measurement>? _MeasurementSet;

    private string _Name;
    private InspectionPlanEntity? _Parent;

    #endregion

    #region properties

    /// <inheritdoc />
    public override string Name => _Name;

    /// <inheritdoc />
    public override InspectionPlanEntityType InspectionPlanEntityType => InspectionPlanEntityType.Part;

    /// <inheritdoc />
    public override InspectionPlanEntity? Parent => _Parent;

    /// <inheritdoc />
    public override int PartCount => _Parts.Count;

    /// <inheritdoc />
    public override int CharacteristicCount => _Characteristics.Count;

    /// <inheritdoc />
    public override int EntityCount => _Parts.Count + _Characteristics.Count;

    /// <inheritdoc />
    public override EntityType EntityType => EntityType.Part;

    /// <summary>
    /// The number of measurements this part has.
    /// </summary>
    public int MeasurementCount => _Measurements.Count;

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

    private int IndexOf(Measurement measurement)
    {
        for (var i = _Measurements.Count - 1; i > -1; --i)
        {
            if (ReferenceEquals(_Measurements[i], measurement))
                return i;
        }

        return -1;
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
                    "Cannot add part because the new part would be an ancestor of itself " +
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
                    "Cannot rename part because a characteristic with the new name already " +
                    "exists on the parent entity.");
            }

            throw new ImportDataException(
                "Cannot rename part because a part with the new name already " +
                "exists on the parent entity.");
        }

        var previousName = Name;
        _Name = newName;

        Parent?.NotifyChildRenamed(this, previousName);
    }

    /// <inheritdoc />
    public override bool ContainsPart(string name)
    {
        return _Parts.HasEntity(name);
    }

    /// <inheritdoc />
    public override bool ContainsCharacteristic(string name)
    {
        return _Characteristics.HasEntity(name);
    }

    /// <inheritdoc />
    public override bool ContainsEntity(string name)
    {
        return ContainsPart(name) || ContainsCharacteristic(name);
    }

    /// <inheritdoc />
    public override bool TryGetPart(
        string name,
        [MaybeNullWhen(false)] out InspectionPlanPart resultPart)
    {
        return _Parts.TryGetEntity(name, out resultPart);
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
        if (TryGetPart(name, out var resultPart))
        {
            resultEntity = resultPart;
            return true;
        }

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
        return _Parts.GetEntities();
    }

    /// <summary>
    /// Gets the part of the given name if it exists. If a part of the given name does not yet exist, a new part with the given
    /// name is added beforehand, potentially replacing an existing characteristic of the same name.
    /// Note: Replacing a characteristic also removes any measured values of this characteristic from all measurements.
    /// </summary>
    /// <param name="name">The name of the part to get or set.</param>
    /// <param name="replacedCharacteristic">Receives a replaced characteristic or null when no characteristic was replaced.</param>
    /// <returns>The existing or newly added part.</returns>
    public InspectionPlanPart GetOrSetPart(string name, out InspectionPlanCharacteristic? replacedCharacteristic)
    {
        // Note: This is not fully optimized yet. TryGetPart() and SetPart() both check whether a part
        // with the given name exists. We could reduce this to one check by not reusing these methods.

        if (TryGetPart(name, out var existingPart))
        {
            replacedCharacteristic = null;
            return existingPart;
        }

        var newPart = SetPart(name, out var replacedEntity);
        replacedCharacteristic = replacedEntity as InspectionPlanCharacteristic;

        return newPart;
    }

    /// <inheritdoc />
    public override IEnumerable<InspectionPlanCharacteristic> EnumerateCharacteristics()
    {
        return _Characteristics.GetEntities();
    }

    /// <inheritdoc />
    public override IEnumerable<InspectionPlanEntity> EnumerateEntities()
    {
        return _Parts.GetEntities().Concat((IEnumerable<InspectionPlanEntity>)_Characteristics.GetEntities());
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic AddCharacteristic(string name)
    {
        return AddCharacteristic(new InspectionPlanCharacteristic(name));
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic AddCharacteristic(InspectionPlanCharacteristic characteristic)
    {
        if (ContainsCharacteristic(characteristic.Name))
        {
            throw new ImportDataException(
                "Cannot add characteristic because a characteristic with the same name already exists.");
        }

        if (ContainsPart(characteristic.Name))
        {
            throw new ImportDataException(
                "Cannot add characteristic because a part with the same name already exists.");
        }

        // No cyclic insertion validation necessary because we are inserting a characteristic and there can only be parts
        // further up.

        _Characteristics.AddUnsafe(characteristic);
        characteristic.NotifyParentChanged(this);

        return characteristic;
    }

    /// <summary>
    /// Adds a new part with the given name.
    /// </summary>
    /// <param name="name">The name of the new part to add.</param>
    /// <returns>The added part.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    /// <exception cref="ImportDataException">Thrown when a characteristic or part of the given name already exists.</exception>
    public InspectionPlanPart AddPart(string name)
    {
        return AddPart(new InspectionPlanPart(name));
    }

    /// <summary>
    /// Adds the given part.
    /// </summary>
    /// <param name="part">The part to add.</param>
    /// <returns>The added part.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when a characteristic or part of the given name already exists.
    /// </exception>
    /// <exception cref="ImportDataException">
    /// Thrown when the part to add is already an ancestor.
    /// </exception>
    public InspectionPlanPart AddPart(InspectionPlanPart part)
    {
        if (ContainsCharacteristic(part.Name))
        {
            throw new ImportDataException(
                "Cannot add part because a characteristic with the same name already exists.");
        }

        if (ContainsPart(part.Name))
        {
            throw new ImportDataException(
                "Cannot add part because a part with the same name already exists.");
        }

        ValidateAcyclic(part);

        _Parts.AddUnsafe(part);
        part.NotifyParentChanged(this);

        return part;
    }

    /// <inheritdoc />
    public override InspectionPlanCharacteristic SetCharacteristic(string name,
        out InspectionPlanEntity? replacedEntity)
    {
        return SetCharacteristic(new InspectionPlanCharacteristic(name), out replacedEntity);
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

        // No cyclic insertion validation necessary because we are inserting a characteristic and there can only be parts
        // further up.

        if (characteristicToReplace is not null)
        {
            _Characteristics.ReplaceUnsafe(indexToReplace, characteristic);
            replacedEntity = characteristicToReplace;
            characteristicToReplace.NotifyParentChanged(null);
        }
        else
        {
            _Characteristics.AddUnsafe(characteristic);

            // If there is a part of the same name, we need to remove it.
            if (_Parts.RemoveEntity(characteristic.Name, out var removedPart))
            {
                replacedEntity = removedPart;
                removedPart.NotifyParentChanged(null);
            }
            else
            {
                replacedEntity = null;
            }
        }

        characteristic.NotifyParentChanged(this);
        return characteristic;
    }

    /// <summary>
    /// Adds the given part potentially replacing an existing inspection plan entity of the same name.
    /// </summary>
    /// <param name="part">The part to add.</param>
    /// <param name="replacedEntity">
    /// Receives the replaced inspection plan entity or null when no inspection plan entity was replaced. A part is never
    /// replaced by itself when it already exists and null is received in such a case.
    /// </param>
    /// <returns>The set part.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when the part to add is already an ancestor.
    /// </exception>
    public InspectionPlanPart SetPart(InspectionPlanPart part, out InspectionPlanEntity? replacedEntity)
    {
        var (indexToReplace, partToReplace) = _Parts.GetIndexEntityPair(part.Name);
        if (ReferenceEquals(partToReplace, part))
        {
            replacedEntity = null;
            return part;
        }

        ValidateAcyclic(part);

        if (partToReplace is not null)
        {
            _Parts.ReplaceUnsafe(indexToReplace, part);
            replacedEntity = partToReplace;
            partToReplace.NotifyParentChanged(null);
        }
        else
        {
            _Parts.AddUnsafe(part);

            // If there is a characteristic of the same name, we need to remove it.
            if (_Characteristics.RemoveEntity(part.Name, out var removedCharacteristic))
            {
                replacedEntity = removedCharacteristic;
                removedCharacteristic.NotifyParentChanged(null);
            }
            else
            {
                replacedEntity = null;
            }
        }

        part.NotifyParentChanged(this);
        return part;
    }

    /// <summary>
    /// Adds a new part with the given name potentially replacing an existing inspection plan entity of the same name.
    /// </summary>
    /// <param name="name">The name of the new part to add.</param>
    /// <param name="replacedEntity">
    /// Receives the replaced inspection plan entity or null when no inspection plan entity was replaced.
    /// </param>
    /// <returns>The set part.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public InspectionPlanPart SetPart(string name, out InspectionPlanEntity? replacedEntity)
    {
        return SetPart(new InspectionPlanPart(name), out replacedEntity);
    }

    /// <inheritdoc />
    public override bool RemoveEntity(InspectionPlanEntity entity)
    {
        if (entity is InspectionPlanPart part)
        {
            if (!_Parts.RemoveEntity(part))
                return false;

            entity.NotifyParentChanged(null);
            return true;
        }

        if (entity is InspectionPlanCharacteristic characteristic)
        {
            if (!_Characteristics.RemoveEntity(characteristic))
                return false;

            entity.NotifyParentChanged(null);
            NotifyMeasurementsIfNecessary(characteristic);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public override bool RemoveEntity(string entityName)
    {
        if (_Parts.RemoveEntity(entityName, out var removedPart))
        {
            removedPart.NotifyParentChanged(null);
            return true;
        }

        if (_Characteristics.RemoveEntity(entityName, out var removedCharacteristic))
        {
            removedCharacteristic.NotifyParentChanged(null);
            NotifyMeasurementsIfNecessary(removedCharacteristic);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public override InspectionPlanPart? GetParentPart()
    {
        return Parent as InspectionPlanPart;
    }

    /// <inheritdoc />
    public override void PrintTree(StringBuilder builder, int level = 0, int indentation = 4)
    {
        if (level < 0)
            level = 0;

        if (indentation < 0)
            indentation = 0;

        var indentationString = new string(' ', level * indentation);
        builder.Append(indentationString).AppendLine(ToString());

        foreach (var childPart in EnumerateParts())
            childPart.PrintTree(builder, level + 1, indentation);

        foreach (var childCharacteristic in EnumerateCharacteristics())
            childCharacteristic.PrintTree(builder, level + 1, indentation);
    }

    /// <inheritdoc />
    internal override void NotifyChildRenamed(InspectionPlanEntity inspectionPlanEntity, string previousName)
    {
        if (inspectionPlanEntity is InspectionPlanPart)
        {
            // If this entity is not indexed yet, we do not need to react to renaming.
            if (!_Parts.IsIndexed)
                return;

            // Check integrity of the rename
            if (!_Parts.TryGetEntity(previousName, out var previousEntity))
                return;
            if (!ReferenceEquals(inspectionPlanEntity, previousEntity)
                || !ReferenceEquals(inspectionPlanEntity.Parent, this))
                return;

            _Parts.ReIndex(previousName, previousEntity);
        }
        else if (inspectionPlanEntity is InspectionPlanCharacteristic)
        {
            // If this entity is not indexed yet, we do not need to react to renaming.
            if (!_Characteristics.IsIndexed)
                return;

            // Check integrity of the rename
            if (!_Characteristics.TryGetEntity(previousName, out var previousEntity))
                return;
            if (!ReferenceEquals(inspectionPlanEntity, previousEntity)
                || !ReferenceEquals(inspectionPlanEntity.Parent, this))
                return;

            _Characteristics.ReIndex(previousName, previousEntity);
        }
    }

    /// <inheritdoc />
    internal override void NotifyChildRemoved(InspectionPlanEntity removedEntity)
    {
        if (ReferenceEquals(removedEntity.Parent, this))
            return;

        if (removedEntity is InspectionPlanCharacteristic removedCharacteristic)
        {
            _Characteristics.RemoveEntity(removedCharacteristic);
            NotifyMeasurementsIfNecessary(removedCharacteristic);
        }
        else if (removedEntity is InspectionPlanPart removedPart)
        {
            _Parts.RemoveEntity(removedPart);
        }
    }

    /// <inheritdoc />
    internal override void NotifyParentChanged(InspectionPlanEntity? newParentEntity)
    {
        if (ReferenceEquals(Parent, newParentEntity))
            return;

        if (newParentEntity is null)
        {
            // Make sure this entity was actually removed from its parent
            if (Parent is not InspectionPlanPart parentPart
                || (parentPart.TryGetPart(Name, out var existingEntity) && ReferenceEquals(existingEntity, this)))
                return;

            _Parent = null;
            return;
        }

        // Make sure entity was actually moved to the new part
        if (newParentEntity is not InspectionPlanPart newParentPart
            || !newParentPart.TryGetPart(Name, out var newChildEntity)
            || !ReferenceEquals(newChildEntity, this))
            return;

        var previousParent = Parent;
        _Parent = newParentEntity;

        previousParent?.NotifyChildRemoved(this);
    }

    /// <summary>
    /// Determines whether this inspection plan entity has the given measurement.
    /// </summary>
    /// <param name="measurement">The inspection plan entity to search for.</param>
    /// <returns>True if the part has the measurement; otherwise, false.</returns>
    public bool ContainsMeasurement(Measurement measurement)
    {
        if (_MeasurementSet != null)
            return _MeasurementSet.Contains(measurement);

        for (var i = _Measurements.Count - 1; i > -1; --i)
        {
            if (ReferenceEquals(_Measurements[i], measurement))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Adds a new measurement to this part.
    /// </summary>
    /// <returns>The added measurement.</returns>
    public Measurement AddMeasurement()
    {
        return AddMeasurement(new Measurement());
    }

    /// <summary>
    /// Adds the given measurement to this part. This removes all measured values from the measurement.
    /// </summary>
    /// <param name="measurement">The measurement to add to this part.</param>
    /// <returns>The added measurement.</returns>
    public Measurement AddMeasurement(Measurement measurement)
    {
        if (ReferenceEquals(this, measurement.Part))
            return measurement;

        if (_MeasurementSet is null && _Measurements.Count >= 8)
            _MeasurementSet = new HashSet<Measurement>(EnumerateMeasurements());

        _Measurements.Add(measurement);
        _MeasurementSet?.Add(measurement);

        var oldPart = measurement.Part;
        measurement.NotifyPartChanged(this);
        oldPart?.NotifyMeasurementRemoved(measurement);

        return measurement;
    }

    /// <summary>
    /// Removes the given measurement from this part.
    /// </summary>
    /// <param name="measurement">The measurement to remove.</param>
    /// <returns>True if the measurement was removed; otherwise, false.</returns>
    public bool RemoveMeasurement(Measurement measurement)
    {
        if (_MeasurementSet is not null && !_MeasurementSet.Contains(measurement))
            return false;

        var removalIndex = IndexOf(measurement);
        if (removalIndex < 0)
            return false;

        _Measurements.RemoveAt(removalIndex);
        _MeasurementSet?.Remove(measurement);

        measurement.NotifyPartChanged(null);
        return true;
    }

    /// <summary>
    /// Removes all measurements from this part.
    /// </summary>
    public void ClearMeasurements()
    {
        if (_Measurements.Count == 0)
            return;

        _MeasurementSet = null;
        var oldList = _Measurements;
        _Measurements = new ResizableArray<Measurement>(0);

        for (var i = 0; i < oldList.Count; ++i)
            oldList[i].NotifyPartChanged(null);

        _Measurements = oldList;
        _Measurements.Clear();
    }

    /// <summary>
    /// Gets all measurements of this part.
    /// </summary>
    /// <returns>All measurements of this part.</returns>
    public IEnumerable<Measurement> EnumerateMeasurements()
    {
        return _Measurements.Items.Take(_Measurements.Count);
    }

    /// <summary>
    /// Notifies this inspection plan entity about a removed measurement.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    /// <param name="removedMeasurement">The removed measurement.</param>
    internal void NotifyMeasurementRemoved(Measurement removedMeasurement)
    {
        if (ReferenceEquals(removedMeasurement.Part, this))
            return;

        var removalIndex = IndexOf(removedMeasurement);
        if (removalIndex > -1)
            _Measurements.RemoveAt(removalIndex);

        _MeasurementSet?.Remove(removedMeasurement);
    }

    #endregion
}