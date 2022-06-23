using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CanvasSwitchTrackBehavior : PlayableBehaviour
{
    public Canvas canvas;
    
    public bool enable = true;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        canvas.enabled = enable;
    }
}
