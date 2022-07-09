using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : PersistentSingletonTool<UIManager>
{
    [SerializeField] private GameObject damageValueEffecPrefab;

    public void ShowDamageValue(Vector3 spawnPos,float damageValue)
    {
       GameObject textGO =  PoolManager.Instance.Release(damageValueEffecPrefab, spawnPos);
       textGO.GetComponent<DamageValueEffect>().UpdateDamageValue(damageValue);
    }


}
