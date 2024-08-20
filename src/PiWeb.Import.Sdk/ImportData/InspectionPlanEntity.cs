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
using System.Text;

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents an inspection plan entity (either a part or characteristic) to be imported.
/// </summary>
public abstract class InspectionPlanEntity(Guid uuid) : Entity(uuid)
{
    #region properties

    /// <summary>
    /// The name of this entity.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// The type of this inspection plan entity.
    /// </summary>
    public abstract InspectionPlanEntityType InspectionPlanEntityType { get; }

    /// <summary>
    /// The parent entity of this entity. If this property is <c>null</c>, this entity is a root element.
    /// </summary>
    public abstract InspectionPlanEntity? Parent { get; }

    /// <summary>
    /// The number of parts. This only includes direct children of this inspection plan entity.
    /// </summary>
    public abstract int PartCount { get; }

    /// <summary>
    /// The number of characteristics. This only includes direct children of this inspection plan entity.
    /// </summary>
    public abstract int CharacteristicCount { get; }

    /// <summary>
    /// The number of inspection plan entities. This only includes direct children of this inspection plan entity.
    /// </summary>
    public abstract int EntityCount { get; }

    #endregion

    #region methods

    /// <summary>
    /// Renames this inspection plan entity.
    /// </summary>
    /// <param name="newName">The new name.</param>
    public abstract void Rename(string newName);

    /// <summary>
    /// Determines whether this entity contains a part with the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>True if this entity contains a part with the given name; otherwise, false.</returns>
    public abstract bool ContainsPart(string name);

    /// <summary>
    /// Determines whether this entity contains a characteristic of the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>True if this entity contains a characteristic of the given name; otherwise, false.</returns>
    public abstract bool ContainsCharacteristic(string name);

    /// <summary>
    /// Determines whether this entity contains an inspection plan entity (either a characteristic or a part) of the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>True if this entity contains an inspection plan entity of the given name; otherwise, false.</returns>
    public abstract bool ContainsEntity(string name);

    /// <summary>
    /// Tries to get the part of the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="resultPart">Receives the part of the given name if it exists.</param>
    /// <returns>True if a part with the given name exists; otherwise, false.</returns>
    public abstract bool TryGetPart(
        string name,
        [MaybeNullWhen(false)] out InspectionPlanPart resultPart);

    /// <summary>
    /// Tries to get the characteristic of the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="resultCharacteristic">Receives the characteristic of the given name if it exists.</param>
    /// <returns>True if a characteristic with the given name exists; otherwise, false.</returns>
    public abstract bool TryGetCharacteristic(string name,
        [MaybeNullWhen(false)] out InspectionPlanCharacteristic resultCharacteristic);

    /// <summary>
    /// Tries to get the entity of the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="resultEntity">Receives the inspection plan entity of the given name if it exists.</param>
    /// <returns>True if an inspection plan entity with the given name exists; otherwise, false.</returns>
    public abstract bool TryGetEntity(string name, [MaybeNullWhen(false)] out InspectionPlanEntity resultEntity);

    /// <summary>
    /// Gets the characteristic of the given name if it exists. If a characteristic of the given name does not yet exist,
    /// a new characteristic with the given name is added beforehand, potentially replacing an existing part of the same name.
    /// </summary>
    /// <param name="name">The name of the characteristic to get or set.</param>
    /// <param name="replacedPart">Receives a replaced part or null when no part was replaced.</param>
    /// <returns>The existing or newly added characteristic.</returns>
    public abstract InspectionPlanCharacteristic GetOrSetCharacteristic(string name,
        out InspectionPlanPart? replacedPart);

    /// <summary>
    /// Gets all parts.
    /// </summary>
    /// <returns>The parts.</returns>
    public abstract IEnumerable<InspectionPlanPart> EnumerateParts();

    /// <summary>
    /// Gets all characteristics.
    /// </summary>
    /// <returns>The characteristics.</returns>
    public abstract IEnumerable<InspectionPlanCharacteristic> EnumerateCharacteristics();

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>The entities.</returns>
    public abstract IEnumerable<InspectionPlanEntity> EnumerateEntities();

    /// <summary>
    /// Adds the given characteristic.
    /// </summary>
    /// <param name="characteristic">The characteristic to add.</param>
    /// <returns>The added characteristic.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when a characteristic or part of the given name already exists.
    /// </exception>
    /// <exception cref="ImportDataException">
    /// Thrown when the characteristic to add is already an ancestor.
    /// </exception>
    public abstract InspectionPlanCharacteristic AddCharacteristic(InspectionPlanCharacteristic characteristic);

    /// <summary>
    /// Adds a new characteristic with the given name.
    /// </summary>
    /// <param name="name">The name of the new characteristic to add.</param>
    /// <returns>The added characteristic.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    /// <exception cref="ImportDataException">Thrown when a characteristic or part of the given name already exists.</exception>
    public abstract InspectionPlanCharacteristic AddCharacteristic(string name);

    /// <summary>
    /// Adds the given characteristic potentially replacing an existing inspection plan entity of the same name.
    /// </summary>
    /// <param name="characteristic">The characteristic to add.</param>
    /// <param name="replacedEntity">
    /// Receives the replaced inspection plan entity or null when no inspection plan entity was replaced. A characteristic is never
    /// replaced by itself when it already exists and null is received in such a case.
    /// </param>
    /// <returns>The added characteristic.</returns>
    /// <exception cref="ImportDataException">
    /// Thrown when the characteristic to add is already an ancestor.
    /// </exception>
    public abstract InspectionPlanCharacteristic SetCharacteristic(
        InspectionPlanCharacteristic characteristic,
        out InspectionPlanEntity? replacedEntity);

    /// <summary>
    /// Adds a new characteristic with the given name potentially replacing an existing inspection plan entity of the same name.
    /// </summary>
    /// <param name="name">The name of the new characteristic to add.</param>
    /// <param name="replacedEntity">
    /// Receives the replaced inspection plan entity or null when no inspection plan entity was replaced.
    /// </param>
    /// <returns>The set characteristic.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public abstract InspectionPlanCharacteristic SetCharacteristic(string name,
        out InspectionPlanEntity? replacedEntity);

    /// <summary>
    /// Removes the given inspection plan entity.
    /// </summary>
    /// <param name="entity">The inspection plan entity to remove.</param>
    /// <returns>True if the entity was removed; otherwise, false.</returns>
    public abstract bool RemoveEntity(InspectionPlanEntity entity);

    /// <summary>
    /// Removes the inspection plan entity of the given name.
    /// </summary>
    /// <param name="entityName">The name of the inspection plan entity to remove.</param>
    /// <returns>True if the entity was removed; otherwise, false.</returns>
    public abstract bool RemoveEntity(string entityName);

    /// <summary>
    /// Gets the nearest ancestor inspection plan entity that is a part.
    /// </summary>
    /// <returns>The parent part or null when there is no such part.</returns>
    public abstract InspectionPlanPart? GetParentPart();

    /// <summary>
    /// Returns a representation of the inspection plan represented by this inspection plan entity in a readable
    /// text form.
    /// </summary>
    /// <param name="builder">The builder to print to.</param>
    /// <param name="level">The indentation level of the root elements.</param>
    /// <param name="indentation">The number of spaces applied as indentation for each indentation level.</param>
    /// <returns>A representation of the inspection plan in readable text form.</returns>
    public abstract void PrintTree(StringBuilder builder, int level = 0, int indentation = 4);

    /// <summary>
    /// Notifies this inspection plan entity that it was moved to a different parent inspection plan entity.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    internal abstract void NotifyParentChanged(InspectionPlanEntity? newParentEntity);

    /// <summary>
    /// Notifies this inspection plan entity about a name change of one of its children.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    /// <param name="renamedEntity">The renamed child inspection plan entity.</param>
    /// <param name="previousName">The previous name of the entity.</param>
    internal abstract void NotifyChildRenamed(InspectionPlanEntity renamedEntity, string previousName);

    /// <summary>
    /// Notifies this inspection plan entity about a removed child inspection plan entity.
    /// Should not be called explicitly by a plugin implementation but is used internally to ensure integrity of
    /// the data structure.
    /// </summary>
    /// <param name="removedEntity">The removed inspection plan entity.</param>
    internal abstract void NotifyChildRemoved(InspectionPlanEntity removedEntity);

    #endregion
}