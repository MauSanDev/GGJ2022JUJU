using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoop : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPos;
    
    private void Awake()
    {
        player.transform.position = playerStartPos.position;
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentLevel();
        }
    }

    private void OnLevelComplete(object[] parametercontainer)
    {
        SceneManager.LoadScene(nextLevelName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 1)
        {
            AudioManager.Instance.PlaySound("DoorUnlocked");
        }

        if (scene.name.Equals("Level3"))
        {
            AudioManager.Instance.PlaySound("GhostWhispers", true);
        }
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
