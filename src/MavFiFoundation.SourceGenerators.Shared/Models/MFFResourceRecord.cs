// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a resource record with a name and associated text.
/// </summary>
/// <param name="Name">The name, including the path, of the resource.</param>
/// <param name="Text">The text content of the resource.</param>
public record MFFResourceRecord(string Name, string Text);
