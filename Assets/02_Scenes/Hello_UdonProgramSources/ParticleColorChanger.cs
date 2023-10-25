
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleColorChanger : UdonSharpBehaviour
{
    public ParticleSystem particleSystem;
    public Gradient[] colorPresets;
    void Start()
    {
        if (particleSystem != null && colorPresets != null)
        {
            ChangeParticleColorPresent(0);
        }
    }
    public void ChangeParticleColorPresent(int presetIndex)
    {
        if (presetIndex < 0 || presetIndex >= colorPresets.Length)
        {
            Debug.LogWarning("Invalid preset index");
            return;
        }

        // Access the ColorOverLifetimeModule of the Particle System
        var colorOverLifetime = particleSystem.colorOverLifetime;

        // Apply the chosen color preset
        colorOverLifetime.color = colorPresets[presetIndex];
    }
}
