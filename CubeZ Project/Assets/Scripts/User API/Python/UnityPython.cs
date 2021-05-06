using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Convenience class for creating a Python engine integrated with Unity.
///
/// All scripts executed by the ScriptEngine created by this class will:
/// * Redirect output to Unity's console
/// * Be able to import any class in a `UnityEngine*` namespace
/// * Be able to import any class in a `UnityEditor*` namespace, if the script
///   is running in the UnityEditor
/// </summary>
public static class UnityPython
{
    private const string NAME_LIBRARY_UNITY_EDITOR = "UnityEditor";


    private static string[] libs = new string[]
	{
		"UnityEngine",
		"CBZ.API",
	    "CBZ.API.Random",
	};


	public static ScriptEngine CreateEngine(IDictionary<string, object> options = null)
	{
		var engine = Python.CreateEngine(options);

		// Redirect IronPython IO
		var infoStream = new MemoryStream();
		var infoWriter = new UnityLogWriter(Debug.Log, infoStream);
		engine.Runtime.IO.SetOutput(infoStream, infoWriter);

		var errorStream = new MemoryStream();
		var errorWriter = new UnityLogWriter(Debug.LogError, errorStream);
		engine.Runtime.IO.SetErrorOutput(errorStream, errorWriter);

        // Load assemblies for the `UnityEngine*` namespaces
        for (int i = 0; i < libs.Length; i++)
        {
		foreach (var assembly in GetAssembliesInNamespace(libs[i]))
		{
			engine.Runtime.LoadAssembly(assembly);
		}
        }


        // Load assemblies for the `UnityEditor*` namespaces

#if UNITY_EDITOR
			foreach (var assembly in GetAssembliesInNamespace(NAME_LIBRARY_UNITY_EDITOR))
			{
				engine.Runtime.LoadAssembly(assembly);
			}
		
#endif


		return engine;
	}

	/// <summary>
	/// Get a list of all loaded assemblies in the current AppDomain for a
	/// namespace beginning with the specified string.
	/// </summary>
	/// <param name="prefix">The beginning of the namespace.</param>
	/// <returns>All matching assemblies.</returns>
	private static IEnumerable<Assembly> GetAssembliesInNamespace(string prefix)
	{
		return AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(t => t.GetTypes())
			.Where(t => t.Namespace != null && t.Namespace.StartsWith(prefix))
			.Select(t => t.Assembly)
			.Distinct();
	}
}
