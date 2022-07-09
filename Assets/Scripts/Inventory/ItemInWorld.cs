using System;
using System.Collections;
using System.Collections.Generic;
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
        rb = GetComponent<Rigidbody2D>();

        randomForceX = Random.Range(itemMinForce.x, itemMaxForce.x);
        randomForceY = Random.Range(itemMinForce.y, itemMaxForce.y);
        
        rb.AddForce(new Vector2(randomForceX,randomForceY),ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!testReduce)
            {
                if (thisItem.ItemID == 0)
                {
                    PlayerBackpackSystem.Instance.GetItemMultiple(thisItem);
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

            if (PlayerBackpackSystem.Instance.IsFull()) return;
            
            gameObject.SetActive(false);
        }
    }
    
    
}
