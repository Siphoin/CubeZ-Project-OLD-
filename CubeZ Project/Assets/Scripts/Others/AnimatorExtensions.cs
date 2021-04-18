using System.Collections;
using UnityEngine;

    public static class AnimatorExtensions
    {

       public static AnimationClip GetCurrentClipPlayed (this Animator animator)
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip;
    }
    }