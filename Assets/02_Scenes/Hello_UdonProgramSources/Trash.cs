
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Trash : UdonSharpBehaviour
{
    public GameManager manager;
    
    void OnPickup()
    {
        //destroy self
        //play sound
        //inc counter
        manager.trashCount++;
        destroySelf();
       
    }
    void destroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
