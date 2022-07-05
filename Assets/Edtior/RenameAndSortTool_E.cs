using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RenameAndSortTool_E
{
    private static int sortIndex = 1;
    
    [MenuItem("GameObject/RenameSort %#F1", false, -3)]
    public static void RenameAndSort()
    {
        if (Selection.activeObject)
        {
            string name = Selection.activeObject.name;
            Selection.activeObject.name = "";
            Selection.activeObject.name = name + sortIndex.ToString("00");
            sortIndex++;
        }
    }
    
    [MenuItem("GameObject/RefreshSortIndex", false, -2)]
    public static void RefreshSortIndex()
    {
        if (Selection.activeObject)
        {
            sortIndex = 1;
        }
    }
    

    // //批量命名排序
    // [MenuItem("GameObject/RenameSortAllSelection", false, -4)]
    // public static void RenameAndSortAll()
    // {
    //     if (Selection.gameObjects.Length> 0)
    //     {
    //         string name = Selection.gameObjects[sortIndex -1].name;
    //         Selection.gameObjects[sortIndex-1].name = name + sortIndex.ToString("00");
    //         sortIndex++;
    //     }
    // }
    
}
