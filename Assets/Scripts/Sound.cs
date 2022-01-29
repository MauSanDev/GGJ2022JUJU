using UnityEngine;

[System.Serializable]
public class Sound
{
    public Transform parentTransform = null;
    public string name;
    public AudioClip clip;
    public float timeToStart = 0f;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(-3f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [Range(0f, 1f)]
    public float spatialBlend = .5f;
    
    public float minDistance = 1f;
    public float timeToFadeIn = 1f;
    public float timeToFadeOut = 1f;

    [HideInInspector]
    public AudioSource source = null;
}
