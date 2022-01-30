using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image monsterImage;
    [SerializeField] private GameObject container;
    [SerializeField] private float transitionTime = 3f;
    
    public static LoseScreenHandler Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void ShowLooseScreen(Sprite imageToShow = null)
    {
        if (imageToShow != null)
        {
            monsterImage.sprite = imageToShow;
        }

        StartCoroutine(LooseRoutine());
    }

    private IEnumerator LooseRoutine()
    {
        container.SetActive(true);
        animator.SetTrigger("Lose");
        yield return new WaitForSeconds(transitionTime);
        container.SetActive(false);
        EventsManager.DispatchEvent(EvenManagerConstants.RESET_LEVEL);
    }
}
