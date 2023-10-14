using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class MusicZone : UdonSharpBehaviour
{
    public AudioClip music;
    public float volume = 0.5f;
    public AudioManager audioManager;
    
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            audioManager.StartAudioTransition(music, volume);
        }
    }
    
    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            audioManager.StartAudioTransition(audioManager.defaultTrack, audioManager.defaultVolume);
        }
    }
}