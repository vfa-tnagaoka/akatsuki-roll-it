using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImportTexture : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetImporter.assetPath.Contains("Resources/Textures"))
        {
            TextureImporter textureImporter = assetImporter as TextureImporter;
            textureImporter.isReadable = true;
            if (assetImporter.assetPath.Contains("Textures/Stencils"))
            {
                Debug.Log(assetImporter.assetPath);
                textureImporter.SetPlatformTextureSettings("iPhone", 256, TextureImporterFormat.ETC2_RGBA8);
                textureImporter.SetPlatformTextureSettings("Android", 256, TextureImporterFormat.ETC2_RGBA8);
                AssetDatabase.SaveAssets();
            }
        }
            
    }
}