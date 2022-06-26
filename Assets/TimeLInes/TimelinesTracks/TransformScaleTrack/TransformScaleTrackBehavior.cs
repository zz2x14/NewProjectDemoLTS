using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TransformScaleTrackBehavior : PlayableBehaviour
{
    public Transform transform;
    public Vector3 scale;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
       transform.localScale = scale;
    }
}
