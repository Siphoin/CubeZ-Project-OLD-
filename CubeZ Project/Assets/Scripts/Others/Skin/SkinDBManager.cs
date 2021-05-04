using UnityEngine;
using System.Linq;
public class SkinDBManager : MonoBehaviour
    {
    private const string PATH_DB_TEXTURES_FOLBERS = "Db/SkinDBFolbers";

    private const string PATH_FOLBER_TEXTURES = "Textures/";


    private static SkinDBManager manager;

    public static SkinDBManager Manager { get => manager; }

    private SkinDBFolbers dbFolbers;

    // Use this for initialization
    void Start()
        {
        if (manager == null)
        {
            dbFolbers = Resources.Load<SkinDBFolbers>(PATH_DB_TEXTURES_FOLBERS);

            if (dbFolbers == null)
            {
                throw new SkinDBManagerException("db folbers not found");
            }

            manager = this;
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            Debug.Log("Skin DB manager initilizated");
#endif
        }

        else
        {
            Destroy(gameObject);
        }
        }

    public Texture GetTextureFromPath (string nameTexture)
    {
        string[] folbers = dbFolbers.FolbersList;

        for (int i = 0; i < folbers.Length; i++)
        {
            string path = $"{PATH_FOLBER_TEXTURES}{folbers[i]}";
            Texture[] textures = Resources.LoadAll<Texture>(path);

            if (textures != null && textures.Length > 0)
            {
                if (textures.Any(t => t.name == nameTexture))
                {
                    return textures.First(a => a.name == nameTexture);
                }
            }
        }

        throw new SkinDBManagerException($"texture {nameTexture} not found");
    }

    }