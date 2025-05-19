# MavFiFoundationSourceGenerators.MFFSourceGenerator

Contains a ready-to-use concrete source generator implementation configured to use all of the plugins available in the [MavFiFoundation.SourceGenerator](https://github.com/mavfi-foundation/source-generators) project.

## Basic functionality

Source generation can be triggered by specifying configuration information using:

- Type Attributes
- Yaml Files
- Json Files
- XML Files

The following template languages can be used to specify generated source code:

- [Scriban](https://github.com/scriban/scriban/blob/master/doc/language.md)
- [Liquid](https://shopify.github.io/liquid/) (via Scriban's [Liquid support](https://github.com/scriban/scriban/blob/master/doc/liquid-support.md))

## Basic usage

Adding the following trigger file would generate an EF Core `DbContext` for all classes in the project that include an attribute named `DbEntity`.

### Trigger file (`ExampleContext.CodeGen.yml`)

```yaml
srcLocatorType: MFFAttributeTypeLocator
srcLocatorInfo: Example.Data.DbEntityAttribute
genOutputInfos:
  - fileNameBuilderInfo: 'ExampleDbContext.g.cs'
    sourceBuilderType: MFFScribanBuilder
    sourceBuilderInfo: |-
      #nullable enable

      using Microsoft.EntityFrameworkCore;

      namespace Example.Data

      public partial class ExampleContext : DbContext
      {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
        {
        }

      {{- for srcType in srcTypes }}
        public DbSet<{{ srcType.Name }}> {{ srcType.Name }}s { get; set; }

      {{- end }}
      }

```

### Generated file (`ExampleDbContext.g.cs`)

```cs
#nullable enable

using Microsoft.EntityFrameworkCore;

namespace Example.Data

public partial class ExampleContext : DbContext
{
    public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
    {

    }

    public DbSet<Example> Examples { get; set; }

    public DbSet<AdditionalExample> AdditionalExamples { get; set; }

}

```
