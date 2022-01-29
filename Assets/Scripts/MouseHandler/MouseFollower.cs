using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [FormerlySerializedAs("camera")] [SerializeField] private Camera cameraMain;
    [SerializeField] private float offset;
    
    
    private PlayerInputHandler input;

    private Vector3 mousePosition;

    private void Start()
    {
        input = LevelData.CurrentLevelData.InputHandler;
    }

    void Update()
    {
        Vector3 viewportMousePos = input.MousePosition;

        if (viewportMousePos.x < 0)
        {
            viewportMousePos.x = 0 + offset;
        }
        
        if (viewportMousePos.x > Screen.width)
        {
            viewportMousePos.x = Screen.width - offset;
        }
        
        if (viewportMousePos.y < 0)
        {
            viewportMousePos.y = 0 + offset;
        }
        
        if (viewportMousePos.y > Screen.height)
        {
            viewportMousePos.y = Screen.height - offset;
        }
        
        mousePosition = cameraMain.ScreenToWorldPoint(viewportMousePos);

        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}
