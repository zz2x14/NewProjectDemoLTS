using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    [SerializeField] private Item thisItem;
    [SerializeField] private bool testReduce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!testReduce)
        {
            PlayerBackpackSystem.Instance.AddItemIntoBackPack(thisItem);
        }
        else
        {
            PlayerBackpackSystem.Instance.RemoveItemFromBackpack(thisItem);
        }
        
        Destroy(gameObject);
    }
}
