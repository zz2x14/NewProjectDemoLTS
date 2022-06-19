using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    private Rigidbody2D rb;
    public float FlySpeed { get; set; }
    public float FlyDir { get; set; }
    public float Damage { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(BulletFlyCor));
    }

    IEnumerator BulletFlyCor()
    {
        while (gameObject.activeSelf)
        {
            rb.velocity = GetBulletDir();

            yield return null;
        }
    }

    protected virtual Vector2 GetBulletDir()
    {
        return Vector2.right * - FlyDir * FlySpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out CharacterBase character))
        {
            character.TakenDamage(Damage);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
