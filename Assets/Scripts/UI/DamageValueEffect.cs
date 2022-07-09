using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageValueEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private float flyUpSpeed;

    private Vector3 flySpeed;

    private void Awake()
    {
        flySpeed = new Vector3(0, flyUpSpeed, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(FlyUpCor));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator FlyUpCor()
    {
        while (gameObject.activeSelf)
        {
            transform.position += flySpeed * Time.deltaTime;
            
            yield return null;
        }
    }

    public void UpdateDamageValue(float value)
    {
        damageValueText.text = value.ToString();
    }
}
