using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boundary : MonoBehaviour
{
    [SerializeField] private bool reboundPlayer;
    [SerializeField] private Vector2 reboundForce;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (reboundPlayer)
        {
            player.Forced(reboundForce);
        }
    }

    
}
