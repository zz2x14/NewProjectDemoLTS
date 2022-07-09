using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineTest : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public TimelineAsset timelineAsset;

    private void Awake()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            FindPlayerTransform();
        }
    }

    public void DisablePlayerGameplayInput()
    {
        ComponentProvider.Instance.PlayerInputAvatar.DisableGamePlayInput();
    }

    public void FindPlayerTransform()
    {
        Debug.Log(1);
        var bingding = playableDirector.GetGenericBinding(timelineAsset.GetOutputTrack(0));

        playableDirector.SetGenericBinding(bingding,FindObjectOfType<PlayerController>().transform);
    }
}
