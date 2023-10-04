
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PayRespect : UdonSharpBehaviour
{
    
    void Start()
    {
        
    }
    void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
    void OnDrop()
    {
        // show some new text
        // disable object
        Debug.Log("Show some text and disable the object");
        DestroySelf();
        // waut for some seconds
    }

    
}
