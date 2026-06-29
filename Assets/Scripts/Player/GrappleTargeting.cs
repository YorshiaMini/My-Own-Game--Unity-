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

    private Transform previousTarget; // la que estaba apuntando anteriormente
    private Transform currentTarget; // la que se está apuntando actualmente
    private Transform selectedTarget; // la que se ha seleccionado para el gancho
   [SerializeField] private float grappleRange = 10f;
    bool isGrappleTargetInRange;

    private float currentTargetDistance;

    private GrapplePoint currentGrapplePoint;


    


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
            currentGrapplePoint = currentTarget.GetComponent<GrapplePoint>();
        } else
        {
            currentTarget = null;
            currentGrapplePoint = null; //Limpiar la referencia al GrapplePoint cuando no hay objetivo
        }

        if(currentTarget != null)
        {

            currentTargetDistance = Vector3.Distance(transform.position, currentTarget.position);

            isGrappleTargetInRange = currentTargetDistance <= grappleRange;


            if(currentTarget != previousTarget)
            {
                if(previousTarget != null) 
                {
                    previousTarget.GetComponent<GrapplePoint>().SetNormal();
                }
                
                
                if(isGrappleTargetInRange)
                {
                    currentGrapplePoint.SetValid();
                }
                else
                {
                    currentGrapplePoint.SetInvalid();
                }

                previousTarget = currentTarget;   
            }


            

            if (playerInputHandler.GrapplePressed && isGrappleTargetInRange)
            {
                selectedTarget = currentTarget;
                playerGrapple.StartGrapple(selectedTarget);
                playerTimeFreeze.DeactivateTimeFreeze(); // Desactiva la congelación del tiempo al iniciar el gancho
            }
        }
        else
        {
            if(previousTarget != null)
            {
                previousTarget.GetComponent<GrapplePoint>().SetNormal();
                previousTarget = null;
            }
            isGrappleTargetInRange = false;
        }
    }



}
