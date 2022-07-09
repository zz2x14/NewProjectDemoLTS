using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : PersistentSingletonTool<PoolManager>
{
    private Dictionary<GameObject, Pool> poolTable = new Dictionary<GameObject, Pool>();

    public Pool[] enemyBulletPools;
    public Pool[] playerBulletPools;
    public Pool[] enemyPools;
    public Pool[] itemPools;
    public Pool[] UIPools;

    private const string DAMAGEUIPARENTNAME = "DamageValueeEffectCanvas";
    
    protected override void Awake()
    {
        base.Awake();
        
        InitializePools(enemyBulletPools);
        InitializePools(playerBulletPools);
        InitializePools(enemyPools);
        InitializePools(itemPools);
        //InitializePools(UIPools);
        InitializeDamageValueUIPools(UIPools);
    }
    
#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(enemyBulletPools);
        CheckPoolSize(playerBulletPools);
        CheckPoolSize(enemyPools);
        CheckPoolSize(itemPools);
        CheckPoolSize(UIPools);
    }
#endif

    private void CheckPoolSize(Pool[] pools)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].RuntimeSize > pools[i].Size)
            {
                Debug.LogWarning($"Pool{pools[i].Prefab.name}'s RuntimeSize{pools[i].RuntimeSize} bigger than Size{pools[i].Size}");
            }
        }
    }
    
    private void InitializePools(Pool[] pools)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            GameObject parent = new GameObject("Pool:" + pools[i].Prefab.name);
            
            parent.transform.SetParent(transform);
            pools[i].ParentTransform = parent.transform;
            
            poolTable.Add(pools[i].Prefab,pools[i]);
            
            pools[i].Init();
        }
    }

    private void InitializeDamageValueUIPools(Pool[] pools)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].ParentTransform = GameObject.Find(DAMAGEUIPARENTNAME).transform;
            
            poolTable.Add(pools[i].Prefab,pools[i]);
            
            pools[i].Init();
        }
    }

    public GameObject Release(GameObject go)
    {
        return poolTable[go].GetPreparedGo();
    }
    public GameObject Release(GameObject go,Vector3 pos)
    {
        return poolTable[go].GetPreparedGo(pos);
    }
    public GameObject Release(GameObject go,Vector3 pos,Quaternion rotation)
    {
        return poolTable[go].GetPreparedGo(pos,rotation);
    }
    public GameObject Release(GameObject go,Vector3 pos,Quaternion rotation,Vector3 scale)
    {
        return poolTable[go].GetPreparedGo(pos,rotation,scale);
    }
}
