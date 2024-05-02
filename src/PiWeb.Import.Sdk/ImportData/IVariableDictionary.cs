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

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents a collection of variables.
/// </summary>
public interface IVariableDictionary
{
    #region properties

    /// <summary>
    /// The number of contained variables.
    /// </summary>
    public int VariableCount { get; }

    #endregion

    #region methods

    /// <summary>
    /// Determines whether this entity contains a variable with the given name.
    /// </summary>
    /// <param name="variableName">The name of the variable to look for.</param>
    /// <returns>True if this entity contains a variable with the given name; otherwise, false.</returns>
    public bool ContainsVariable(string variableName);

    /// <summary>
    /// Gets the variable value for the given variable name if it exists.
    /// </summary>
    /// <param name="variableName">The name of the variable to get the value for.</param>
    /// <param name="variableValue">Receives the variable value if it exists; otherwise, the null variable value.</param>
    /// <returns>True if the variable value exists; otherwise, false.</returns>
    public bool TryGetVariableValue(string variableName, out VariableValue variableValue);

    /// <summary>
    /// Returns all contained variables.
    /// </summary>
    /// <returns>The contained variables.</returns>
    public IEnumerable<KeyValuePair<string, VariableValue>> EnumerateVariables();

    /// <summary>
    /// Removes the variable with the given name.
    /// </summary>
    /// <param name="variableName">The name of the variable to remove.</param>
    /// <returns>True if the variable was removed; otherwise, false.</returns>
    public bool RemoveVariable(string variableName);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="variableValue">The variable value to set.</param>
    public void SetVariable(string variableName, VariableValue variableValue);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="intValue">The variable value to set.</param>
    public void SetVariable(string variableName, int intValue);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="doubleValue">The variable value to set.</param>
    public void SetVariable(string variableName, double doubleValue);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="stringValue">The variable value to set.</param>
    public void SetVariable(string variableName, string stringValue);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="dateTimeValue">The variable value to set.</param>
    public void SetVariable(string variableName, DateTime dateTimeValue);

    /// <summary>
    /// Sets the given variable value for the given variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set the value for.</param>
    /// <param name="catalogEntry">The variable value to set.</param>
    public void SetVariable(string variableName, CatalogEntry catalogEntry);

    #endregion
}