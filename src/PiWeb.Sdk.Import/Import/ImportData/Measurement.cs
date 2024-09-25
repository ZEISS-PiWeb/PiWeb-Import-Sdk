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

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents a measurement to be imported.
/// </summary>
[DebuggerDisplay("{ToString(), nq}")]
public sealed class Measurement : Entity
{
    #region members

    private readonly Dictionary<InspectionPlanCharacteristic, MeasuredValue> _ValueMap =
        new Dictionary<InspectionPlanCharacteristic, MeasuredValue>();

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Measurement"/> class.
    /// </summary>
    public Measurement() : base(Guid.NewGuid())
    {
    }

    #endregion

    #region properties

    /// <summary>
    /// The part measured by this measurement.
    /// </summary>
    public InspectionPlanPart? Part { get; private set; }

    /// <summary>
    /// Gets the number of measured values in this measurement.
    /// </summary>
    public int MeasuredValueCount => _ValueMap.Count;

    /// <inheritdoc />
    public override EntityType EntityType => EntityType.Measurement;

    #endregion

    #region methods

    private void RemoveMeasuredValues(InspectionPlanCharacteristic removedCharacteristic)
    {
        _ValueMap.Remove(removedCharacteristic);
        foreach (var childEntity in removedCharacteristic.EnumerateCharacteristics())
            RemoveMeasuredValues(childEntity);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Measurement [{MeasuredValueCount} values]";
    }

    /// <summary>
    /// Determines whether this measurement has a measured value for the given characteristic.
    /// </summary>
    /// <param name="characteristic">The characteristic to search a measured value for.</param>
    /// <returns>True if the measurement has a measured value for the given characteristic; otherwise, false.</returns>
    public bool ContainsMeasuredValue(InspectionPlanCharacteristic characteristic)
    {
        return _ValueMap.ContainsKey(characteristic);
    }

    /// <summary>
    /// Gets the measured value for the given characteristic of this measurement if it exists.
    /// </summary>
    /// <param name="characteristic">The characteristic to get a measured value for.</param>
    /// <param name="measuredValue">Receives the measured value if it exists.</param>
    /// <returns>
    /// True if this measurement has a measured value for the given characteristic; otherwise, false.
    /// </returns>
    public bool TryGetMeasuredValue(
        InspectionPlanCharacteristic characteristic,
        [MaybeNullWhen(false)] out MeasuredValue measuredValue)
    {
        return _ValueMap.TryGetValue(characteristic, out measuredValue);
    }

    /// <summary>
    /// Adds the given measured value for the given characteristic. Measured values can only be added for
    /// characteristics of the part this measurement belongs to (see <see cref="Part"/>).
    /// </summary>
    /// <param name="measuredCharacteristic">The characteristic to add a measured value for.</param>
    /// <param name="measuredValue">The measured value to add.</param>
    /// <returns>The added measured value.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when <paramref name="measuredCharacteristic"/> does not belong to the same part as this measurement
    /// or when the measurement does not belong to a part at all.
    /// </exception>
    /// <exception cref="ImportDataException">
    /// Thrown when a measured value for the given inspection plan characteristic already exists in this measurement.
    /// </exception>
    public MeasuredValue AddMeasuredValue(
        InspectionPlanCharacteristic measuredCharacteristic,
        MeasuredValue measuredValue)
    {
        if (!ReferenceEquals(measuredCharacteristic.GetParentPart(), Part))
        {
            throw new ImportDataException(
                "Cannot add a measured value to a measurement for a characteristic that belongs to " +
                "a different part than the measurement.");
        }

        try
        {
            _ValueMap.Add(measuredCharacteristic, measuredValue);
        }
        catch (ArgumentException ex)
        {
            throw new ImportDataException(
                "Cannot add a measured value to a measurement that already contains " +
                "a measured value for the same characteristic.",
                ex);
        }

        return measuredValue;
    }

    /// <summary>
    /// Adds a new measured value for the given characteristic. Measured values can only be added for
    /// characteristics of the part this measurement belongs to (see <see cref="Part"/>).
    /// </summary>
    /// <returns>The added measured value.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when <paramref name="measuredCharacteristic"/> does not belong to the same part as this measurement
    /// or when the measurement does not belong to a part at all.
    /// </exception>
    /// <exception cref="ImportDataException">
    /// Thrown when a measured value for the given inspection plan characteristic already exists.
    /// </exception>
    public MeasuredValue AddMeasuredValue(InspectionPlanCharacteristic measuredCharacteristic)
    {
        return AddMeasuredValue(measuredCharacteristic, new MeasuredValue());
    }

    /// <summary>
    /// Adds the given measured value for the given characteristic potentially replacing an existing measured value for
    /// the characteristic. Measured values can only be added for characteristics of the part this measurement belongs
    /// to (see <see cref="Part"/>).
    /// </summary>
    /// <param name="measuredCharacteristic">
    /// The measured characteristic.
    /// </param>
    /// <param name="measuredValue">The measured value to add.</param>
    /// <returns>The added measured value.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when <paramref name="measuredCharacteristic"/> does not belong to the same part as this measurement
    /// or when the measurement does not belong to a part at all.
    /// </exception>
    public MeasuredValue SetMeasuredValue(
        InspectionPlanCharacteristic measuredCharacteristic,
        MeasuredValue measuredValue)
    {
        if (!ReferenceEquals(measuredCharacteristic.GetParentPart(), Part))
        {
            throw new ImportDataException(
                "Cannot add a measured value to a measurement for a characteristic that belongs to " +
                "a different part than the measurement.");
        }

        _ValueMap[measuredCharacteristic] = measuredValue;

        return measuredValue;
    }

    /// <summary>
    /// Adds a new measured value for the given characteristic potentially replacing an existing measured value for
    /// the characteristic. Measured values can only be added for characteristics of the part this measurement belongs
    /// to (see <see cref="Part"/>).
    /// </summary>
    /// <param name="measuredCharacteristic">
    /// The measured characteristic.
    /// </param>
    /// <returns>The added measured value.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when <paramref name="measuredCharacteristic"/> does not belong to the same part as this measurement
    /// or when the measurement does not belong to a part at all.
    /// </exception>
    public MeasuredValue SetMeasuredValue(InspectionPlanCharacteristic measuredCharacteristic)
    {
        return SetMeasuredValue(measuredCharacteristic, new MeasuredValue());
    }

    /// <summary>
    /// Gets the measured value for the given characteristic if it exists. If a measured value for the given characteristic does
    /// not yet exist, a new measured value is added beforehand.
    /// </summary>
    /// <param name="measuredCharacteristic">The characteristic for which to get or set a measured value.</param>
    /// <returns>The existing or newly added measured value.</returns>
    public MeasuredValue GetOrSetMeasuredValue(InspectionPlanCharacteristic measuredCharacteristic)
    {
        return TryGetMeasuredValue(measuredCharacteristic, out var existingMeasuredValue)
            ? existingMeasuredValue
            : SetMeasuredValue(measuredCharacteristic);
    }

    /// <summary>
    /// Removes the measured value for the given characteristic from this measurement.
    /// </summary>
    /// <param name="measuredCharacteristic">The characteristic to remove the measured value for.</param>
    /// <returns>True if the measured value was removed; otherwise, false.</returns>
    public bool RemoveMeasuredValue(InspectionPlanCharacteristic measuredCharacteristic)
    {
        return _ValueMap.Remove(measuredCharacteristic);
    }

    /// <summary>
    /// Clears all measured values from this measurement.
    /// </summary>
    public void ClearMeasuredValues()
    {
        _ValueMap.Clear();
    }

    /// <summary>
    /// Gets all measured values of this measurement.
    /// </summary>
    /// <returns>
    /// The list of measured value pairs of this measurement. Each key value pair is keyed by the measured inspection plan characteristic.
    /// </returns>
    public IEnumerable<KeyValuePair<InspectionPlanCharacteristic, MeasuredValue>> EnumerateMeasuredValues()
    {
        return _ValueMap;
    }

    /// <summary>
    /// Notifies this measurement about a change of the part it belongs to.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    /// <param name="newPart">The new part this measurement belongs to.</param>
    internal void NotifyPartChanged(InspectionPlanPart? newPart)
    {
        if (ReferenceEquals(newPart, Part))
            return;

        // Was the measurement actually removed?
        if (newPart is null && Part?.ContainsMeasurement(this) == true)
            return;

        // Was the measurement actually moved to the new part?
        if (newPart is not null && !newPart.ContainsMeasurement(this))
            return;

        // Since we changed the part, none of the measured values can still belong to a characteristic of this part,
        // so we must clear all of them.

        Part = newPart;
        _ValueMap.Clear();
    }

    /// <summary>
    /// Notifies this measurement about the removal of a child characteristic from the part it belongs to.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    internal void NotifyCharacteristicRemoved(InspectionPlanCharacteristic removedCharacteristic)
    {
        if (ReferenceEquals(removedCharacteristic.GetParentPart(), Part))
            return;

        RemoveMeasuredValues(removedCharacteristic);
    }

    #endregion
}