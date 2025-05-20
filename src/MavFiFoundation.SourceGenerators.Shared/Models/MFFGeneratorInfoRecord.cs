// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Used to store generator configuration information.
/// </summary>
/// <param name="ContainingNamespace">The namespace of the triggering type or file.</param>
/// <param name="SrcLocatorType">The name of the source locator type to use for locating source types.</param>
/// <param name="SrcLocatorInfo">Configuration information for the specified source locator.</param>
/// <param name="GenOutputInfos">Builder configuration information used to produce source for 
/// all matched types. This executes once for all matched types.</param>
/// <param name="SrcOutputInfos">Builder configuration information used to produce source for
/// a single matched type. This executes once for each matched type.</param>
public record MFFGeneratorInfoRecord
(
    string? ContainingNamespace,

    string SrcLocatorType,

    object SrcLocatorInfo,

    EquatableArray<MFFBuilderRecord> GenOutputInfos,

    EquatableArray<MFFBuilderRecord> SrcOutputInfos
);
