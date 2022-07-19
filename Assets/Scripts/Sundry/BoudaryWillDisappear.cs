using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(800)]
public class BoudaryWillDisappear : Boundary
{
    [SerializeField] private GameChapter matchingChapter;

    private void OnEnable()
    {
        if (matchingChapter == GameManager.Instance._GameChapter)
        {
            gameObject.SetActive(false);
        }
    }
}
