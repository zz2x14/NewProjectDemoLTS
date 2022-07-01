using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTool : MonoBehaviour
{
    [SerializeField] private GameChapter targetGameChapter;

    private void OnDisable()
    {
        GameManager.Instance._GameChapter = targetGameChapter;
    }
}
