
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

public class GrandmaInteraction : UdonSharpBehaviour
{
    public GhostScript script;
    private bool canAdvanceText = false;
    private VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (script != null)
        {
            script.SendScript("Well, hello again.");
            canAdvanceText = true;
        } else
        {
            Debug.Log("the script for gma is null");
        }
        
    }
    private void Update()
    {
        if (canAdvanceText && localPlayer != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                script.SendScript("next line");
            }
        }
    }
}
