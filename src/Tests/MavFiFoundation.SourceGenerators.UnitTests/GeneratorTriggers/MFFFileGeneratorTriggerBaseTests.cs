using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using System.Collections.Immutable;
using Moq.Protected;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFFileGeneratorTriggerBaseTests
{
   private readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFFileGeneratorTriggerBaseTests()
    {
        _mockSerializer = new Mock<IMFFSerializer>(MockBehavior.Strict);
        _mockSerializer.Setup(x => x.DeserializeObject<MFFGeneratorInfoModel>(It.IsAny<String>()))
            .Returns(new MFFGeneratorInfoModel(){
                SrcLocatorType = "LocatorType",
                SrcLocatorInfo = "SourceLocatorInfo"
            });
    }

    [Fact]
    public void GetGeneratorInfoFromFile_SetsContainingNameSpace ()
    {
        //Arrange
        var loaders = Array.Empty<IMFFResourceLoader>();
        var resource = new MFFResourceRecord(Path.Combine("TestSpace","Test.CodeGen.test"), "FileText");
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var cancellationToken = new CancellationToken();
        var cut = new MFFFileGeneratorTriggerBaseTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.Exposed_GetGeneratorInfoFromFile(
            loaders, resource, resources,cancellationToken);

        //Assert
        actual.Should().NotBeNull();
        actual.ContainingNamespace.Should().NotBeNullOrWhiteSpace();
        actual.ContainingNamespace.Should().Be("TestSpace");
    }

    [Fact]
    public void GetGeneratorInfoFromFile_AttemptsToLoadResources ()
    {
        //Arrange
        var loaders = Array.Empty<IMFFResourceLoader>();
        var resource = new MFFResourceRecord(Path.Combine("TestSpace","Test.CodeGen.test"), "FileText");
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var cancellationToken = new CancellationToken();
        var cutMock = new Mock<MFFFileGeneratorTriggerBaseTestClass>(_mockSerializer.Object);
        cutMock.Protected().Setup("LoadResources",
            ItExpr.IsAny<MFFGeneratorInfoModel>(),
            ItExpr.IsAny<ImmutableArray<MFFResourceRecord>>(),
            ItExpr.IsAny<IEnumerable<IMFFResourceLoader>>(),
            ItExpr.IsAny<CancellationToken>()).Verifiable(Times.Once);

        //Act
        cutMock.Object.Exposed_GetGeneratorInfoFromFile(
            loaders, resource, resources,cancellationToken);

        //Assert
        cutMock.VerifyAll();
    }
}
