using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteModeTool_E : AssetPostprocessor
{
    private static int imageID = 10;
    
    [MenuItem("Assets/SetMultipleSingle",false,-12)]
    static void SetSpriteModeToMultipleSingle()
    {
        if(Selection.assetGUIDs.Length > 0)
        {
            AssetImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject));
            if(importer is TextureImporter)
            {
                if( (importer as TextureImporter).spriteImportMode != SpriteImportMode.Multiple)
                {
                    (importer as TextureImporter).spriteImportMode = SpriteImportMode.Multiple;
                }
            }

            importer.SaveAndReimport();
        }
    }

    [MenuItem("Assets/SetMultipleGroup", false, -13)]
    static void SetSpriteModeToMultipleGroup()
    {
        if (Selection.assetGUIDs.Length > 0)
        {
            for (int i = 0; i < Selection.instanceIDs.Length; i++)
            {
                AssetImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(Selection.instanceIDs[i]));

                if (importer is TextureImporter)
                {
                    if ((importer as TextureImporter).spriteImportMode != SpriteImportMode.Multiple)
                    {
                        (importer as TextureImporter).spriteImportMode = SpriteImportMode.Multiple;
                    }
                }

                importer.SaveAndReimport();
            }
        }
    }

    /// <summary>
    /// 批量重命名文件 - 避免拖入Tilemap时重复
    /// </summary>
    [MenuItem("Assets/RenameAsset %F1",false, -14)]
    static void RenameImage()
    {
        if (Selection.assetGUIDs.Length > 0)
        {
            for (int i = 0; i < Selection.instanceIDs.Length; i++)
            { 
                imageID++;
                AssetDatabase.RenameAsset
                    (AssetDatabase.GetAssetPath(Selection.instanceIDs[i]), imageID.ToString());
            }
        }
        
        AssetDatabase.Refresh();
    }

    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        if (textureImporter.assetPath.Contains("Images"))
        {
            if (textureImporter.assetPath.Contains("Tilesets") || textureImporter.assetPath.Contains("Objects"))
            {
                textureImporter.spritePixelsPerUnit = 32;
            }
            else if(textureImporter.assetPath.Contains("Player") || textureImporter.assetPath.Contains("Enemy"))
            {
                textureImporter.spriteImportMode = SpriteImportMode.Multiple;
                //if (textureImporter.spriteImportMode != SpriteImportMode.Multiple)
                //{
                //    textureImporter.spriteImportMode = SpriteImportMode.Multiple;
                //    textureImporter.spritePivot = new Vector2(0.33f, 0);
                //}
            }
           
            textureImporter.filterMode = FilterMode.Point;
        }
    }
}
