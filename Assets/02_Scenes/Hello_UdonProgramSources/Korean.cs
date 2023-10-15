
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Korean : UdonSharpBehaviour
{

    public GameManager manager;
    //public GameObject teleport;
    private VRCPlayerApi localPlayer;
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    //void OnDrop()
    void Interact()
    {
        manager.isEnglish = false;
        Debug.Log("english");


        /*if (localPlayer != null)
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
        }*/

    }

}
