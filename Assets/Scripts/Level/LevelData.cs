
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static int NotesCollected = 0;
    
    [SerializeField] private ScriptableLevelData rawLevelData;
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private LightHandler lightHandler;

    private static LevelData instance;

    private LevelGoals currentGoals = new LevelGoals();
    private void Awake()
    {
        instance = this;
    }
    
    public static LevelData CurrentLevelData => instance;

    public LevelGoals LevelGoals => rawLevelData.levelGoals;
    public LevelGoals CurrentGoals => currentGoals;
    
    public float LanternStartAmount => rawLevelData.lanternStartAmount;
    public PlayerInputHandler InputHandler => input;
    public LightHandler LightHandler => lightHandler;
    public string TutorialText => rawLevelData.tutorialText;
}
