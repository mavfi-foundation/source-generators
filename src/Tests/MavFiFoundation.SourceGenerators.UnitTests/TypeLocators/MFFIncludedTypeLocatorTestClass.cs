using System;

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.TypeLocators;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFIncludedTypeLocatorTestClass : MFFIncludedTypeLocator
{
    public MFFGeneratorInfoWithSrcTypesRecord? ExposedGetIncludedType(MFFGeneratorInfoRecord? genInfo)
    {
        return GetIncludedType(genInfo);
    }
}
