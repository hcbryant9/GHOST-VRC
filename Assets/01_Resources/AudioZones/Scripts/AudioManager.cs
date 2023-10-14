using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDK3.Data;

enum AudioState
{
    Idle,
    FadingOut,
    FadingIn
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(VRCSpatialAudioSource))]
public class AudioManager : UdonSharpBehaviour
{
    public AudioClip defaultTrack;
    public float defaultVolume = 0.5f;
    public float transitionTime = 2.0f;

    [HideInInspector] public AudioSource audioSource;
    private bool isFading = false;

    [SerializeField]
    private DataList clipQueue;

    private float currentClipVolume;
    private float nextClipVolume;

    private float fadeTimer = 0f;

    private AudioState currentState = AudioState.Idle;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = defaultTrack;
        audioSource.volume = defaultVolume;
        audioSource.Play();
    }

    public void StartAudioTransition(AudioClip newTrack, float volume)
    {
        if (clipQueue.Contains(newTrack)) return;
        clipQueue.Add(newTrack);
        currentClipVolume = audioSource.volume;
        nextClipVolume = volume;

        if (!isFading)
        {
            isFading = true;
            currentState = AudioState.FadingOut;
            fadeTimer = 0f;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case AudioState.FadingOut:
                audioSource.volume = Mathf.Lerp(currentClipVolume, 0f, fadeTimer / transitionTime);
                fadeTimer += Time.deltaTime;
                if (fadeTimer >= transitionTime)
                {
                    audioSource.Stop();
                    currentState = AudioState.FadingIn;
                    fadeTimer = 0f;

                    // Set and play the next clip
                    if (clipQueue.Count > 0)
                    {
                        AudioClip nextClip = (AudioClip)clipQueue[0].Reference;
                        audioSource.clip = nextClip;
                        clipQueue.RemoveAt(0);
                        audioSource.Play();
                    }
                }
                break;

            case AudioState.FadingIn:
                audioSource.volume = Mathf.Lerp(0f, nextClipVolume, fadeTimer / transitionTime);
                fadeTimer += Time.deltaTime;
                if (fadeTimer >= transitionTime)
                {
                    audioSource.volume = nextClipVolume;
                    isFading = false;
                    currentState = AudioState.Idle;

                    // Check if there are more tracks in the queue to start fading out again
                    if (clipQueue.Count > 0)
                    {
                        fadeTimer = 0f;
                        currentState = AudioState.FadingOut;
                    }
                }
                break;

            default:
                // The default state (AudioState.Idle) does nothing.
                break;
        }
    }
}