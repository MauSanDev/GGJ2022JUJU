using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovementTest : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float playerSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        bool isWalking = !Mathf.Approximately(xAxis, 0) || !Mathf.Approximately(yAxis, 0);

        Debug.Log(isWalking);
        playerAnimator.SetBool("IsWalking", isWalking);
        
        float xMov = xAxis * Time.deltaTime * playerSpeed;
        float yMov = yAxis * Time.deltaTime * playerSpeed;
        transform.Translate(new Vector3(xMov, yMov));
    }
}
