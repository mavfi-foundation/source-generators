// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents generator information along with a collection of source type symbols.
/// </summary>
/// <param name="GenInfo">The generator configuration information record.</param>
/// <param name="SrcTypes">An array of source type symbols associated with the generator, wrapped in an <see cref="EquatableArray{T}"/>.</param>
public record class MFFGeneratorInfoWithSrcTypesRecord(
    MFFGeneratorInfoRecord GenInfo,
    EquatableArray<MFFTypeSymbolRecord> SrcTypes
);
