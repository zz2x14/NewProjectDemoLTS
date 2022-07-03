using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    private PlayableDirector director;

    private float startTime;
    

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    IEnumerator WaitPlayOverToDestroy()
    {
        startTime = Time.time;
        
        while (director.duration > Time.time - (startTime + 0.1f))
        {
            yield return null;
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        director.Play();
        StartCoroutine(nameof(WaitPlayOverToDestroy));
    }
}
