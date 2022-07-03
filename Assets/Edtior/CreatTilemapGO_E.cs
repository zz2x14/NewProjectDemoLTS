using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class CreatTilemapGO_E
{
    private const string NAME_FRONTSORTINGLAYER = "Ground";
    private const string NAME_ENVIRONMENTITEMSORTINGLAYER = "EnvironmentItem";
    
    [MenuItem("GameObject/CreatMyTilemapGo/GroundTilemap",false,-1)]
    static void CreatGroundTilemapGO()
    {
        if (Selection.activeTransform.GetComponent<Grid>())
        {
            GameObject tilemapGO = new GameObject("NewGround") {layer = 7};
            tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();
            tilemapGO.GetComponent<TilemapRenderer>().sortingLayerName = NAME_FRONTSORTINGLAYER;
            tilemapGO.AddComponent<Rigidbody2D>();
            tilemapGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            tilemapGO.AddComponent<TilemapCollider2D>();
            tilemapGO.GetComponent<TilemapCollider2D>().usedByComposite = true;
            tilemapGO.AddComponent<CompositeCollider2D>();
            tilemapGO.transform.SetParent(Selection.activeTransform);
        }
    }
    
    [MenuItem("GameObject/CreatMyTilemapGo/StairsTilemap")]
    static void CreatStairsTilemapGO()
    {
        if (Selection.activeTransform.GetComponent<Grid>())
        {
            GameObject tilemapGO = new GameObject("NewStairs") {layer = 8};
            tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();
            tilemapGO.GetComponent<TilemapRenderer>().sortingLayerName = NAME_ENVIRONMENTITEMSORTINGLAYER;
            tilemapGO.AddComponent<Rigidbody2D>();
            tilemapGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            tilemapGO.AddComponent<TilemapCollider2D>();
            tilemapGO.GetComponent<TilemapCollider2D>().usedByComposite = true;
            tilemapGO.AddComponent<CompositeCollider2D>();
            tilemapGO.transform.SetParent(Selection.activeTransform);
        }
    }
    
    [MenuItem("GameObject/CreatMyTilemapGo/StairsGroundTilemap")]
    static void CreatStairsGroundTilemapGO()
    {
        if (Selection.activeTransform.GetComponent<Grid>())
        {
            GameObject tilemapGO = new GameObject("NewStairsGround") {layer = 9};
            tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();
            tilemapGO.GetComponent<TilemapRenderer>().sortingLayerName = NAME_FRONTSORTINGLAYER;
            tilemapGO.AddComponent<Rigidbody2D>();
            tilemapGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            tilemapGO.AddComponent<TilemapCollider2D>();
            tilemapGO.GetComponent<TilemapCollider2D>().usedByComposite = true;
            tilemapGO.AddComponent<CompositeCollider2D>();
            tilemapGO.transform.SetParent(Selection.activeTransform);
        }
    }
    
    [MenuItem("GameObject/CreatMyTilemapGo/HangGroundTilemap")]
    static void CreatHangGroundTilemapGO()
    {
        if (Selection.activeTransform.GetComponent<Grid>())
        {
            GameObject tilemapGO = new GameObject("NewHangGround") {layer = 10};
            tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();
            tilemapGO.GetComponent<TilemapRenderer>().sortingLayerName = NAME_FRONTSORTINGLAYER;
            tilemapGO.AddComponent<Rigidbody2D>();
            tilemapGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            tilemapGO.AddComponent<TilemapCollider2D>();
            tilemapGO.GetComponent<TilemapCollider2D>().usedByComposite = true;
            tilemapGO.AddComponent<CompositeCollider2D>();
            tilemapGO.transform.SetParent(Selection.activeTransform);
        }
    }
    
    [MenuItem("GameObject/CreatMyTilemapGo/EnvironmentObjectTilemap")]
    static void CreatEnvironmentObjectTilemapGO()
    {
        if (Selection.activeTransform.GetComponent<Grid>())
        {
            GameObject tilemapGO = new GameObject("NewEnvironmentObject") {layer = 12};
            tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();
            tilemapGO.GetComponent<TilemapRenderer>().sortingLayerName = NAME_ENVIRONMENTITEMSORTINGLAYER;
            tilemapGO.transform.SetParent(Selection.activeTransform);
        }
    }

    
}
