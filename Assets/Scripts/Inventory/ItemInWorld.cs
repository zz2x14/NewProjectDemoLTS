using System;
using System.Collections;
using System.Collections.Generic;
using MyEventSpace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemInWorld : MonoBehaviour
{
    [SerializeField] private Item thisItem;
    [SerializeField] private bool testReduce;

    [SerializeField] private Vector2 itemMinForce;
    [SerializeField] private Vector2 itemMaxForce;

    private float randomForceX;
    private float randomForceY;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        EventManager.Instance.AddEventHandlerListener(EventName.OnSceneTeleport,DisableSelf);
        
        rb = GetComponent<Rigidbody2D>();

        randomForceX = Random.Range(itemMinForce.x, itemMaxForce.x);
        randomForceY = Random.Range(itemMinForce.y, itemMaxForce.y);
        
        rb.AddForce(new Vector2(randomForceX,randomForceY),ForceMode2D.Force);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventHandlerListener(EventName.OnSceneTeleport,DisableSelf);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerBackpackSystem.Instance.IsFull()) return;
            
            if (!testReduce)
            {
                if (thisItem.ItemID == 0)
                {
                    PlayerBackpackSystem.Instance.GetCoinMultiple(thisItem);
                }
                else
                {
                    PlayerBackpackSystem.Instance.AddItemIntoBackPack(thisItem);
                }
            
            }
            else
            {
                PlayerBackpackSystem.Instance.RemoveItemFromBackpack(thisItem);
            }
            
            gameObject.SetActive(false);
        }
    }

    //传送时物品禁用
    private void DisableSelf(object o ,EventArgs e)
    {
        gameObject.SetActive(false);
    }
    
    
}
