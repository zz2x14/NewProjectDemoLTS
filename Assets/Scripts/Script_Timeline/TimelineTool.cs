using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineTool: MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    private const string NAME_PLAYERTFTWEENTARCK = "PlayerTfTweenTrack";
    private const string NAME_PLAYERANIMATORTRACK = "PlayerWalkAnimToPlayerIdleAnimTrack";

    private void Awake()
    {
        foreach (var bindingInfo in playableDirector.playableAsset.outputs)
        {
            if (bindingInfo.streamName == NAME_PLAYERTFTWEENTARCK)
            {
                playableDirector.SetGenericBinding(bindingInfo.sourceObject,FindObjectOfType<PlayerController>().transform);
            }

            if (bindingInfo.streamName == NAME_PLAYERANIMATORTRACK)
            {
                playableDirector.SetGenericBinding(bindingInfo.sourceObject,FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>());
            }
        }
    }
    
    //ForSignalTrack
    public void DisablePlayerGameplayInput()
    {
        ComponentProvider.Instance.PlayerInputAvatar.DisableGamePlayInput();
    }

    public void EnablePlayerGameplayInput()
    {
        ComponentProvider.Instance.PlayerInputAvatar.EnableGameplayInput();
    }

   
}
