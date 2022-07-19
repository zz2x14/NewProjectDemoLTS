using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Canvas thisCanvas;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBarEffect;
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private float effectDelay;
    [SerializeField] private bool isDelay;

    private PlayerController player;

    private float targetRate;
    private float curRate;

    private Coroutine decreasedCor;
    private Coroutine increasedCor;

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

    public void UpdateHealthBar ()
    {
        targetRate = player.CurHealth / player.MaxHealth;
        
        if (curRate > targetRate)
        {
            if (decreasedCor != null)
            {
                StopCoroutine(decreasedCor);
            }
            
            healthBar.fillAmount = targetRate;
            decreasedCor = StartCoroutine(BarDecreasedEffectCor(healthBarEffect));
        }
        else
        {
            if (increasedCor != null)
            {
                StopCoroutine(increasedCor);
            }
            
            healthBarEffect.fillAmount = targetRate;
            increasedCor = StartCoroutine(BarIncreasedEffectCor(healthBar));
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
    IEnumerator BarIncreasedEffectCor(Image bar)
    {
        if (isDelay)
            yield return new WaitForSeconds(effectDelay);
        
        while (curRate < targetRate)
        {
            curRate += Time.deltaTime * decreaseSpeed;
            bar.fillAmount = curRate;
            
            yield return null;
        }
    }

    public void UpdateHealthBarImmediately(float curValue,float maxValue)
    {
        healthBar.fillAmount = curValue / maxValue;
        healthBarEffect.fillAmount = healthBar.fillAmount;
    }

    public void EnableHealthHUDCanvas()
    {
        thisCanvas.enabled = true;
    }

    public void DisableHeatlthHUDCanvas()
    {
        thisCanvas.enabled = false;
    }
    
}
