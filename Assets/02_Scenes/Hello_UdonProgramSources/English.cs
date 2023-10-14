
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class English : UdonSharpBehaviour
{
    public GameManager manager;
    public GameObject teleport;
    private VRCPlayerApi localPlayer;
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    void OnDrop()
    {
        manager.isEnglish = true;
        Debug.Log("english");

        
        if (localPlayer != null)
        {
            if (teleport != null)
            {
                Debug.Log("teleporting to: " + teleport.transform.position);
                localPlayer.TeleportTo(teleport.transform.position, teleport.transform.rotation);
            }
            else
            {
                Debug.Log("teleport location is null");
            }

        }
        else
        {
            Debug.Log("player is null");
        }

    }


    

}
