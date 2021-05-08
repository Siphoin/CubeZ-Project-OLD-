using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class UnityPython
{
    private const string NAME_LIBRARY_UNITY_EDITOR = "UnityEditor";


    private const string PATH_DATA_ASSEMBLY_API = "cbz_api/CBZ_API_ASSEMBLY";


    private static CBZ.API.Assembly.AssemblyUserAPIList libs = null;



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


        LoadAPIAssembly();

        // Load assemblies for the `UnityEngine*` namespaces
        for (int i = 0; i < libs.AssemblyList.Length; i++)
        {
            foreach (var assembly in GetAssembliesInNamespace(libs.AssemblyList[i]))
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

    private static void LoadAPIAssembly()
    {
        if (libs == null)
        {
            libs = Resources.Load<CBZ.API.Assembly.AssemblyUserAPIList>(PATH_DATA_ASSEMBLY_API);

            if (libs == null)
            {
                throw new PythonEngineException("libs assembly API not found");
            }

        }
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
