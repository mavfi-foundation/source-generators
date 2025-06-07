// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/

using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators.Testing;

// All of this code was grabbed from Refit
// (https://github.com/reactiveui/refit/pull/1216/files)
// based on a suggestion from
// sharwell - https://discord.com/channels/732297728826277939/732297994699014164/910258213532876861
// If the .NET Roslyn testing packages get updated to have something like this in the future
// I'll remove these helpers.
public static partial class CSharpIncrementalSourceGeneratorVerifier<TIncrementalGenerator>
	where TIncrementalGenerator : IIncrementalGenerator, new()
{
#pragma warning disable CA1034 // Nested types should not be visible
    public class Test : CSharpSourceGeneratorTest<EmptySourceGeneratorProvider, DefaultVerifier>
#pragma warning restore CA1034 // Nested types should not be visible
    {
        #region CSharpSourceGeneratorTest Implementation

        /// <inheritdoc/>
        protected override IEnumerable<Type> GetSourceGenerators()
        {
            yield return new TIncrementalGenerator().GetType();
        }

        /// <inheritdoc/>
		protected override ParseOptions CreateParseOptions()
        {
            var parseOptions = (CSharpParseOptions)base.CreateParseOptions();
            return parseOptions.WithLanguageVersion(LanguageVersion.Preview);
        }
        
        #endregion
	}
}