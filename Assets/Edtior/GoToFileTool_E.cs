using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GoToFileTool_E 
{
    const string PATH_AssetsImages = "I:/zhuomian/Unity Recourses/CraftPix/2dNewProjectAssets";

    [MenuItem("Assets/GoToFile/AssetsFile",false)]//TODO:这种情况如何将其显示在上方
    static void OpenGameAssetsImagesFile()
    {
        EditorUtility.RevealInFinder(PATH_AssetsImages);
    }

    [MenuItem("Assets/GoToFile/DefaultDataFile",false)]
    static void OpenDefaultDataFile()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
    
}
