using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckAnimProgressTool 
{
    public static bool AnimOverWithName(Animator anim,AnimatorStateInfo animatorStateInfo,string animName,float progress)
    {
        animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(animName) && animatorStateInfo.normalizedTime >= progress;
    }
    
    public static bool AnimOverWithTag(Animator anim,AnimatorStateInfo animatorStateInfo,string animTag,float progress)
    {
        animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsTag(animTag) && animatorStateInfo.normalizedTime >= progress;
    }
}
