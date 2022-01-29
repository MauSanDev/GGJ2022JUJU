using System.Collections.Generic;
using UnityEngine;

public class MultidimensionalAnimationHandler : AbstractPlayerInputListener
{
    private static int ANIM_IS_WALKING = Animator.StringToHash("IsWalking");
    private static int ANIM_IS_RUNNING = Animator.StringToHash("IsRunning");

    [SerializeField] private MoveAxis initialFacingAxis = MoveAxis.Down;
    [SerializeField] private Animator leftObject = null;
    [SerializeField] private Animator rightObject = null;
    [SerializeField] private Animator topObject = null;
    [SerializeField] private Animator downObject = null;

    private Dictionary<MoveAxis, Animator> pairedAnimators;
    private MoveAxis currentMovementAxis = MoveAxis.Down;

    private void Awake()
    {
        pairedAnimators = new Dictionary<MoveAxis, Animator>()
        {
            { MoveAxis.Left, leftObject },
            { MoveAxis.Right, rightObject },
            { MoveAxis.Top, topObject },
            { MoveAxis.Down, downObject },
        };

        currentMovementAxis = initialFacingAxis;
        TurnAnimators(currentMovementAxis);
    }

    private enum MoveAxis
    {
        Left,
        Right,
        Top,
        Down
    }
    
    public override void OnInputChanged(PlayerInputData inputData)
    {
        Vector3 movementAxis = inputData.movementAxis;
        if (!Mathf.Approximately(movementAxis.x, 0))
        {
            currentMovementAxis = movementAxis.x > 0 ? MoveAxis.Right : MoveAxis.Left;
        }
        else if (!Mathf.Approximately(movementAxis.y, 0))
        {
            currentMovementAxis = movementAxis.y > 0 ? MoveAxis.Top : MoveAxis.Down;
        }
        
        TurnAnimators(currentMovementAxis);

        pairedAnimators[currentMovementAxis].SetBool(ANIM_IS_WALKING, inputData.isMoving);
    }

    private void TurnAnimators(MoveAxis flags)
    {
        foreach (MoveAxis moveId in pairedAnimators.Keys)
        {
            pairedAnimators[moveId].gameObject.SetActive(flags == moveId);
        }
    }
}
