using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData:ScriptableObject
{
    public CharacterBaseData baseData;

    public void InitializeHealth()
    {
        baseData.curHealth = baseData.maxHealth;
    }
}

[System.Serializable]
public class CharacterBaseData
{
    public float maxHealth;
    public float curHealth;
    public float attackDamage;

    // public float CurHealth
    // {
    //     get => curHealth;
    //     set => curHealth = Mathf.Max(value, 0);
    // }
    //
    // public float MaxHealth
    // {
    //     get => maxHealth;
    //     set => maxHealth = value;
    // }

}
