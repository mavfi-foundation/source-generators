using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Represents a code action for the MavFi Foundation source generators,
/// encapsulating the title, the updated syntax node, and an equivalence key
/// for identifying similar actions.
/// </summary>
/// <param name="Title">The title of the code action, typically displayed in the IDE.</param>
/// <param name="UpdatedNode">The <see cref="SyntaxNode"/> representing the updated syntax node after applying the action.</param>
/// <param name="EquivalenceKey">A key used to identify equivalence between code actions.</param>
public record class MFFCodeAction(
    string Title,
    SyntaxNode UpdatedNode,
    string? EquivalenceKey
);
