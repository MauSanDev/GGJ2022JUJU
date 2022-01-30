using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Image turnOn;
    
    public void OnPointerEnter()
    {
        turnOn.gameObject.SetActive(true);
    }
    
    public void OnPointerExit()
    {
        turnOn.gameObject.SetActive(false);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
