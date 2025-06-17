// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.ResourceLoaders;

/// <summary>
/// Provides functionality to load resources by matching resource names with a specified prefix.
/// </summary>
/// <remarks>
/// This loader is intended to be used as a plugin for resource loading in the MavFi Foundation source generators.
/// It matches resource names that start with a specific prefix and retrieves their content.
/// </remarks>
public class MFFResourceLoader : MFFGeneratorPluginBase, IMFFResourceLoader
{
    /// <summary>
    /// Default name used to identify the resource loader plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFResourceLoader);

    /// <summary>
    /// Default loader prefix used to determine a resource should be loaded.
    /// </summary>
    public const string DefaultLoaderPrefix = nameof(MFFResourceLoader) + ":";

    /// <summary>
    /// Current loader prefix used to determine a resource should be loaded.
    /// </summary>
    public string Prefix { get; private set; }

    public MFFResourceLoader() : this(
        DefaultName,
        DefaultLoaderPrefix
    ) { }

    public MFFResourceLoader(string name, string prefix) : base(name) 
    { 
        Prefix = prefix;
    }

    /// Attempts to load a resource based on the provided <paramref name="objResourceInfo"/>.
    /// If <paramref name="objResourceInfo"/> is a string that starts with the expected prefix,
    /// it will search for a matching resource in <paramref name="allResources"/>.
    /// If found, replaces <paramref name="objResourceInfo"/> with the resource's text and returns <c>true</c>.
    /// Throws <see cref="ResourceNotFoundException"/> if the resource is not found.
    /// </summary>
    /// <param name="objResourceInfo">
    /// A reference to an object containing resource information. If it is a string with the correct prefix,
    /// it will be replaced with the loaded resource's text upon success.
    /// </param>
    /// <param name="allResources">
    /// An immutable array of <see cref="MFFResourceRecord"/> representing all available resources.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// <c>true</c> if the resource was successfully loaded and <paramref name="objResourceInfo"/> was updated; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ResourceNotFoundException">
    /// Thrown if the resource specified by <paramref name="objResourceInfo"/> is not found in <paramref name="allResources"/>.
    /// </exception>
    public bool TryLoadResource(
         ref object? objResourceInfo,
         ImmutableArray<MFFResourceRecord> allResources,
         CancellationToken cancellationToken)
    {
        if (objResourceInfo is string strResourceInfo)
        {
            if (strResourceInfo.StartsWith(Prefix))
            {
                strResourceInfo = strResourceInfo
                    // Remove the prefix
                    .Substring(Prefix.Length)
                    // Allow / or \ for directory separator in file paths
                    .Replace('\\', '/').Trim();

                var res = allResources.FirstOrDefault(r => r.Name.EndsWith(strResourceInfo));

                if (res is not null)
                {
                    objResourceInfo = res.Text;
                    return true;
                }
                else
                {
                    throw new ResourceNotFoundException($"Resource.Name: '{strResourceInfo}'");
                }
            }
        }

        return false;
    }

 }
