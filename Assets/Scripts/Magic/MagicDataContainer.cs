using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum MagicType
{
    Damage,
    Cure,
    Control
}
public enum MagicApproach
{
    General,
    OnPlayer,
    OnEnemy,
}

public class MagicDataContainer : ScriptableObject
{
    [SerializeField] private MagicBaseData magicBase;
    
    public bool isMaster;
    public bool isReady;
    public MagicType magicType;
    public MagicApproach magicApproach;
    
    public int ID => magicBase.magicID;
    public string Name => magicBase.magicName;
    public Sprite Icon => magicBase.magicIcon;
    public string Des => magicBase.magicDescription;
    public float ColdDown => magicBase.magicCD;
    public int Cost => magicBase.magicCost;
    public GameObject MagicPrefab => magicBase.magicPrefab;

    private void OnEnable()
    {
        if (isMaster)
        {
            isReady = true;
        }
    }
}

[System.Serializable]
public class MagicBaseData
{
    public int magicID;
    public string magicName;
    public Sprite magicIcon;
    [TextArea] public string magicDescription;
    public GameObject magicPrefab;
    public float magicCD;
    public int magicCost;
}
