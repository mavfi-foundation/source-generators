// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// The base class that all <see cref="IMFFGeneratorPlugin"/> implementations SHOULD implement.
/// </summary>
public class MFFGeneratorPluginBase : IMFFGeneratorPlugin
{
    /// <inheritdoc/>
    public string Name { get; private set; }

    /// <summary>
    /// Constructor for <see cref="MFFGeneratorPluginBase"/>
    /// </summary>
    /// <param name="name">The name used to identify the plugin.</param>
    public MFFGeneratorPluginBase(string name)
    {
        Name = name;
    }
}
