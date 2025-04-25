using MavFiFoundation.SourceGenerators.TestSupport;
using Scriban;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class TemplateTests
{
    private static void ParseAndCheckTemplate(string templateText)
    {
        // Act
        var templateProcessor = Template.Parse(templateText);

        // Assert
        templateProcessor.HasErrors.Should().BeFalse($"templates should parse without errors.\nThe following error(s) occurred parsing template:\n{templateProcessor.Messages.ToString()}");
    }


    [Theory] 
    [InlineData(Constants.SourceFiles.AdditionalFile.TEST_TEMPLATE)]  
    public static async Task Parse_EmbeddedTestTemplate_ReturnsWithHasErrorsFalse(string templateName)
    {
        // Arrange
        var templateText = await EmbeddedResourceHelper.ReadEmbeddedSourceAsync(
            templateName,
            EmbeddedResourceHelper.EmbeddedResourceType.AdditionalFiles);

        ParseAndCheckTemplate(templateText);
    }

    [Theory] 
    [InlineData(MFFGeneratorConstants.Generator.CREATE_GENERATOR_CONSTANTS_TEMPLATE_NAME)]  
    public static async Task Parse_EmbeddedGeneratorTemplate_ReturnsWithHasErrorsFalse(string templateName)
    {
        // Arrange
        var templateText = 	await EmbeddedResourceHelper.ReadEmbeddedSourceAsync(
            $"Templates.{templateName}",
            typeof(MFFGeneratorBase).Assembly);

        ParseAndCheckTemplate(templateText);
    }
}
