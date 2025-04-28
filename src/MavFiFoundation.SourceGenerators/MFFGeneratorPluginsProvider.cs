using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.TypeLocators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators;

public class MFFGeneratorPluginsProvider : IMFFGeneratorPluginsProvider
{
    #region IMFFGeneratorPluginsProvider Implementation

    public IDictionary<string, IMFFGeneratorTrigger> GeneratorTriggers { get; private set; }
    public IDictionary<string, IMFFResourceLoader> ResourceLoaders { get; private set; }
    public IDictionary<string, IMFFTypeLocator> TypeLocators { get; private set; }
    public IDictionary<string, IMFFBuilder> Builders { get; private set; }

    public IMFFBuilder? DefaultFileNameBuilder { get; protected set;}

    public IMFFSerializer DefaultSerializer { get; protected set;}

    #endregion

    #region Constructors

    public MFFGeneratorPluginsProvider (bool setDefaultFileNameBuilder2First = true)
    {
        GeneratorTriggers = new Dictionary<string, IMFFGeneratorTrigger>();
        ResourceLoaders = new Dictionary<string, IMFFResourceLoader>();
        TypeLocators = new Dictionary<string, IMFFTypeLocator>();
        Builders = new Dictionary<string, IMFFBuilder>();
        DefaultSerializer = new MFFJsonSerializer();

        AddDefaultGeneratorTriggers();
        AddDefaultResourceLoaders();
        AddDefaultTypeLocators();
        AddDefaultBuilders(setDefaultFileNameBuilder2First);
    }

    #endregion

    #region Private/Protected Methods
    protected void AddDefaultGeneratorTrigger(IMFFGeneratorTrigger locator)
    {
        this.GeneratorTriggers.Add(locator.Name, locator);
    }

    protected virtual void AddDefaultGeneratorTriggers()
    {
        AddDefaultGeneratorTrigger(new MFFAttributeGeneratorTrigger(DefaultSerializer));
        AddDefaultGeneratorTrigger(new MFFXmlGeneratorTrigger(new MFFXmlSerializer()));
        AddDefaultGeneratorTrigger(new MFFJsonGeneratorTrigger(new MFFJsonSerializer()));
        AddDefaultGeneratorTrigger(new MFFYamlGeneratorTrigger(new MFFYamlSerializer()));
    }  

    protected void AddDefaultResourceLoader(IMFFResourceLoader loader)
    {
        this.ResourceLoaders.Add(loader.Name, loader);
    }

    protected virtual void AddDefaultResourceLoaders()
    {
        AddDefaultResourceLoader(new MFFResourceLoader());
    }  

    protected void AddDefaultSourceTypeLocator(IMFFTypeLocator locator)
    {
        this.TypeLocators.Add(locator.Name, locator);
    }

    protected virtual void AddDefaultTypeLocators()
    {
        AddDefaultSourceTypeLocator(new MFFIncludedTypeLocator());
        AddDefaultSourceTypeLocator(new MFFAttributeTypeLocator(DefaultSerializer));
    }  

    protected void AddDefaultBuilder(IMFFBuilder builder)
    {
        this.Builders.Add(builder.Name, builder);
    }

    protected virtual void AddDefaultBuilders(bool setDefaultFileNameBuilder2First)
    {
        var firstBuilder = new MFFScribanBuilder();

        if(setDefaultFileNameBuilder2First)
        {
            DefaultFileNameBuilder = firstBuilder;
        }

        AddDefaultBuilder(firstBuilder);
        AddDefaultBuilder(new MFFLiquidBuilder());
    }  

    #endregion
}
