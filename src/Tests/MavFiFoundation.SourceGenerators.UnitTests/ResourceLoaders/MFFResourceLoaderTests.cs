using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Testing.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.ResourceLoaders;
public class MFFResourceLoaderTests
{

    #region TryLoadResource Tests

    [Fact]
    public void TryLoadResource_NothingIn_NothingOut ()
    {
        //Arrange
        var allResources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        object? resourceInfo = null;
        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var resLoaded = cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        resLoaded.Should().BeFalse();
        resourceInfo.Should().BeNull();
    }

    [Fact]
    public void TryLoadResource_ValidFilePathWithBackSlashes_ReturnsTrueAndResourceIsUpdated ()
    {
        //Arrange
        var resourceText = "Test Text";
        var resourceName = "Test\\File.cs";
        var resourcePath = "C:/Test/File.cs";
        object? resourceInfo = $"{ MFFResourceLoader.DefaultLoaderPrefix }{resourceName}";

        var allResources = new []{
            new MFFResourceRecordBuilder()
                .Name(resourcePath)
                .Text(resourceText)
                .Build()
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var resLoaded = cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        resLoaded.Should().BeTrue();
        resourceInfo.Should().Be(resourceText);
    }

   [Fact]
    public void TryLoadResource_ValidFilePathWithForwardSlashes_ReturnsTrueAndResourceIsUpdated ()
    {
        //Arrange
        var resourceText = "Test Text";
        var resourceName = "Test/File.cs";
        var resourcePath = "C:/Test/File.cs";
        object? resourceInfo = $"{ MFFResourceLoader.DefaultLoaderPrefix }{resourceName}";

        var allResources = new []{
            new MFFResourceRecordBuilder()
                .Name(resourcePath)
                .Text(resourceText)
                .Build()
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var resLoaded = cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        resLoaded.Should().BeTrue();
        resourceInfo.Should().Be(resourceText);
    }

   [Fact]
    public void TryLoadResource_UsesCaseSensitiveMatch ()
    {
        //Arrange
        var resourceText = "Test Text";
        var resourceName = "test/file.cs";
        var resourcePath = "C:/Test/File.cs";
        object? resourceInfo = $"{ MFFResourceLoader.DefaultLoaderPrefix }{resourceName}";

        var allResources = new []{
            new MFFResourceRecordBuilder()
                .Name(resourcePath)
                .Text(resourceText)
                .Build()
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var act = () => cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        act.Should().ThrowExactly<ResourceNotFoundException>();
    }

    [Fact]
    public void TryLoadResource_ValidClassName_ReturnsTrueAndResourceIsUpdated ()
    {
        //Arrange
        var resourceText = "Test Text";
        var resourceName = "TestNameSpace.TestClass";
        var resourcePath = "ProjectNamespace.TestNameSpace.TestClass";
        object? resourceInfo = $"{ MFFResourceLoader.DefaultLoaderPrefix }{resourceName}";
 
        var allResources = new []{
            new MFFResourceRecordBuilder()
                .Name(resourcePath)
                .Text(resourceText)
                .Build()
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var resLoaded = cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        resLoaded.Should().BeTrue();
        resourceInfo.Should().Be(resourceText);
    }

    [Fact]
    public void TryLoadResource_NonLoaderResourceInfoValue_ReturnsFalseAndResourceInfoIsNotChanged ()
    {
        //Arrange
        var resourceText = "Test Text";
        var resourcePath = "ProjectNamespace.TestNameSpace.TestClass";
        object? resourceInfo = "Some Info";
        var origResourceInfo = resourceInfo;

        var allResources = new []{
            new MFFResourceRecordBuilder()
                .Name(resourcePath)
                .Text(resourceText)
                .Build()
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act
        var resLoaded = cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        resLoaded.Should().BeFalse();
        resourceInfo.Should().Be(origResourceInfo);
    }

    [Fact]
    public void TryLoadResource_UnmatchedResourceWithResourceLoaderPrefix_ThrowsResourceNotFoundException ()
    {
        //Arrange
        object? resourceInfo = $"{ MFFResourceLoader.DefaultLoaderPrefix }Some.Class.Name";
        var origResourceInfo = resourceInfo;

        var allResources = Array.Empty<MFFResourceRecord>().ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFResourceLoader();

        // Act  
        Action act = () => cut.TryLoadResource(ref resourceInfo, allResources, cancellationToken);

        //Assert
        act.Should().ThrowExactly<ResourceNotFoundException>().WithMessage("Resource.Name: 'Some.Class.Name'");
    }

    #endregion
}