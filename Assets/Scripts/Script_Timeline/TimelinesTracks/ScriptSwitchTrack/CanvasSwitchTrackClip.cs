using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CanvasSwitchTrackClip : PlayableAsset
{
    public ExposedReference<Canvas> canvas;

    public bool enable = true;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playabe = ScriptPlayable<CanvasSwitchTrackBehavior>.Create(graph);
        
        var canvasSwith = playabe.GetBehaviour();

        canvasSwith.canvas = canvas.Resolve(graph.GetResolver());

        canvasSwith.enable = enable;
        
        return playabe;
    }
}
