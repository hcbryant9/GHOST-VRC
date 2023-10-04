
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostScript : UdonSharpBehaviour
{
    public TypeWriter writer;
    void Start()
    {
        SendScript();
    }
    public void SendScript()
    {
        if (writer != null)
        {
            writer.Write("hello world");
        } else
        {
            Debug.Log("Writer is null");
        }
        
    }
}
