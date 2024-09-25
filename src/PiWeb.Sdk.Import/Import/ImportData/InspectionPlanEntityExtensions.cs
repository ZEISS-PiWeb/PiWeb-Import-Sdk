#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// A collection of extension methods for <see cref="InspectionPlanEntity"/>
/// </summary>
public static class InspectionPlanEntityExtensions
{
    #region methods

    /// <summary>
    /// Determines whether the given entity and this entity depend on the same part for storing measurements or
    /// measurement values. The measurement root for a part is always the part itself. If this entity or the
    /// given entity is a characteristic without ancestor part, this check will always be false.
    /// </summary>
    /// <param name="thisEntity">The base entity.</param>
    /// <param name="otherEntity">The entity to check.</param>
    /// <param name="measurementRoot">Receives the measurement root of this entity if it exists; otherwise, null.</param>
    /// <returns>
    /// True if the same part is used as measurement root by the given entity and this entity; otherwise, false.
    /// </returns>
    public static bool HasSameMeasurementRootAs(
        this InspectionPlanEntity? thisEntity,
        InspectionPlanEntity? otherEntity,
        [NotNullWhen(true)] out InspectionPlanPart? measurementRoot)
    {
        measurementRoot = thisEntity as InspectionPlanPart ?? thisEntity?.GetParentPart();
        if (measurementRoot is null)
            return false;

        var otherPart = otherEntity as InspectionPlanPart ?? otherEntity?.GetParentPart();

        return ReferenceEquals(measurementRoot, otherPart);
    }

    /// <summary>
    /// Returns a representation of the inspection plan represented by this inspection plan entity in a readable
    /// text form.
    /// </summary>
    /// <returns>representation of the inspection plan in readable text form.</returns>
    public static string PrintTree(this InspectionPlanEntity entity)
    {
        var stringBuilder = new StringBuilder();
        entity.PrintTree(stringBuilder);
        return stringBuilder.ToString();
    }

    #endregion
}