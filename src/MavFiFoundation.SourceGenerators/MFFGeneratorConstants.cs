// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Contains constant values used by the MavFi Foundation source generators.
/// </summary>
public static class MFFGeneratorConstants
{
    /// <summary>
    /// Contains constant values related to the generator configuration and attributes.
    /// </summary>
    public static class Generator 
    {
        /// <summary>
        /// The name representing the compiling project.
        /// </summary>
        public const string CompilingProject = "Self";

        /// <summary>
        /// The fully qualified name of the <c>MFFEmbeddedResourceAttribute</c>.
        /// </summary>
        public const string EmbeddedResourceAttributeName = 
            "MavFiFoundation.SourceGenerators.MFFEmbeddedResourceAttribute";

        /// <summary>
        /// The fully qualified name of the <c>MFFCreateGeneratorConstantsAttribute</c>.
        /// </summary>
        public const string CreateGeneratorConstantsAttributeName = 
            "MavFiFoundation.SourceGenerators.MFFCreateGeneratorConstantsAttribute";

        /// <summary>
        /// The output file name for generated generator constants.
        /// </summary>
        public const string CreateGeneratorConstantsOutputName = 
            "MFFGeneratorConstants.g.cs";

        /// <summary>
        /// The template file name used for generating generator constants.
        /// </summary>
        public const string CreateGeneratorConstantsTemplateName = 
            "MFFGeneratorConstants.scriban-cs";
    }
}
