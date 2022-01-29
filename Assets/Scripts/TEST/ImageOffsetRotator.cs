using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageOffsetRotator : MonoBehaviour
{
    private float offsetSpeed = 2f;
    private RawImage RawImage = null;
    
    private void Awake()
    {
        RawImage = GetComponent<RawImage>();
    }


    // Update is called once per frame
    void Update()
    {
        Rect uvRect = RawImage.uvRect;
        float currentX = uvRect.x;
        float newX = Mathf.Repeat(currentX + offsetSpeed * Time.deltaTime, 1);
        RawImage.uvRect = new Rect(newX, uvRect.y, uvRect.width, uvRect.height);
    }
}
