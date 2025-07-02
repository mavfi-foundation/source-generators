// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Specifies that the decorated type (class, struct, or interface) should trigger source code generated, 
/// and provides metadata for the source generation process.
/// </summary>
/// <remarks>
/// This attribute is intended for use with source generators to provide information used to populate <see cref="Models.MFFGeneratorInfoModel"/>
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface,
	AllowMultiple = false, Inherited = false)]
public class MFFGenerateSourceAttribute
	: Attribute
{ 
    public bool UseSymbolForLocatorInfo { get; private set; }

    /// <inheritdoc cref="Models.MFFGeneratorInfoModel.SrcLocatorType"/>
    public string SrcLocatorType { get; private set; }

    /// <inheritdoc cref="Models.MFFGeneratorInfoModel.SrcLocatorInfo"/>
    public string? SrcLocatorInfo { get; private set; }

    /// <inheritdoc cref="Models.MFFGeneratorInfoModel.SrcOutputInfos"/>
    public string? SrcOutputInfo { get; private set; }

    /// <inheritdoc cref="Models.MFFGeneratorInfoModel.GenOutputInfos"/>
    public string? GenOutputInfo { get; private set; }

    /// <param name="srcLocatorType">
    /// This is a required parameter. Populates <see cref="Models.MFFGeneratorInfoModel.SrcLocatorType"/> property.
    /// </param>
    /// <param name="srcLocatorInfo">
    /// Optional. Populates <see cref="Models.MFFGeneratorInfoModel.SrcLocatorInfo"/> property.
    /// </param>
    /// <param name="srcOutputInfo">
    /// Optional. Populates <see cref="Models.MFFGeneratorInfoModel.SrcOutputInfos"/> property.
    /// </param>
    /// <param name="genOutputInfo">
    /// Optional. Populates <see cref="Models.MFFGeneratorInfoModel.GenOutputInfos"/> property.
    /// </param>
    /// <param name="useSymbolForLocatorInfo">
    /// Optional. Indicates whether to use symbol information for the locator. If true, <see cref="UseSymbolForLocatorInfo"/> is set to true.
    /// If false and <paramref name="srcLocatorInfo"/> is null, <see cref="UseSymbolForLocatorInfo"/> is also set to true.
    /// </param>
    public MFFGenerateSourceAttribute(string srcLocatorType,
                                      string? srcLocatorInfo = null,
                                      string? srcOutputInfo = null,
                                      string? genOutputInfo = null,
                                      bool useSymbolForLocatorInfo = false)
    {
        this.SrcLocatorType = srcLocatorType;
        this.SrcLocatorInfo = srcLocatorInfo;
        this.SrcOutputInfo = srcOutputInfo;
        this.GenOutputInfo = genOutputInfo;

        if (useSymbolForLocatorInfo)
        {
            this.UseSymbolForLocatorInfo = true;
        }
        else
        {
            if (srcLocatorInfo is null)
            {
                this.UseSymbolForLocatorInfo = true;
            }
        }
    }
}