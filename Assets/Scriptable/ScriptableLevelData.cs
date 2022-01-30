using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class ScriptableLevelData : ScriptableObject
{
    public float lanternStartAmount;
    public string tutorialText = "";
    public float delayToShow = 1f;
    public LevelGoals levelGoals;
}


[Serializable]
public class LevelGoals
{
    public int keysAmount = 0;
    public int pressurePlates = 0;
    public int enabledPillars = 0;
    public List<int> patternId = new List<int>();
}

public class LevelGoalChecker
{
    private LevelGoals currentLevelGoals;
    private LevelGoals currentLevelProgress;

    public LevelGoalChecker(LevelGoals toAchieve)
    {
        currentLevelGoals = toAchieve;
        currentLevelProgress = new LevelGoals();
    }

    public void RegisterKey()
    {
        currentLevelProgress.keysAmount++;

        CheckIfGoalsMatches();
    }
    
    public void RegisterPillar()
    {
        currentLevelProgress.enabledPillars++;

        CheckIfGoalsMatches();
    }

    public void EnablePressurePlate(bool enable)
    {
        if (enable)
        {
            currentLevelProgress.pressurePlates++;
        }
        else
        {
            currentLevelProgress.pressurePlates--;
        }

        CheckIfGoalsMatches();
    }
    
    public bool AddToPattern(int number)
    {
        currentLevelProgress.patternId.Add(number);

        int min = Mathf.Min(currentLevelGoals.patternId.Count, currentLevelProgress.patternId.Count);
        for (int i = 0; i < min; i++)
        {
            if (currentLevelProgress.patternId[i] != currentLevelGoals.patternId[i])
            {
                currentLevelProgress.patternId.Clear();
                EventsManager.DispatchEvent(EvenManagerConstants.ON_PATTERN_BROKEN);
                return false;
            }
        }

        if (currentLevelProgress.patternId.Count == currentLevelGoals.patternId.Count)
        {
            EventsManager.DispatchEvent(EvenManagerConstants.ON_PATTERN_DONE);
        }

        CheckIfGoalsMatches();
        return true;
    }

    private void CheckIfGoalsMatches()
    {
        bool samePillars = currentLevelGoals.enabledPillars == currentLevelProgress.enabledPillars;
        bool samePressurePlates = currentLevelGoals.pressurePlates == currentLevelProgress.pressurePlates;
        bool sameKeys = currentLevelGoals.keysAmount == currentLevelProgress.keysAmount;
        bool samePattern = false;

        if (currentLevelGoals.patternId.Count == currentLevelProgress.patternId.Count)
        {
            for (int i = 0; i < currentLevelGoals.patternId.Count; i++)
            {
                if (currentLevelGoals.patternId[i] != currentLevelProgress.patternId[i])
                {
                    return;
                }
            }
        }

        samePattern = true;
        if (samePillars && samePressurePlates && sameKeys && samePattern)
        {
            EventsManager.DispatchEvent(EvenManagerConstants.ON_GOALS_ACHIEVED);
        }
        else
        {
            EventsManager.DispatchEvent(EvenManagerConstants.ON_GOALS_DISMISS);
        }
        
    }
    
}
