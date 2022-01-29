using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private ScriptableLevelData rawLevelData;
    [SerializeField] private PlayerInputHandler input;

    private static LevelData instance;

    private void Awake()
    {
        instance = this;
    }


    public static LevelData CurrentLevelData => instance;
    
    
    public int LanternStartAmount => rawLevelData.lanternStartAmount;
    public PlayerInputHandler InputHandler => input;
}
