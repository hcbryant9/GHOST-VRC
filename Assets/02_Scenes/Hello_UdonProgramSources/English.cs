
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class English : UdonSharpBehaviour
{
    public GameManager manager;
    //public GameObject teleport;
    private VRCPlayerApi localPlayer;
    public AudioSource ambient;
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    //void OnDrop()
    void Interact()
    {
        manager.isEnglish = true;
        Debug.Log("english");
        if (ambient != null)
        {
            ambient.Play();
        }

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
