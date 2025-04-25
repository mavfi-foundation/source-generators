using Scriban;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Builders;

public abstract class MFFScribanBuilderBase : MFFGeneratorPluginBase, IMFFBuilder
{
   #region Constructors

    public MFFScribanBuilderBase(string name) : base(name) { }
    
    #endregion

    #region IMFFBuilder Implementation

    public string Build(object templateInfo,
                        MFFBuilderRecord builderRec, 
                        IEnumerable<MFFTypeSymbolRecord> srcTypes)
    {
        var template = templateInfo as string;

        if (template is null)
        {
            throw new InvalidCastException($"{nameof(templateInfo)} must be a string.");
        }
        
        var templateProcessor = Parse(template);
        return Render(templateProcessor, builderRec, srcTypes);

    }

    public string Build(object templateInfo,
                        MFFBuilderRecord builderRec, 
                        MFFTypeSymbolRecord srcType)
    {
        var template = templateInfo as string;

        if (template is null)
        {
            throw new InvalidCastException($"{nameof(templateInfo)} must be a string.");
        }
        
        var templateProcessor = Parse(template);
        return Render(templateProcessor, builderRec, srcType);
    }

    #endregion

    #region Private/Protected Methods

    protected abstract Template Parse(string template);

    protected virtual string Render(
        Template templateProcessor, 
        MFFBuilderRecord builderRec, 
        IEnumerable<MFFTypeSymbolRecord> srcTypes)       
    {
        return templateProcessor.Render(
            new 
            {
                outputInfos=builderRec.AdditionalOutputInfos, 
                srcTypes=srcTypes
            }, 
            member => member.Name
        );
    }

    protected virtual string Render(
        Template templateProcessor, 
        MFFBuilderRecord builderRec, 
        MFFTypeSymbolRecord srcType)       
    {
        return templateProcessor.Render(
            new 
            {
                outputInfos=builderRec.AdditionalOutputInfos, 
                srcType=srcType
            }, 
            member => member.Name
        );
    }

    #endregion
}
