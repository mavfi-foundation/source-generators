// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Scriban;

namespace MavFiFoundation.SourceGenerators.Builders;

/// <summary>
/// Represents a builder class for parsing Scriban templates within the MavFi Foundation source generators.
/// Inherits from <see cref="MFFScribanBuilderBase"/> and provides a default name and parsing logic.
/// </summary>
public class MFFScribanBuilder : MFFScribanBuilderBase
{
    /// <summary>
    /// Default name used to identify the builder plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFScribanBuilder);

    public MFFScribanBuilder() : base(DefaultName) { }

    /// <inheritdoc/>
    protected override Template Parse(string template)
    {
        return Template.Parse(template);
    }
}
