using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    [SerializeField] Animator animator;
    PlayerMovement playerMovement;
    Rigidbody rb;

    private bool wasGroundedLastFrame = true; // Variable para rastrear el estado de grounded en el frame anterior


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

        void Update()
    {
        animator.SetBool("IsGrounded", playerMovement.IsGrounded);
        animator.SetFloat("MoveSpeed", playerMovement.CurrentSpeed);

        if(wasGroundedLastFrame && !playerMovement.IsGrounded && rb.linearVelocity.y > 0.1f)
        {
            animator.SetTrigger("Jump");
        }

        wasGroundedLastFrame = playerMovement.IsGrounded;

     /*   Debug.Log(
    "Speed: " + playerMovement.CurrentSpeed +
    " | VelocityY: " + rb.linearVelocity.y +
    " | Grounded: " + playerMovement.IsGrounded
                    );*/


    }
}
