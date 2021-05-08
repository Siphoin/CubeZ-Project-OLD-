using IronPython.Hosting;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

    public class PythonEngine : MonoBehaviour
    {
		public static PythonEngine Engine { get; private set; }

	public string ActiveFileRead { get; private set; }

	private const string PATH_SCRIPTS_MAP = "localData/session/scripts";
		void Awake()
		{
            if (Engine == null)
            {
				DontDestroyOnLoad(gameObject);
				Engine = this;
#if UNITY_EDITOR
				Debug.Log("Python engine is working...");
#endif
            }

            else
            {
				Destroy(gameObject);
            }
		}

		private void CompileString (string[] sourceCode, string[] files)
        {
			var engine = UnityPython.CreateEngine();
        for (int i = 0; i < sourceCode.Length; i++)
        {
			ActiveFileRead = files[i].Replace(CacheSystem.GetPathAssetsData(), string.Empty);


			var scope = engine.CreateScope();
			var source = engine.CreateScriptSourceFromString(sourceCode[i]);


            try
            {
				source.Execute(scope);
			}
			catch (Microsoft.Scripting.SyntaxErrorException e)
			{
				Loading.LoadScene("main_menu");
				CBZ.API.Debug.Debug.Print($"Python Error: {e.Message} ->", CBZ.API.Debug.LogMessageType.Error);
				CBZ.API.Debug.Debug.Print($"File path: {ActiveFileRead}", CBZ.API.Debug.LogMessageType.Error);

			}
		}
		
		}

	public void CompileScripts()
	{
        if (!Directory.Exists(CacheSystem.GetPathAssetsData() + PATH_SCRIPTS_MAP))
        {
			return;
        }
	string[] files = Directory.GetFiles(CacheSystem.GetPathAssetsData() + PATH_SCRIPTS_MAP, "*.py", SearchOption.AllDirectories);
		string[] data = new string[files.Length];
		for (int i = 0; i < files.Length; i++)
        {

			data[i] = File.ReadAllText(files[i]);
			
			
        }
		if (data.Length > 0)
        {
		CompileString(data, files);
        }

	}
}