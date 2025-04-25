using Xunit.Abstractions;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorTestData: IXunitSerializable
{
        public string Scenario { get; set; } = string.Empty;
        public IEnumerable<string> Sources { get; set; } = new HashSet<string>();
        public IEnumerable<(string, string)> AdditionalFiles { get; set; } = 
            new HashSet<(string, string)>();
        public IEnumerable<(Type, string, string)> GeneratedSources { get; set; } = 
            new HashSet<(Type, string, string)>();

    public void Deserialize(IXunitSerializationInfo info)
    {
        Scenario = info.GetValue<string>(nameof(Scenario));
        Sources = info.GetValue<IEnumerable<string>>(nameof(Sources));
        AdditionalFiles = info.GetValue<object[][]>(nameof(AdditionalFiles))
            .Select(af => (af[0].ToString() ?? string.Empty, af[1].ToString() ?? string.Empty)).ToArray();
        GeneratedSources = info.GetValue<object[][]>(nameof(GeneratedSources))
            .Select(gs => ((Type)gs[0], gs[1].ToString() ?? string.Empty, gs[2].ToString() ?? string.Empty)).ToArray();
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Scenario), Scenario);
        info.AddValue(nameof(Sources), Sources);
        info.AddValue(nameof(AdditionalFiles), AdditionalFiles
            .Select(af => new object[] {af.Item1, af.Item2}).ToArray());
        info.AddValue(nameof(GeneratedSources), GeneratedSources
            .Select(gs => new object[] {gs.Item1, gs.Item2, gs.Item3}).ToArray());
    }

    public override string ToString()
    {
        return $"\"{ Scenario }\"";
    }
}
