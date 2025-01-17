using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static class AnimatorUtility
{
    public static string[] GetAnimationClips(Animator animator)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
            return new string[0];

        var controller = animator.runtimeAnimatorController as AnimatorController;
        if (controller == null)
            return new string[0];

        var clips = controller.animationClips;
        string[] clipNames = new string[clips.Length];

        for (int i = 0; i < clips.Length; i++)
        {
            clipNames[i] = clips[i].name;
        }

        return clipNames;
    }
}