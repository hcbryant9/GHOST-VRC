using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleColorChanger : UdonSharpBehaviour
{
    // For the particles
    public ParticleSystem particleSystem;
    public Gradient[] colorPresets;
    public float colorChangeDuration = 2.0f; // Adjust the duration as needed

    // For the sky
    public Color[] skyColors;
    public Material skyMaterial;
    private int currentColorIndex = 0;
    private Color currentSkyColor;

    private bool isTransitioning = false;
    private float transitionStartTime;
    private int targetColorIndex;

    void Start()
    {
        if (particleSystem != null && colorPresets != null)
        {
            ChangeParticleColorPresent(0);
        }
        if (skyMaterial != null && skyColors != null)
        {
            ChangeSkyColorPresent(0);
        }
    }

    public void ChangeParticleColorPresent(int presetIndex)
    {
        if (presetIndex < 0 || presetIndex >= colorPresets.Length)
        {
            Debug.LogWarning("Invalid preset index");
            return;
        }
        //stopping,clearing
        particleSystem.Stop();
        particleSystem.Clear();

       
        // Access the ColorOverLifetimeModule of the Particle System
        var colorOverLifetime = particleSystem.colorOverLifetime;

        // Apply the chosen color preset
        colorOverLifetime.color = colorPresets[presetIndex];

        //changing the sky
        ChangeSkyColorPresent(presetIndex);

        //okay, we can play again :)
        particleSystem.Play();
    }

    void ChangeSkyColorPresent(int colorIndex)
    {
        if (skyMaterial != null && colorIndex >= 0 && colorIndex < skyColors.Length)
        {
            // Store the current sky color and smoothly transition to the new color
            currentSkyColor = skyMaterial.GetColor("_Tint");
            targetColorIndex = colorIndex;
            StartColorTransition(targetColorIndex);

            //stopping particle system and emitting a new color
            
            //ChangeParticleColorPresent(colorIndex);
        }
    }

    void Update()
    {
        if (isTransitioning)
        {
            float t = (Time.time - transitionStartTime) / colorChangeDuration;

            // Lerp between the current and target color
            Color lerpedColor = Color.Lerp(currentSkyColor, skyColors[targetColorIndex], t);

            // Apply the lerped color to the sky material
            skyMaterial.SetColor("_Tint", lerpedColor);

            if (t >= 1.0f)
            {
                isTransitioning = false;
            }
        }
    }

    void StartColorTransition(int target)
    {
        Color targetColor = skyColors[target];
        currentSkyColor = skyMaterial.GetColor("_Tint");
        isTransitioning = true;
        transitionStartTime = Time.time;
    }

    
}
