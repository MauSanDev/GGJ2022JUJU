using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoop : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPos;

    private PlayerInputHandler _playerInputHandler = null;
    
    private void Awake()
    {
        player.transform.position = playerStartPos.position;
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
        EventsManager.SubscribeToEvent(EvenManagerConstants.RESET_LEVEL, OnLevelComplete);
        SceneManager.sceneLoaded += OnSceneLoaded;
        _playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentLevel();
        }
    }
    
    private void OnLevelReset(object[] parametercontainer)
    {
        ResetCurrentLevel();
    }

    private void OnLevelComplete(object[] parametercontainer)
    {
        SceneManager.LoadScene(nextLevelName);
        _playerInputHandler.EnableInputs = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 1)
        {
            if(AudioManager.Instance != null)
                AudioManager.Instance.PlaySound("DoorUnlocked");
        }

        if (scene.name.Equals("Level3"))
        {
            if(AudioManager.Instance != null)
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
