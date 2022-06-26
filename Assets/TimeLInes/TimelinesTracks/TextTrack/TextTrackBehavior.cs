using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class TextTrackBehavior : PlayableBehaviour
{
   public Text text;

   public int textSize;
   public Color textColor;

   [TextArea]
   public string textContent;
   
   public override void ProcessFrame(Playable playable, FrameData info, object playerData)
   {
      text.fontSize = textSize;
      text.color = textColor;
      text.text = textContent;
   }
}
