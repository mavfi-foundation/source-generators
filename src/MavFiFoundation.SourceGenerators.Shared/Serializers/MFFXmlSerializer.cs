using System.Xml;
using System.Xml.Serialization;

using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Serializers;

public class MFFXmlSerializer : MFFSerializerBase, IMFFSerializer
{
    public MFFXmlSerializer()
    {
        RegisterSerializerFor<MFFGeneratorInfoModel>(SerializeGeneratorInfoObject);
        RegisterSerializerFor<MFFBuilderModel>(SerializeBuilderObject);

        RegisterDeserializerFor<MFFGeneratorInfoModel>(DeserializeGeneratorInfoObject);
        RegisterDeserializerFor<MFFBuilderModel>(DeserializeGeneratorInfoObject);
    }

    protected List<MFFBuilderModel> DeserializeBuilders(XmlNode buildersNode)
    {
        var builders = new List<MFFBuilderModel>();

        foreach (XmlNode node in buildersNode.ChildNodes)
        {
            builders.Add(DeserializeBuilder(node));
        }

        return builders;
    }

    protected MFFBuilderModel DeserializeBuilder(XmlNode builderNode)
    {
        var builderInfo = new MFFBuilderModel();

        if (builderNode is not null && builderNode.HasChildNodes)
        {
            foreach (XmlNode node in builderNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case nameof(MFFBuilderModel.FileNameBuilderType):
                        builderInfo.FileNameBuilderType = node.InnerText.Trim();
                        break;
                    case nameof(MFFBuilderModel.FileNameBuilderInfo):
                        builderInfo.FileNameBuilderInfo = node.InnerText;
                        break;
                    case nameof(MFFBuilderModel.SourceBuilderType):
                        builderInfo.SourceBuilderType = node.InnerText.Trim();
                        break;
                    case nameof(MFFBuilderModel.SourceBuilderInfo):
                        builderInfo.SourceBuilderInfo = Deserialize(
                            node.Attributes?.GetNamedItem("type")?.Value,
                            node.InnerXml);
                        break;
                    case nameof(MFFBuilderModel.AdditionalOutputInfos):
                        var additionalOutputInfos = new Dictionary<string, object>();

                        foreach (XmlNode addNode in node.ChildNodes)
                        {
                            var key = addNode.Attributes?.GetNamedItem("key")?.InnerText?.Trim();
                            var value = Deserialize(
                                    addNode.Attributes?.GetNamedItem("type")?.Value,
                                    addNode.InnerXml);

                            if(key is not null && value is not null)
                            {
                                additionalOutputInfos.Add(key, value);
                            }
                        }

                        builderInfo.AdditionalOutputInfos = additionalOutputInfos;
                        break;

                }
            }
        }

        return builderInfo;

    }

    protected MFFGeneratorInfoModel DeserializeGeneratorInfo(XmlNode genInfoNode)
    {
        var genInfo = new MFFGeneratorInfoModel();

        if (genInfoNode is not null && genInfoNode.HasChildNodes)
        {
            foreach (XmlNode node in genInfoNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case nameof(MFFGeneratorInfoModel.ContainingNamespace):
                        genInfo.ContainingNamespace = node.InnerText.Trim();
                        break;
                    case nameof(MFFGeneratorInfoModel.SrcLocatorType):
                        genInfo.SrcLocatorType = node.InnerText.Trim();
                        break;
                    case nameof(MFFGeneratorInfoModel.SrcLocatorInfo):
                        genInfo.SrcLocatorInfo = Deserialize(
                            node.Attributes?.GetNamedItem("type")?.Value,
                            node.InnerXml);
                        break;
                    case nameof(MFFGeneratorInfoModel.GenOutputInfos):
                        genInfo.GenOutputInfos = DeserializeBuilders(node);
                        break;
                    case nameof(MFFGeneratorInfoModel.SrcOutputInfos):
                        genInfo.SrcOutputInfos = DeserializeBuilders(node);
                        break;
                }
            }
        }

        return genInfo;
    }

    protected object? DeserializeBuilderObject(string xml)
    {
        return DeserializeBuilder(xml);
    }

    protected MFFBuilderModel DeserializeBuilder(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        var node = doc.DocumentElement!.SelectSingleNode(
            $"/{nameof(MFFBuilderModel)}");

        if (node is null)
        {
            throw new XmlException($"{nameof(MFFBuilderModel)} root node not found.");
        }
        else
        {
            return DeserializeBuilder(node);
        }
    }

    protected object? DeserializeGeneratorInfoObject(string xml)
    {
        return DeserializeGeneratorInfo(xml);
    }

    protected MFFGeneratorInfoModel DeserializeGeneratorInfo(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        var node = doc.DocumentElement!.SelectSingleNode(
            $"/{nameof(MFFGeneratorInfoModel)}");

        if (node is null)
        {
            throw new XmlException($"{nameof(MFFGeneratorInfoModel)} root node not found.");
        }
        else
        {
            return DeserializeGeneratorInfo(node);
        }
    }

    protected string SerializeBuilderObject(object value)
    {
        MFFBuilderModel? builder = value as MFFBuilderModel;

        if(builder is not null)
        {
            SerializeBuilder(builder);
        }

        return string.Empty;
    }

    protected string SerializeBuilder(MFFBuilderModel builder)
    {
        XmlDocument doc = new XmlDocument();

        SerializeBuilder(builder, doc);

        return GetFormattedXml(doc);
    }

    protected void SerializeBuilder(MFFBuilderModel builder, XmlDocument doc, XmlElement? root = null)
    {
        XmlElement body = doc.CreateElement($"{nameof(MFFBuilderModel)}");

        if(root is null)
        {
            doc.AppendChild(body);
        }
        else
        {
            root.AppendChild(body);
        }

        if (builder.FileNameBuilderType is not null)
        {
            body.AppendChild(
                CreateElement(
                    doc,
                    $"{nameof(MFFBuilderModel.FileNameBuilderType)}",
                    builder.FileNameBuilderType)
            );
        }

        if (builder.FileNameBuilderInfo is not null)
        {
            body.AppendChild(
                CreateElement(
                    doc,
                    $"{nameof(MFFBuilderModel.FileNameBuilderInfo)}",
                    builder.FileNameBuilderInfo)
            );
        }

        if (builder.SourceBuilderType is not null)
        {
            body.AppendChild(
                CreateElement(
                    doc,
                    $"{nameof(MFFBuilderModel.SourceBuilderType)}",
                    builder.SourceBuilderType)
            );
        }

        if (builder.SourceBuilderInfo is not null)
        {
            body.AppendChild(
                CreateElementForObject(
                    doc,
                    $"{nameof(MFFBuilderModel.SourceBuilderInfo)}",
                    builder.SourceBuilderInfo)
            );
        }

        if (builder.AdditionalOutputInfos is not null && builder.AdditionalOutputInfos.Any())
        {
            var addBuildersElement = doc.CreateElement($"{nameof(builder.AdditionalOutputInfos)}");
            
            foreach(var addBuilder in builder.AdditionalOutputInfos)
            {
                XmlAttribute keyAttribute = doc.CreateAttribute("key");
                keyAttribute.InnerText = addBuilder.Key;
                var addBuilderElement = CreateElementForObject(doc, "AdditionalOutputInfo", addBuilder.Value);
                addBuilderElement.Attributes.Append(keyAttribute);
                addBuildersElement.AppendChild(addBuilderElement);
            }
            
            body.AppendChild(addBuildersElement);
        }

    }

    protected string SerializeGeneratorInfoObject(object value)
    {
        MFFGeneratorInfoModel? genInfo = value as MFFGeneratorInfoModel;

        if(genInfo is not null)
        {
            return SerializeGeneratorInfo(genInfo);
        }

        return string.Empty;
    }

    protected string SerializeGeneratorInfo(MFFGeneratorInfoModel genInfo)
    {
        XmlDocument doc = new XmlDocument();
        SerializeGeneratorInfo(genInfo, doc);
        return GetFormattedXml(doc);
    }

    protected void SerializeGeneratorInfo(MFFGeneratorInfoModel genInfo, XmlDocument doc, XmlElement? root = null)
    {
        XmlElement body = doc.CreateElement($"{nameof(MFFGeneratorInfoModel)}");

        if(root is null)
        {
            doc.AppendChild(body);
        }
        else
        {
            root.AppendChild(body);
        }

        if (genInfo.ContainingNamespace is not null)
        {
            body.AppendChild(
                CreateElement(
                    doc,
                    $"{nameof(MFFGeneratorInfoModel.ContainingNamespace)}",
                    genInfo.ContainingNamespace)
            );
        }

        if (genInfo.SrcLocatorType is not null)
        {
            body.AppendChild(
                CreateElement(
                    doc,
                    $"{nameof(MFFGeneratorInfoModel.SrcLocatorType)}",
                    genInfo.SrcLocatorType)
            );
        }

        if (genInfo.SrcLocatorInfo is not null)
        {
            body.AppendChild(
                CreateElementForObject(
                    doc,
                    $"{nameof(MFFGeneratorInfoModel.SrcLocatorInfo)}",
                    genInfo.SrcLocatorInfo)
            );
        }

        if (genInfo.GenOutputInfos is not null && genInfo.GenOutputInfos.Any())
        {
            var outputElement = doc.CreateElement($"{nameof(genInfo.GenOutputInfos)}");
            
            foreach(var outputInfo in genInfo.GenOutputInfos)
            {
                SerializeBuilder(outputInfo, doc, outputElement);
            }
            
            body.AppendChild(outputElement);
        }

        if (genInfo.SrcOutputInfos is not null && genInfo.SrcOutputInfos.Any())
        {
            var outputElement = doc.CreateElement($"{nameof(genInfo.SrcOutputInfos)}");
            
            foreach(var outputInfo in genInfo.SrcOutputInfos)
            {
                SerializeBuilder(outputInfo, doc, outputElement);
            }
            
            body.AppendChild(outputElement);

        }

    }

    protected string GetFormattedXml(XmlDocument doc)
    {
        using (StringWriter stringWriter = new StringWriter())
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                doc.WriteTo(xmlWriter);
            }

            return stringWriter.ToString();
        }
    }

    protected string GetFormattedXmlForObject(object obj)
    {
        var serializer = new XmlSerializer(obj.GetType());

        using StringWriter stringWriter = new StringWriter();
        XmlWriterSettings settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true
        };

        using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
        {
            serializer.Serialize(xmlWriter, obj);
        }

        return stringWriter.ToString();
    }

    protected XmlElement CreateElement(XmlDocument doc, string name, string value)
    {
        XmlElement element = doc.CreateElement(name);
        element.InnerXml = value;
        return element;
    }

    protected XmlElement CreateElementForObject(XmlDocument doc, string name, object value)
    {
        XmlAttribute infoTypeAttribute = doc.CreateAttribute("type");
        var valueType = value.GetType();

        infoTypeAttribute.Value = valueType.FullName;
        
        var assemblyName = valueType.Assembly.GetName().Name;
 
        var omitAssemblies = new string[] {
            "mscorelib",
            "mscorlib",
            "System.Private.CoreLib"
        };

        if(!omitAssemblies.Contains(assemblyName))
        {
            infoTypeAttribute.Value += ", " + assemblyName; 
        }

        string valueString; 

        if(value is string)
        {
            valueString = value.ToString()!;
        }
        else
        {
            valueString = GetFormattedXmlForObject(value);
        }

        XmlElement infoElement = CreateElement(doc, name, valueString);

        infoElement.Attributes.Append(infoTypeAttribute);

        return infoElement;
    }

    protected object? Deserialize(string? type2Deserialize, string text)
    {
        if(type2Deserialize is null || 
            string.IsNullOrWhiteSpace(type2Deserialize) ||
            type2Deserialize == "System.String")
        {
            return text;
        }

        var serializer = new XmlSerializer(Type.GetType(type2Deserialize, true)!);
        using StringReader stringReader = new StringReader(text);
        return serializer.Deserialize(stringReader);
    }

    protected override string AbsSerializeObject(object value)
    {
        var serializer = new XmlSerializer(value.GetType());

        using (StringWriter stringWriter = new StringWriter())
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                    Indent = true,
                    IndentChars = "  ",
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(xmlWriter, value);
            }

            return stringWriter.ToString();
        }
    }

    protected override object? AbsDeserializeObject(string value, Type type)
    {
        var serializer = new XmlSerializer(type);
        using StringReader stringReader = new StringReader(value);
        return serializer.Deserialize(stringReader);
    }
}
