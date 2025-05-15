// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Defines the interface to be used by all generator plugins.
/// </summary>
public interface IMFFGeneratorPlugin
{
    /// <summary>
    /// Gets the name used to identity the plugin.
    /// </summary>
    string Name { get; }  
}
