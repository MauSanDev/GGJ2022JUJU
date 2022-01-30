using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private string soundToPlay = "";

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(soundToPlay);
    }
}
