using System.Collections;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    
    private AudioSource[] Sources => GetComponents<AudioSource>();

    public static AudioManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InstantiateAudioSources();
        PlaySound("SoftCreepyWind");
        PlaySound("MusicPattern_2");
    }

    private void InstantiateAudioSources()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = sound.parentTransform != null ? sound.parentTransform.gameObject.AddComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.minDistance = sound.minDistance;
        }
    }

    public void PlaySound(string name, bool fadeIn = false)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name.Equals(name));

        if (sound == null)
        {
            Debug.LogError($"Sound {name} not found.");
            return;
        }

        if (sound.timeToStart > 0)
        {
            sound.source.PlayDelayed(sound.timeToStart);
        }
        else
        {
            sound.source.Play();
        }

        if (fadeIn)
        {
            StartCoroutine(FadeIn(sound));
        }
    }

    public void StopSound(string name, bool fadeOut = false)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name.Equals(name));
        
        if (sound == null)
        {
            Debug.LogError($"Sound {name} not found.");
            return;
        }

        if (fadeOut)
        {
            StartCoroutine(FadeOut(sound));
        }
        else
        {
            sound.source.Stop();
        }
    }

    private IEnumerator FadeIn(Sound sound)
    {
        float time = 0f;
        AudioSource source = Sources.FirstOrDefault(x => x.clip.Equals(sound.clip));

        while (time <= sound.timeToFadeIn)
        {
            time += .1f;
            source.volume = Mathf.Lerp(0, sound.volume, time / sound.timeToFadeIn);
            
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator FadeOut(Sound sound)
    {
        float time = 0f;
        AudioSource source = Sources.FirstOrDefault(x => x.clip.Equals(sound.clip));
        
        while (time <= sound.timeToFadeOut)
        {
            time += .1f;
            source.volume = Mathf.Lerp(sound.volume, 0, time / sound.timeToFadeOut);

            yield return new WaitForSeconds(.1f);
        }
        
        sound.source.Stop();
    }
}
