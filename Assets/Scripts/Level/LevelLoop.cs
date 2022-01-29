using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoop : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private void Awake()
    {
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_LEVEL_COMPLETE, OnLevelComplete);
    }

    private void OnLevelComplete(object[] parametercontainer)
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
