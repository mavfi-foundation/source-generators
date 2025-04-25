using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.ResourceLoaders;

public class MFFResourceLoader : MFFGeneratorPluginBase, IMFFResourceLoader
{
    public const string DEFAULT_NAME = nameof(MFFResourceLoader);

    public const string DEFAULT_LOADER_PREFIX = nameof(MFFResourceLoader) + ":";

    public string Prefix {get; private set; }

    public MFFResourceLoader() : this(
        DEFAULT_NAME,
        DEFAULT_LOADER_PREFIX
    ) { }

    public MFFResourceLoader(string name, string prefix) : base(name) 
    { 
        Prefix = prefix;
    }

   public bool TryLoadResource(
        ref object? objResourceInfo, 
        ImmutableArray<MFFResourceRecord> allResources,
        CancellationToken cancellationToken)
    {
        if(objResourceInfo is string strResourceInfo)
        {
            if(strResourceInfo.StartsWith(Prefix))
            {
                strResourceInfo = strResourceInfo
                    // Remove the prefix
                    .Substring(Prefix.Length)
                    // Allow / or \ for directory separator in file paths
                    .Replace('\\','/').Trim();

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
