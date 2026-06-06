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

    //Giro Player
    private float _rotationSpeed = 7f;
    Quaternion targetRotation;
    [SerializeField] Transform _visualModel;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<PlayerInputHandler>();
        _isGrounded = true; // Inicializa el estado de grounded

        targetRotation = _visualModel.rotation; // Inicializa la rotación objetivo con la rotación actual del modelo visual   
    }


    void Update()
    {
        GiroPlayer();
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
           rb.linearVelocity = new Vector3(rb.linearVelocity.x, _jumpForce, 0); 
           // Aplica la fuerza de salto directamente a la velocidad vertical

            Debug.Log("Player Jumped with force"); 
            inputHandler.ConsumeJumpRequest();
            //salto del player 
        }
     
    }


    void drawRayCast()
    {
        Debug.DrawRay(_groundCheck.position, Vector3.down * _groundCheckDistance, Color.yellow);
    }
    void GiroPlayer()
    {
        if (inputHandler.moveInput.x > 0)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (inputHandler.moveInput.x < 0)
        {
            targetRotation = Quaternion.Euler(0, -180, 0);
        }

        _visualModel.rotation = Quaternion.Slerp(_visualModel.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

}
