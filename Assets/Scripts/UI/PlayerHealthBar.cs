using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBarEffect;
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private float effectDelay;
    [SerializeField] private bool isDelay;

    private PlayerController player;

    private float targetRate;
    private float curRate;

    private Coroutine decreasedCor;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        
        InitializeHealthBar();
    }

    private void OnEnable()
    {
        player.OnHurt += UpdateHealthBar;
    }

    private void OnDisable()
    {
        player.OnHurt -= UpdateHealthBar;
    }
 
    private void InitializeHealthBar()
    {
        healthBar.fillAmount = player.CurHealth / player.MaxHealth;
        healthBarEffect.fillAmount = player.CurHealth / player.MaxHealth;
        curRate = player.CurHealth / player.MaxHealth;
        targetRate = curRate;
    }

    private void UpdateHealthBar()
    {
        targetRate = player.CurHealth / player.MaxHealth;
        
        if (decreasedCor != null)
        {
            StopCoroutine(decreasedCor);
        }

        if (targetRate < curRate)
        {
            healthBar.fillAmount = targetRate;
            decreasedCor = StartCoroutine(BarDecreasedEffectCor(healthBarEffect));
        }
    }

    IEnumerator BarDecreasedEffectCor(Image bar)
    {
        if (isDelay)
            yield return new WaitForSeconds(effectDelay);
        
        while (curRate > targetRate)
        {
            curRate -= Time.deltaTime * decreaseSpeed;
            bar.fillAmount = curRate;
            
            yield return null;
        }
    }
    
}
