
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class TypeWriter : UdonSharpBehaviour
{
    public TMPro.TextMeshProUGUI textObject;
    
    public void Write(string message)
    {
        if(textObject != null)
        {
            textObject.text = message;
        } else
        {
            Debug.Log("TMPro object is empty");
        }
        
    }
}
