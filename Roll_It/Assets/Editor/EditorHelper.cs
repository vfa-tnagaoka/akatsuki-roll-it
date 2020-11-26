using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class EditorHelper 
{

    [UnityEditor.MenuItem("Tools/Clear All PlayerPrefs")]
    public static void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
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
                    //Debug.Log("Deleted an auto-generate test scene" + file);
                }
            }
        }
        AssetDatabase.Refresh();
    }
}

