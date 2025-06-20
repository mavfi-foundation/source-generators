// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Reflection;
using System.Text;

namespace MavFiFoundation.SourceGenerators.TestSupport;

public static class EmbeddedResourceHelper
{
	public const string EmbeddedSourceFolderName = "SourceFiles";

	public enum EmbeddedResourceType
	{
		Code,
		AdditionalFiles,
		GeneratedCode
	}

	private static StreamReader GetEmbeddedResourceReader(string name, Assembly? assembly = null)
	{
		if (assembly == null)
		{
			assembly = Assembly.GetExecutingAssembly();
		}	

		var resName = $"{assembly.GetName().Name}.{name}";
		var stream = assembly.GetManifestResourceStream(resName)!;  	
		return new StreamReader(stream, Encoding.UTF8);  
	}

	public static async Task<string> ReadEmbeddedSourceAsync(string name, Assembly? assembly = null)
	{	
		using var sr = GetEmbeddedResourceReader(name, assembly);
		return await sr.ReadToEndAsync();
	}

	public static string ReadEmbeddedSource(string name, Assembly? assembly = null)
	{	
		using var sr = GetEmbeddedResourceReader(name, assembly);
		return sr.ReadToEnd();
	}

	public static async Task<string> ReadEmbeddedSourceAsync(string name, EmbeddedResourceType resType)
	{		
		var resName = $"{EmbeddedSourceFolderName}.{resType.ToString()}.{name}";
		return await ReadEmbeddedSourceAsync(resName);
	}

	public static string ReadEmbeddedSource(string name, EmbeddedResourceType resType)
	{		
		var resName = $"{EmbeddedSourceFolderName}.{resType.ToString()}.{name}";
		return ReadEmbeddedSource(resName);
	}

}
