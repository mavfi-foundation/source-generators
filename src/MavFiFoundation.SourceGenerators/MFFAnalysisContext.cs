using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;
/// <summary>
/// Context for analyzer validation.
/// </summary>
/// <param name="Compilation"><see cref="Compilation"/> that is the subject of the analysis.</param>
/// <param name="CancellationToken">Token to check for requested cancellation of the analysis.</param>
public record MFFAnalysisContext(
    Compilation Compilation,
    CancellationToken CancellationToken
);
