using UnityEditor;
using UnityEngine;

public class ScriptableLevelData : ScriptableObject
{
    public float lanternStartAmount;
    public string tutorialText = "";
    public float delayToShow = 1f;
    
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
