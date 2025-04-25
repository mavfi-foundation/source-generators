using System;

namespace MavFiFoundation.SourceGenerators.ResourceLoaders;

public class ResourceNotFoundException : InvalidOperationException
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
