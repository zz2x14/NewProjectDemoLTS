using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicData/CureMagicData",fileName = "NewCureMagicData")]
public class CureMagic : Magic
{
    [SerializeField] private CureMagicData cureMagicData;
    public float CureValue => cureMagicData.cureValue;
}

[System.Serializable]
public class CureMagicData
{
    public float cureValue;
}

