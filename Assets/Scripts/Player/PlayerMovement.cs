using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private PlayerInputHandler inputHandler;
    private PlayerTimeFreeze timeFreezeHandler;
    private Rigidbody rb;


    [SerializeField] private float moveSpeed = 5f;


    //Jump variables

    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckDistance = .2f;
    [SerializeField] LayerMask _surfaceForJump;
    private bool _isGrounded;
    public bool IsGrounded => _isGrounded; // Propiedad pública para acceder al estado de grounded desde otras clases

    //Giro Player
    private float _rotationSpeed = 10.5f;
    Quaternion targetRotation;
    [SerializeField] Transform _visualModel;


    //CoyoteTime variables
    [SerializeField] private float coyoteTime = 0.2f; // Duración del coyote time en segundos
    private float coyoteTimeCounter; // Contador para el coyote time    

    public bool HasCoyoteTime => coyoteTimeCounter > 0f; 
    // Propiedad para verificar si el player aún tiene coyote time disponible


    //Speed Variable
    public float CurrentSpeed => Mathf.Abs(rb.linearVelocity.x); // Propiedad pública para acceder a la velocidad del player desde otras clases


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<PlayerInputHandler>();
        timeFreezeHandler = GetComponent<PlayerTimeFreeze>();


        _isGrounded = true; // Inicializa el estado de grounded
        coyoteTimeCounter = 0f; // Inicializa el contador del coyote time

        targetRotation = _visualModel.rotation; // Inicializa la rotación objetivo con la rotación actual del modelo visual   
    }


    void Update()
    {
        GiroPlayer();

        // Coyote Time logic
        if (_isGrounded && rb.linearVelocity.y <= 0)
        {
            coyoteTimeCounter = coyoteTime; // Reinicia el contador del coyote time cuando el player está en el suelo
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrementa el contador del coyote time cuando el player no está en el suelo
        }

        if (coyoteTimeCounter < 0f) // Asegura que el contador no sea negativo
        
        {
            coyoteTimeCounter = 0f; 
        }


    }
    
    void FixedUpdate()
    {
        if (!timeFreezeHandler.IsFrozen)
        {
            rb.linearVelocity = new Vector3(inputHandler.moveInput.x * moveSpeed, rb.linearVelocity.y, 0);
        }
        //movimiento del player

        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _surfaceForJump);

        drawRayCast(); //Dibuja el raycast para verificar si el player está tocando el suelo
        

        //controla que podamos saltar
        if (inputHandler.HasJumpBuffered && HasCoyoteTime && !timeFreezeHandler.IsFrozen)
        {  
           rb.linearVelocity = new Vector3(rb.linearVelocity.x, _jumpForce, 0); 
          
           // Aplica la fuerza de salto directamente a la velocidad vertical

            inputHandler.ConsumeJumpBuffer();
            ConsumeCoyoteTime();
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
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (inputHandler.moveInput.x < 0)
        {
            targetRotation = Quaternion.Euler(0, -90, 0);
        }

        _visualModel.rotation = Quaternion.Slerp(_visualModel.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    void ConsumeCoyoteTime()
    {
        coyoteTimeCounter = 0f; // Consume el coyote time al realizar un salto
    }

}
