
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostScript : UdonSharpBehaviour
{
    public TypeWriter writer;
    
    public void SendScript(string message)
    {
        if (writer != null)
        {
            writer.Write(message);
            
        } else
        {
            Debug.Log("Writer is null");
        }
        
    }
    public void ClearText()
    {
        writer.ResetTyping();
    }
}
