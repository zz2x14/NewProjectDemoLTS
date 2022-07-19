using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicData/ControlMagicData",fileName = "NewControlMagicData")]
public class ControlMagic : MagicDataContainer
{
    [SerializeField] private ControlMagicData ControlMagicData;
    public float ControlValue => ControlMagicData.controlValue;
}

[System.Serializable]
public class ControlMagicData
{
    public float controlValue;
}

