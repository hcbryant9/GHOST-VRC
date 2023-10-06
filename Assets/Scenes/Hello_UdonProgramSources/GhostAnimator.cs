
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostAnimator : UdonSharpBehaviour
{
    private Animator anim;
    public GhostScript script;
    void Start()
    {
        anim = GetComponent<Animator>();
        StopAnimation();
    }
    public void PlayAnimation()
    {
        anim.enabled = true;
        if(script != null)
        {
            script.SendScript("hello world");
        }
        else
        {
            Debug.Log("ghost script is null in ghost animator");
        }
    }
    public void StopAnimation()
    {
        
        anim.enabled = false;
    }
}
