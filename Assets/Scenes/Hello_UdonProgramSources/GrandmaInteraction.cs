
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

public class GrandmaInteraction : UdonSharpBehaviour
{
    public GhostScript script;
    public int scriptSize = 10;
    private bool canAdvanceText = false;
    private VRCPlayerApi localPlayer;
    private int scriptCounter = 0;
    private bool playerIsInArea;
    private string[] scriptArr = new string[]
    {
        "Well, Hello Again",
        "Don't worry, dearie; you're young. You'll learn in time that I don't live with my body.",
        "You can visit me wherever you please. It's just here where there's nothing to distract you from me",
        "I live in your head, along with everyone else you've met, alive and dead. You just don't have a body to project me onto any longer",
        "I'm very much still me. I just have a little less ... autonomy than I once did",
        "Enough of my blabbering; thank you for humoring me, darling. Let's go around and meet some of the other ghosts here.",
        "No one I'm aware of.",
        "No one truly knows anyone, same for oneself.",
        "The ghosts you see are evoked by their reminders here. They'll be just as real as they ever were, they just, like myself, have a little less autonomy",
        "Ah, there I go being cryptic agai; it's the only thing the dead know how to be.",
        "There's much to learn; let's go!"
    };
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (script != null)
        {
            playerIsInArea = true;
            script.SendScript(scriptArr[0]);
            scriptCounter++;
            canAdvanceText = true;
        } else
        {
            Debug.Log("the script for gma is null");
        }
        
    }
    private void Update()
    {
        if ((canAdvanceText && localPlayer != null) && playerIsInArea)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                script.SendScript(scriptArr[scriptCounter]);
                scriptCounter++;
                if(scriptCounter > scriptSize)
                {
                    canAdvanceText = false;
                }
            }
        }
    }
    private void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        playerIsInArea = false;
        scriptCounter = 0;
        script.ClearText();
    }
}
