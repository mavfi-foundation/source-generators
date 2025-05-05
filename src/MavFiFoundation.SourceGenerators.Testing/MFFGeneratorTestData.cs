namespace MavFiFoundation.SourceGenerators.Testing;

public class MFFGeneratorTestData
{
        public string Scenario { get; set; } = string.Empty;
        public IEnumerable<string> Sources { get; set; } = new HashSet<string>();
        public IEnumerable<(string, string)> AdditionalFiles { get; set; } = 
            new HashSet<(string, string)>();
        public IEnumerable<(Type, string, string)> GeneratedSources { get; set; } = 
            new HashSet<(Type, string, string)>();

    public override string ToString()
    {
        return $"\"{ Scenario }\"";
    }
}
