using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private GameInputActions inputActions;


   [SerializeField] Vector2 _moveInput;
   public Vector2 moveInput => _moveInput;


   [SerializeField] private float jumpBufferTime = 0.1f;
     // Tiempo durante el cual el salto se puede ejecutar después de presionar el botón
    private float jumpBufferCounter; // Contador para el tiempo de buffer de salto

    public bool HasJumpBuffered => jumpBufferCounter > 0; // Propiedad para verificar si hay un salto en buffer




    void Awake()
    {
        inputActions = new GameInputActions();
    }
  
    
    void Update()
    {
        _moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        // Use moveInput for player movement logic   


        if (inputActions.Player.Jump.triggered)
        {
            
            jumpBufferCounter = jumpBufferTime; // Reinicia el contador de buffer de salto
            
        }

        jumpBufferCounter -= Time.deltaTime; // Decrementa el contador de buffer de salto

        if(jumpBufferCounter < 0)
        {
            jumpBufferCounter = 0; // Asegura que el contador no sea negativo
        }
          
        // Use jumpRequest for player jump logic
    }


    public void ConsumeJumpBuffer()
    {
        jumpBufferCounter = 0; // Consume el salto restableciendo el contador de buffer
    }

      void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
