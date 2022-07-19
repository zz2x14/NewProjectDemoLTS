using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    protected Animator anim;
    protected AnimatorStateInfo animInfo;

    [SerializeField] protected MagicDataContainer magic;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

}
