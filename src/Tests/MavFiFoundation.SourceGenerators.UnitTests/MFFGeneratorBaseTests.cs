
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Testing.Models;
using MavFiFoundation.SourceGenerators.Testing;

using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Builders;

namespace MavFiFoundation.SourceGenerators.UnitTests;

public class MFFGeneratorBaseTests
{
    private readonly Mock<IMFFGeneratorPluginsProvider> _mockPluginsProvider;
    private readonly Mock<IMFFGeneratorHelper> _mockGeneratorHelper;
    private readonly object _sources;
    private readonly object _diagnostics;
    private readonly CancellationToken _cancellationToken;
    private readonly SourceProductionContext _context;
    
    public MFFGeneratorBaseTests()
    {
        _mockPluginsProvider = new Mock<IMFFGeneratorPluginsProvider>(MockBehavior.Strict);
        _mockGeneratorHelper = new Mock<IMFFGeneratorHelper>(MockBehavior.Strict);
        
        var codeAnalysisAssembly = TestHelpers.GetCodeAnalysisAssembly();
        _sources = TestHelpers.CreateAdditionalSourcesCollection(codeAnalysisAssembly);
        _diagnostics = TestHelpers.CreateDiagnosticBag(codeAnalysisAssembly);
        _cancellationToken = new CancellationToken();
        _context = TestHelpers.CreateContext(
            codeAnalysisAssembly, _sources, _diagnostics, _cancellationToken);
    }

    [Fact]
    public void CreateOutput_NothingIn_NothingOut()
    {
        // Arrange
        var cut = new MFFGeneratorBaseTestClass(_mockPluginsProvider.Object, _mockGeneratorHelper.Object);

        // Act
        cut.ExposedCreateOutput(
            Array.Empty<MFFGeneratorInfoWithSrcTypesRecord?>().ToImmutableArray(),
            _context);

        // Assert
        var addedSourceCount = TestHelpers.GetSourcesCount(_sources);
        addedSourceCount.Should().Be(0);
    }

    [Fact]
    public void CreateOutput_CreatesSourcesForGenOutputInfos()
    {
        // Arrange
        var expectedSourceCode = "GenOutputInfoSourceCode";
        var mockBuilder = new Mock<IMFFBuilder>(MockBehavior.Strict);
        mockBuilder
            .Setup(x => x.Build(
                It.Is<object>(s =>s.ToString() == MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo),
                It.IsAny<MFFBuilderRecord>(),
                It.IsAny<IEnumerable<MFFTypeSymbolRecord>>()))
            .Returns(MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo);

        mockBuilder
            .Setup(x => x.Build(
                It.Is<object>(s =>s.ToString() == MFFBuilderRecordBuilder.DefaultSourceBuilderInfo),
                It.IsAny<MFFBuilderRecord>(),
                It.IsAny<IEnumerable<MFFTypeSymbolRecord>>()))
            .Returns(expectedSourceCode);

        _mockPluginsProvider.SetupGet(x => x.Builders)
            .Returns(new Dictionary<string, IMFFBuilder>()
            {
                { MFFBuilderRecordBuilder.DefaultSourceBuilderType, mockBuilder.Object }
            });

        _mockPluginsProvider.SetupGet(x => x.DefaultFileNameBuilder)
            .Returns(mockBuilder.Object);

        var genAndSrcInfos = new MFFGeneratorInfoWithSrcTypesRecord?[]{
            new MFFGeneratorInfoWithSrcTypesRecordBuilder()
                .GenInfo(new MFFGeneratorInfoRecordBuilder()
                    .AddGenOutputInfo(new MFFBuilderRecordBuilder().Build())
                    .Build())
                .Build()
        }.ToImmutableArray();

        var cut = new MFFGeneratorBaseTestClass(_mockPluginsProvider.Object, _mockGeneratorHelper.Object);

        // Act
        cut.ExposedCreateOutput(genAndSrcInfos, _context);

        // Assert
        var addedSourceCount = TestHelpers.GetSourcesCount(_sources);
        addedSourceCount.Should().Be(1);

        var containsSource = TestHelpers.ContainsSource(
            _sources, 
            MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo,
            expectedSourceCode);

        containsSource.Should().BeTrue();
    }

        [Fact]
    public void CreateOutput_CreatesSourcesForSrcOutputInfos()
    {
        // Arrange
        var expectedSourceCode = "GenOutputInfoSourceCode";
        var mockBuilder = new Mock<IMFFBuilder>(MockBehavior.Strict);
        mockBuilder
            .Setup(x => x.Build(
                It.Is<object>(s =>s.ToString() == MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo),
                It.IsAny<MFFBuilderRecord>(),
                It.IsAny<MFFTypeSymbolRecord>()))
            .Returns(MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo);

        mockBuilder
            .Setup(x => x.Build(
                It.Is<object>(s =>s.ToString() == MFFBuilderRecordBuilder.DefaultSourceBuilderInfo),
                It.IsAny<MFFBuilderRecord>(),
                It.IsAny<MFFTypeSymbolRecord>()))
            .Returns(expectedSourceCode);

        _mockPluginsProvider.SetupGet(x => x.Builders)
            .Returns(new Dictionary<string, IMFFBuilder>()
            {
                { MFFBuilderRecordBuilder.DefaultSourceBuilderType, mockBuilder.Object }
            });

        _mockPluginsProvider.SetupGet(x => x.DefaultFileNameBuilder)
            .Returns(mockBuilder.Object);

        var genAndSrcInfos = new MFFGeneratorInfoWithSrcTypesRecord?[]{
            new MFFGeneratorInfoWithSrcTypesRecordBuilder()
                .GenInfo(new MFFGeneratorInfoRecordBuilder()
                    .AddSrcOutputInfo(new MFFBuilderRecordBuilder().Build())
                    .Build())
                .AddSrcType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build()
        }.ToImmutableArray();

        var cut = new MFFGeneratorBaseTestClass(_mockPluginsProvider.Object, _mockGeneratorHelper.Object);

        // Act
        cut.ExposedCreateOutput(genAndSrcInfos, _context);

        // Assert
        var addedSourceCount = TestHelpers.GetSourcesCount(_sources);
        addedSourceCount.Should().Be(1);

        var containsSource = TestHelpers.ContainsSource(
            _sources, 
            MFFBuilderRecordBuilder.DefaultFileNameBuilderInfo,
            expectedSourceCode);

        containsSource.Should().BeTrue();
    }
}
