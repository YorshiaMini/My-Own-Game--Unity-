using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputHandler inputHandler;
    [SerializeField] private float moveSpeed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    
    void FixedUpdate()
    {

        rb.linearVelocity = new Vector3(inputHandler.moveInput.x * moveSpeed, rb.linearVelocity.y, 0);
    }
}
