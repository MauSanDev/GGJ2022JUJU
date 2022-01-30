using UnityEngine;

public class PilarLightInteraction : AbstractLightInteraction
{
    public virtual void OnLightStart()
    {
        ShowTextBehaviour.Instance.ShowText("You activated a Pilar");
        LevelData.CurrentLevelData.CurrentGoals.lightsOn++;
        AudioManager.Instance.PlaySound("TorchOn");
    }
}
