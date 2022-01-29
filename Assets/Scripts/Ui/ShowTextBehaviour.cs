using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextBehaviour : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float timeToWaitBeforeShowing = 1;
    [SerializeField] private float timeToWaitShowing = 5;
    [SerializeField] private Animation animationJUJU;

    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        string tutorialText = LevelData.CurrentLevelData.TutorialText;
        
        if (!string.IsNullOrEmpty(tutorialText))
        {
            StartCoroutine(ShowTextRoutine(tutorialText));
        }
    }

    private IEnumerator ShowTextRoutine(string tutorialText)
    {
        text.text = tutorialText;
        yield return new WaitForSeconds(timeToWaitBeforeShowing);

        animationJUJU.Play();
        
        yield return new WaitForSeconds(timeToWaitShowing);
    }
}
