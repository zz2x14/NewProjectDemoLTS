using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : PersistentSingletonTool<PoolManager>
{
    private Dictionary<GameObject, Pool> poolTable = new Dictionary<GameObject, Pool>();

    public Pool[] bulletPools;
    
    protected override void Awake()
    {
        base.Awake();
        
        InitializePools(bulletPools);
    }
    
#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(bulletPools);
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
