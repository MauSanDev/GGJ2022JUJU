using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    
    private PlayerInputHandler _inputHandler = null;
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, _inputHandler.IsWalking);
    }
}
