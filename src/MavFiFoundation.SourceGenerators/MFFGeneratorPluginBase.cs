using System;

namespace MavFiFoundation.SourceGenerators;

public class MFFGeneratorPluginBase : IMFFGeneratorPlugin
{
   	public string Name {get; private set; }

    public MFFGeneratorPluginBase(string name)
    {
        Name = name;
    }
}
