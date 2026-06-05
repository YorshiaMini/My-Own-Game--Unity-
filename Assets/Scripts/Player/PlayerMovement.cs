using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private PlayerInputHandler inputHandler;
    private Rigidbody rb;


    [SerializeField] private float moveSpeed = 5f;


    //Jump variables

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckDistance = .2f;
    [SerializeField] LayerMask _surfaceForJump;
    bool _isGrounded;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<PlayerInputHandler>();
        _isGrounded = true; // Inicializa el estado de grounded
    }

    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(inputHandler.moveInput.x * moveSpeed, rb.linearVelocity.y, 0);
        //movimiento del player

        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _surfaceForJump);

        Debug.Log("Is Grounded: " + _isGrounded); // Debug para verificar si el player está tocando el suelo
        drawRayCast();
        

        //revisa si el player esta tocando el suelo
        if (_isGrounded && inputHandler.jumpRequest)
        {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            Debug.Log("Player Jumped with force"); 
            inputHandler.ConsumeJumpRequest();
            //salto del player 
        }
     
    }

    void drawRayCast()
    {
        Debug.DrawRay(_groundCheck.position, Vector3.down * _groundCheckDistance, Color.yellow);
    }


}
