
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.Playables;

public class Sun : UdonSharpBehaviour
{
    public PlayableDirector playableDirector;
    public MeshRenderer meshRenderer;

    public void PlayDirectorAndShowRenderer()
    {
        // Check if the Playable Director is not null
        if (playableDirector != null)
        {
            // Play the Playable Director
            playableDirector.Play();
        }

        // Check if the Mesh Renderer is not null
        if (meshRenderer != null)
        {
            // Enable the Mesh Renderer to make it visible
            meshRenderer.enabled = true;
        }
    }
}
