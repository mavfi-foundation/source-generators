// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Scriban;

namespace MavFiFoundation.SourceGenerators.Builders;

/// <summary>
/// Provides a builder for parsing and handling Liquid templates using the Scriban library.
/// Inherits from <see cref="MFFScribanBuilderBase"/> and overrides the template parsing logic to use Liquid syntax.
/// </summary>
public class MFFLiquidBuilder : MFFScribanBuilderBase
{
    /// <summary>
    /// Default name used to identify the builder plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFLiquidBuilder);

    public MFFLiquidBuilder() : base(DefaultName) { }

    /// <inheritdoc/>
    protected override Template Parse(string template)
    {
        return Template.ParseLiquid(template);
    }
}
