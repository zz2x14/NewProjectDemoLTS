using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemDroppedFromEnemy : MonoBehaviour
{
    [Header("Item掉落")]
    [SerializeField] private List<GameObject> itemDroppedList = new List<GameObject>();
    [SerializeField] [Range(0,100)] private List<int> itemDroppedProbabilityList = new List<int>();

    [Header("金币掉落")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] [Range(0,100)] private int coinProbability;
    [SerializeField] private int coinDroppedMinNum;
    [SerializeField] private int coinDroppedMaxNum;

    private List<int> probabilityGotList;

    private int coinRandomValue;

    private void Awake()
    {
        probabilityGotList = new List<int>(itemDroppedList.Count);
    }

    private void OnEnable()
    {
        probabilityGotList.Clear();
    }

    public void DropItemAndCoin()
    {
        PlayerBackpackSystem.Instance.coinGotCount = 0;
        
        coinRandomValue = Random.Range(1, 101);
        PlayerBackpackSystem.Instance.coinGotCount = Random.Range(coinDroppedMinNum, coinDroppedMaxNum);
        
        if (coinRandomValue <= coinProbability)
        {
            PoolManager.Instance.Release(coinPrefab, transform.position);
        }

        for (int i = 0; i < itemDroppedList.Count; i++)
        {
            probabilityGotList.Add(GetProbabilityValue());
        }

        for (int i = 0; i < itemDroppedProbabilityList.Count; i++)
        {
            if (itemDroppedProbabilityList[i] == probabilityGotList[i])
            {
                PoolManager.Instance.Release(itemDroppedList[i], transform.position);
            }
        }
    }

    private void OnDisable()
    {
        DropItemAndCoin();
    }

    //Sign:重要的思维逻辑
    public int GetProbabilityValue()
    {
        int randomValue = Random.Range(1, 101);
        int probabilityValue = 0;
        
        for (int i = 0; i < itemDroppedProbabilityList.Count; i++)
        {
            probabilityValue += itemDroppedProbabilityList[i];
            
            if (randomValue < probabilityValue)
            {
                return probabilityValue;
            }
        }

        return 0;
    }
}
