using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicType
{
    Damage,
    Cure,
    Control
}
public class Magic : ScriptableObject
{
    [SerializeField] private MagicBaseData magicBase;
    public bool isMaster;
    public MagicType magicType;
    public int ID => magicBase.magicID;
    public string Name => magicBase.magicName;
    public Sprite Icon => magicBase.magicIcon;
    public string Des => magicBase.magicDescription;
    public int Cost => magicBase.magicCost;
}

[System.Serializable]
public class MagicBaseData
{
    public int magicID;
    public string magicName;
    public Sprite magicIcon;
    [TextArea] public string magicDescription;
    public int magicCost;
}
