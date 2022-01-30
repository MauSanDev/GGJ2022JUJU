using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] private float animationTime = 14f;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (timer >= animationTime)
        {
            SceneManager.LoadScene("Menu");
        }
        
        timer += Time.deltaTime;
    }
}
