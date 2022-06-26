using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Playables;

public class TransformScaleTrackClip : PlayableAsset
{
    public ExposedReference<Transform> transform;
    public Vector3 scale;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TransformScaleTrackBehavior>.Create(graph);

        var scaleBehavior = playable.GetBehaviour();

        scaleBehavior.transform = transform.Resolve(graph.GetResolver());
        scaleBehavior.scale = scale;
        
        return playable;
    }
}
