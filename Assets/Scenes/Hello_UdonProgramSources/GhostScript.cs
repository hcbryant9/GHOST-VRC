
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostScript : UdonSharpBehaviour
{
    public TypeWriter writer;
    
    public void SendScript()
    {
        if (writer != null)
        {
            writer.Write("hello world, today we will be talking about how I became a ghost in a very fun and interesting way omg");
            
        } else
        {
            Debug.Log("Writer is null");
        }
        
    }
}
