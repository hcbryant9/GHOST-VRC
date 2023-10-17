
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GeneralInteraction : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;
    public GameManager manager;
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
        {
        manager.generalInteraction = true;
        }
    }
