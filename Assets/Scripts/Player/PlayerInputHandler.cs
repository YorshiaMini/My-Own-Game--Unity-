using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private GameInputActions inputActions;


   [SerializeField] Vector2 _moveInput;
   public Vector2 moveInput => _moveInput;


      [SerializeField] bool _jumpRequest;
   public bool jumpRequest => _jumpRequest;


    void Awake()
    {
        inputActions = new GameInputActions();
    }


    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }
    
    void Update()
    {
        _moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        // Use moveInput for player movement logic   


        _jumpRequest = inputActions.Player.Jump.triggered;

        if (_jumpRequest)
        {
            Debug.Log("Jump Request: " + _jumpRequest);  
            
        }
          
        // Use jumpRequest for player jump logic
    }
}
