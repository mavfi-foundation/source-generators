// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a collection of type symbol records along with their source identifier
/// for the assembly that contains the source type.
/// </summary>
/// <param name="Source">
/// The source identifier or description associated with the type symbols.
/// </param>
/// <param name="Types">
/// An array of <see cref="MFFTypeSymbolRecord"/> representing the type symbols.
/// </param>
public record class MFFTypeSymbolSources(
    string Source,
    EquatableArray<MFFTypeSymbolRecord> Types
);

