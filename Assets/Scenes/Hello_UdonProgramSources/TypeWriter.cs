
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class TypeWriter : UdonSharpBehaviour
{
    public TMPro.TextMeshProUGUI textObject;
    [UdonSynced]private bool isTyping = false;
    [UdonSynced]private int typingProgress = 0;
    
    public void Write(string message)
    {
        if(textObject != null)
        {
            if (!isTyping)
            {
                typingProgress = 0;
                isTyping = true;
            }
            while(typingProgress < message.Length)
            {
                textObject.text += message[typingProgress];
                typingProgress++;
            }
            
            isTyping = false;
            
        } else
        {
            Debug.Log("TMPro object is empty");
        }
        
    }
    public void ResetTyping()
    {
        isTyping = false;
        typingProgress = 0;
        textObject.text = "";
    }
}
