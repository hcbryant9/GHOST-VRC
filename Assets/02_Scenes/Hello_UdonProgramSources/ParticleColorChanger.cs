
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleColorChanger : UdonSharpBehaviour
{
    //for the particles
    public ParticleSystem particleSystem;
    public Gradient[] colorPresets;

    //for the sky
    public Color[] skyColors;
    public Material skyMaterial;
    private int currentColorIndex = 0;
    void Start()
    {
        if (particleSystem != null && colorPresets != null)
        {
            ChangeParticleColorPresent(0);
        }
        if(skyMaterial != null && skyColors != null)
        {

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

        //changing the sky
        ChangeSkyColorPresent(presetIndex);
    }
    void ChangeSkyColorPresent(int colorIndex)
    {
        if (skyMaterial != null && colorIndex >= 0 && colorIndex < skyColors.Length)
        {
            skyMaterial.SetColor("_Tint", skyColors[colorIndex]);
            currentColorIndex = colorIndex;
        }
    }
}
