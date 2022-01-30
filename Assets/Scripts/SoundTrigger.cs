using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private string[] soundsToPlay;

    public void PlaySound()
    {
        foreach (string sound in soundsToPlay)
        {
            AudioManager.Instance.PlaySound(sound);    
        }
    }
}
