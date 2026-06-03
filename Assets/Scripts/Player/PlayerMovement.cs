using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private PlayerInputHandler inputHandler;
    private Rigidbody rb;


    [SerializeField] private float moveSpeed = 5f;


    //Jump variables

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckDistance = 0.1f;
    [SerializeField] LayerMask _surfaceForJump;
    bool _isGrounded;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(inputHandler.moveInput.x * moveSpeed, rb.linearVelocity.y, 0);
        //movimiento del player

     
    }
}
