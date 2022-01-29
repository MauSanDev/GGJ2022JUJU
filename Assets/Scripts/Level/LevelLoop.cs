using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoop : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPos;
    private void Awake()
    {
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
        player.transform.position = playerStartPos.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentLevel();
        }
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
    }

    private void OnLevelComplete(object[] parametercontainer)
    {
        SceneManager.LoadScene(nextLevelName);
        AudioManager.Instance.PlaySound("DoorUnlocked");
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
