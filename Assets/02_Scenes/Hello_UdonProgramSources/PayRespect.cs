
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PayRespect : UdonSharpBehaviour
{
    private bool isDropped = false;
    private float dropTime = 0f;
    public GhostAnimator ghost;

    public ParticleColorChanger particleColorChanger;
    void DestroySelf()
    {
        if (ghost != null)
        {
            ghost.PlayAnimation();
        }
        
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
    void OnDrop()
    {
        isDropped = true;
        dropTime = Time.time;
        if (particleColorChanger != null)
        {
            particleColorChanger.ChangeParticleColorPresent(3);
        }
        

    }
    void Update()
    {
        //check if the object has been dropped for 2 seconds
        if (isDropped && Time.time - dropTime >= 2f)
        {
            Debug.Log(Time.time);
            Debug.Log("Show some text and disable the object");
            DestroySelf();
        }
    }

}
