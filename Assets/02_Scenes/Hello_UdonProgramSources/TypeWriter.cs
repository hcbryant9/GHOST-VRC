
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class TypeWriter : UdonSharpBehaviour
{
    public TMPro.TextMeshProUGUI textObjectKor;
    public TMPro.TextMeshProUGUI textObjectEng;
    public RawImage textbox;
    public GameManager manager;
    
    public void Write(string message)
    {
        if(manager != null)
        {
            //is english -> write to the english textbox
            if (manager.isEnglish)
            {
                if (textObjectEng != null)
                {
                    textbox.enabled = true;
                    textObjectEng.text = message;
                }
                else
                {
                    Debug.Log("english textbox is null");
                }
            }
            else
            {
                if(textObjectKor != null)
                {
                    textbox.enabled = true;
                    textObjectKor.text = message;
                }
                else
                {
                    Debug.Log("korean textbox is null");
                }
            }
        
        } else
        {
            Debug.Log("manager is null");
        }
       
        
    }
    public void ResetTyping()
    {
        
        textbox.enabled = false;
        textObjectEng.text = "";
        textObjectKor.text = "";
    }
}
