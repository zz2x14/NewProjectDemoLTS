using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class TextTrackClip : PlayableAsset
{
    public ExposedReference<Text> text;

    public int textSize;
    public Color textColor;

    [TextArea]
    public string textContent;
    
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextTrackBehavior>.Create(graph);

        var textBehavior = playable.GetBehaviour();

        textBehavior.text = text.Resolve(graph.GetResolver());

        textBehavior.textSize = textSize;
        textBehavior.textColor = textColor;
        textBehavior.textContent = textContent;

        return playable;
    }
}
