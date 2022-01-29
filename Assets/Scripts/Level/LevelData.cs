using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private ScriptableLevelData rawLevelData;
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private LightHandler lightHandler;

    private static LevelData instance;

    private void Awake()
    {
        instance = this;
    }


    public static LevelData CurrentLevelData => instance;
    
    
    public float LanternStartAmount => rawLevelData.lanternStartAmount;
    public PlayerInputHandler InputHandler => input;
    public LightHandler LightHandler => lightHandler;
    public string TutorialText => rawLevelData.tutorialText;
}
