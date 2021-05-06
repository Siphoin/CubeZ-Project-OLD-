using IronPython.Hosting;
using System.Collections;
using UnityEngine;

    public class PythonEngine : MonoBehaviour
    {
		public static PythonEngine Engine { get; private set; }
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

		public void CompileString (string code)
        {
			var engine = UnityPython.CreateEngine();
			var scope = engine.CreateScope();


			var source = engine.CreateScriptSourceFromString(code);
			source.Execute(scope);
		}
	}