
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Sticker : UdonSharpBehaviour
{
    public GameManager manager;
    void OnDrop()
    {
        manager.startCount++;
        //deleting stars
        DestroySelf();
    }
    void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
