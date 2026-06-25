using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleTargeting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private PlayerTimeFreeze playerTimeFreeze;
    private PlayerInputHandler playerInputHandler;
    private PlayerGrapple playerGrapple;

    [SerializeField] private LayerMask grappleTargetLayerMask;
    [SerializeField] private float raycastDistance  = 100f;
    private Transform currentTarget; // la que se está apuntando actualmente
    private Transform selectedTarget; // la que se ha seleccionado para el gancho
   [SerializeField] private float grappleRange = 10f;
    bool isGrappleTargetInRange;

    private float currentTargetDistance;

    


    void Start()
    {
        playerTimeFreeze = GetComponent<PlayerTimeFreeze>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerGrapple = GetComponent<PlayerGrapple>();
        isGrappleTargetInRange = false;

    }

    
    void Update()
    {
        if (!playerTimeFreeze.IsFrozen){return;}


        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,raycastDistance, grappleTargetLayerMask)) 
        {
            currentTarget = hit.transform;
        } else
        {
            currentTarget = null;
        }

        if(currentTarget != null)
        {
            currentTargetDistance = Vector3.Distance(transform.position, currentTarget.position);

            isGrappleTargetInRange = currentTargetDistance <= grappleRange;

            if (playerInputHandler.GrapplePressed && isGrappleTargetInRange)
            {
                selectedTarget = currentTarget;
                playerGrapple.StartGrapple(selectedTarget);
                playerTimeFreeze.DeactivateTimeFreeze(); // Desactiva la congelación del tiempo al iniciar el gancho
            }
        }
        else
        {
            isGrappleTargetInRange = false;
        }
    }



}
