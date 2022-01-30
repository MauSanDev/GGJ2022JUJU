using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ScriptableLevelData : ScriptableObject
{
    public float lanternStartAmount;
    public string tutorialText = "";
    public float delayToShow = 1f;
    public LevelGoals levelGoals;
    
    [MenuItem("Assets/Create/LevelDataConfig")]
    public static void CreateMyAsset()
    {
        ScriptableLevelData asset = ScriptableObject.CreateInstance<ScriptableLevelData>();

        AssetDatabase.CreateAsset(asset, "Assets/Scriptable/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}


public class LevelGoals
{
    public int keysAmount = 0;
    public int presurePlates = 0;
    public int lightsOn = 0;
    public List<int> patternId = new List<int>();
}
