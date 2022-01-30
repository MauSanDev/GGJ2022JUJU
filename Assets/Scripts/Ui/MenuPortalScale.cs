
using System.Collections;
using UnityEngine;

public class MenuPortalScale : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    private Vector3 startScale;
    private void Start()
    { 
        startScale = transform.localScale;
        StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        transform.localScale = Vector3.zero;
        transform.position = startPos;
        AudioManager.Instance.PlaySound("Flame");
        iTween.ScaleTo(gameObject, startScale, 1.5f);
    }
}
