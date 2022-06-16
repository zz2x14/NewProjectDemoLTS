using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData:ScriptableObject
{
    public CharacterBaseData baseData;

    public void InitializeHealth()
    {
        baseData.CurHealth = baseData.MaxHealth;
    }
}

[System.Serializable]
public class CharacterBaseData
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;

    [SerializeField] private float attackDamage;

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float CurHealth
    {
        get => curHealth;
        set => curHealth = value;
    }

    public float AttackDamage
    {
        get => attackDamage;
        set => attackDamage = value;
    }
}
