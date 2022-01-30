using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextBehaviour : MonoBehaviour
{
    public static ShowTextBehaviour Instance = null;
    
    [SerializeField] private Text text;
    [SerializeField] private float timeToWaitBeforeShowing = 1;
    [SerializeField] private float timeToWaitShowing = 5;
    [SerializeField] private Animation animationJUJU;

    private Vector3 startPos;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        string tutorialText = LevelData.CurrentLevelData.TutorialText;
        ShowText(tutorialText);
    }

    private Coroutine textRoutine = null;
    
    public void ShowText(string textToShow)
    {
        if (textRoutine != null)
        {
            StopCoroutine(textRoutine);
        }
        
        startPos = transform.position;
        if (!string.IsNullOrEmpty(textToShow))
        {
            textRoutine = StartCoroutine(ShowTextRoutine(textToShow));
        }
    }

    private IEnumerator ShowTextRoutine(string tutorialText)
    {
        text.text = tutorialText;
        yield return new WaitForSeconds(timeToWaitBeforeShowing);

        animationJUJU.Play();
        
        yield return new WaitForSeconds(timeToWaitShowing);
        textRoutine = null;
    }
}
