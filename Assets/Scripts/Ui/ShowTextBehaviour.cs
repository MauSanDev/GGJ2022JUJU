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


    private Queue<string> textsToShow = new Queue<string>();

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
        textsToShow.Enqueue(textToShow);
        
        if (!string.IsNullOrEmpty(textToShow) && textRoutine == null)
        {
            textRoutine = StartCoroutine(ShowTextRoutine());
        }
    }

    private IEnumerator ShowTextRoutine()
    {
        while (textsToShow.Count > 0)
        {
            string textToShow = textsToShow.Dequeue();
            text.text = textToShow;
            yield return new WaitForSeconds(timeToWaitBeforeShowing);

            animationJUJU.Play();
        
            yield return new WaitForSeconds(timeToWaitShowing);
        }
        textRoutine = null;
    }
}
