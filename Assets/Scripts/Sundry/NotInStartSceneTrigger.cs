using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotInStartSceneTrigger : MonoBehaviour
{
    private PlayerInput input;

    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        input.InStartScene = false;
    }
}
