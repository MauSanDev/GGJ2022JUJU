using UnityEngine;

public class DontFocusEnemyBehavior : AbstractEnemyBehavior
{
    [SerializeField] private float timeToGetFocused = 3f;
    [SerializeField] private Sprite monsterFace;
    private float focusedTime = 0;
    
    protected override void OnBehaviorStart()
    {
        focusedTime = 0f;
    }

    protected override void Behave()
    {
        if (focusedTime > timeToGetFocused)
        {
            LoseScreenHandler.Instance.ShowLooseScreen(monsterFace);
            return;
        }

        focusedTime += Time.deltaTime;
    }
}
