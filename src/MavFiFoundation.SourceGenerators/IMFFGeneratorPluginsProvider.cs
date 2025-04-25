using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.TypeLocators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

namespace MavFiFoundation.SourceGenerators;

public interface IMFFGeneratorPluginsProvider
{
    IDictionary<string, IMFFGeneratorTrigger> GeneratorTriggers { get; }
    IDictionary<string, IMFFResourceLoader> ResourceLoaders { get; }
    IDictionary<string, IMFFTypeLocator> TypeLocators { get; }
    IDictionary<string, IMFFBuilder> Builders { get; }
    IMFFBuilder? DefaultFileNameBuilder { get; }
}
