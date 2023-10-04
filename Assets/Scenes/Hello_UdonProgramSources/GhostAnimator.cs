
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostAnimator : UdonSharpBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        StopAnimation();
    }
    public void PlayAnimation()
    {
        anim.enabled = true;
    }
    public void StopAnimation()
    {
        anim.enabled = false;
    }
}
