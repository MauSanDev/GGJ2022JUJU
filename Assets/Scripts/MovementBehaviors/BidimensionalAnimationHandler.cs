using UnityEngine;

public class BidimensionalAnimationHandler : AbstractMovementListener
{
    private static int ANIM_IS_MOVING = Animator.StringToHash("IsMoving");

    [SerializeField] private Animator animator = null;
    [SerializeField] private bool invertAxis = false;

    private bool isMoving = false;
    private float originalX;
    private float invertedX;

    private void Awake()
    {
        originalX = invertAxis ? -transform.localScale.x : transform.localScale.x;
        invertedX = -originalX;
    }

    public override void OnMovementChanged(Vector3 movementAxis)
    {
        isMoving = !Mathf.Approximately(movementAxis.x, 0) || !Mathf.Approximately(movementAxis.x, 0);
        animator.SetBool(ANIM_IS_MOVING, isMoving);
        
        transform.localScale = new Vector3(movementAxis.x > 0 ? originalX : invertedX, transform.localScale.y, transform.localScale.z);
    }

}
