using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UDL.Editor
{
	public class InitTestSceneRemover
	{
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void Init()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeStateChanged;
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;

            AssetDatabase.SaveAssets();
        }

        public static void OnPlaymodeStateChanged(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.EnteredEditMode)
			{
				var files = Directory.GetFiles(Application.dataPath);
				for (int i = 0; i < files.Length; i++)
				{
					var file = files[i];
					if (file.Contains("InitTestScene") && !file.EndsWith(".meta", System.StringComparison.Ordinal))
					{
						File.Delete(file);
						Debug.LogWarning("Deleted an auto-generate test scene: " + file);
					}
				}
			}
			AssetDatabase.Refresh();
		}
	}
}
