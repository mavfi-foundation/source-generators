
namespace MavFiFoundation.SourceGenerators;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface,
	AllowMultiple = false, Inherited = false)]
public class MFFGenerateSourceAttribute
	: Attribute
{ 
    public bool UseSymbolForLocatorInfo { get; private set; }

    public string SrcLocatorType { get; private set; }

    public string SrcLocatorInfo { get; private set; }

    public string OutputInfo { get; private set; }

    public MFFGenerateSourceAttribute(string srcLocatorType,
                                      string srcLocatorInfo,
                                      string outputInfo,
                                      bool useSymbolForLocatorInfo = false) 
    {
        this.SrcLocatorType = srcLocatorType;
        this.SrcLocatorInfo = srcLocatorInfo;
        this.OutputInfo = outputInfo;
        this.UseSymbolForLocatorInfo = useSymbolForLocatorInfo;
    }

    public MFFGenerateSourceAttribute(string srcLocatorType,
                                     string outputInfo,
                                     bool useSymbolForLocatorInfo = true) 
        : this(srcLocatorType, string.Empty, outputInfo, useSymbolForLocatorInfo)
    {
    }
}