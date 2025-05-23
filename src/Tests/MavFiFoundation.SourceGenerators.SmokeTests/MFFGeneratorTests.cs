namespace MavFiFoundation.SourceGenerators.SmokeTests;

public class MFFGeneratorTests
{
    [Theory]
    [InlineData("GeneratedByIncludedTypeTest")]  
    [InlineData("GeneratedByJsonAttributeTest")]  
    [InlineData("GeneratedByXmlAttributeTest")]  
    [InlineData("GeneratedByYamlAttributeTest")]  
    public void Types_Are_Generated(string typeName)
    {
        var expected = Type.GetType(typeName);
        expected.Should().NotBeNull();
    }
}