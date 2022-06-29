using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public Transform playerPos { get; set; }
    
    [SerializeField] private NpcData npcData;
    public NpcData NpcData => npcData;
    
    [SerializeField] private bool willTalk;
    
    [SerializeField] private bool isMover;
    public bool IsMover => isMover;

    protected virtual void Awake()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    // public virtual void FaceToTarget(Transform target)
    // {
    //
    // }

}
