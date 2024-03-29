﻿using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

public static class CacheSystem
    {
    private const string COMMENT_GENERATION = "// Generated by Cache System CubeZ Project\n// Version 1.1\n// All right resolved by Night Studio\n\n";
    private const string MAIN_FOLBER_NAME = "localData/";

    public static void SaveSerializeObject (string folberName, string fileName, object objectTarget, Formatting formatJson = Formatting.None)
    {
        string path = GetPathAssetsData() + MAIN_FOLBER_NAME + folberName;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);


#if UNITY_EDITOR
            Debug.Log($"Created new directory in cache game: Path: {path}");
#endif

        }
        string dataFile = null;
        try
        {
            dataFile = COMMENT_GENERATION + JsonConvert.SerializeObject(objectTarget, formatJson);
        }
        catch (JsonException e)
        {
            throw new CacheSystemException($"Cache System Exception: called error on JSON library: Error: {e.Message}");
        }

        string endPath = $"{path}/{fileName}";

        File.WriteAllText(endPath, dataFile);

#if UNITY_EDITOR
        Debug.Log($"File {fileName} saved on directory {path}");
#endif
    }

    public static T DeserializeObject<T> (string path)
    {
       
        if (!File.Exists(path))
        {
            throw new CacheSystemException($"Not found path to file. Path {path}");
        }

        string dataFile = File.ReadAllText(path);


        try
        {
        return JsonConvert.DeserializeObject<T>(dataFile);
        }
        catch (JsonException e)
        {
            throw new CacheSystemException($"Cache System Exception: called error on JSON library: Error: {e.Message}");
        }

    }

    public static T DeserializeObject<T>(JObject jObject)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(jObject.ToString());
        }
        catch (JsonException e)
        {
            throw new CacheSystemException($"Cache System Exception: called error on JSON library: Error: {e.Message}");
        }
    }

    public static bool FileExits (string folberName, string fileName)
    {
        string path = GetPathAssetsData() + MAIN_FOLBER_NAME + folberName + fileName;

        return File.Exists(path);
    }

    public static string GetPathAssetsData ()
    {
        RuntimePlatform currentPlatform = Application.platform;
        string path = null;

        if (currentPlatform == RuntimePlatform.WindowsPlayer || currentPlatform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }

        else
        {
            path = Application.persistentDataPath;
        }
        path += "/";
        return path;
    }

    public static void DeleteFile (string path) {


        if (!File.Exists(path))
        {
            throw new CacheSystemException($"Not found path to file. Path {path}");
        }

        File.Delete(path);

#if UNITY_EDITOR
        Debug.Log($"File on path {path} deleted.");
#endif
    }
    }