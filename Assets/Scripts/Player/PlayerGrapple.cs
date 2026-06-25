using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{

    private Rigidbody rb;
    private bool isGrappling = false;
    public bool IsGrappling => isGrappling; // Propiedad pública para acceder al estado de grappling desde otras clases
    private Transform grappleTarget;
    [SerializeField] private float grappleSpeed = 10f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(!isGrappling) { return; }

        transform.position = Vector3.MoveTowards(transform.position, grappleTarget.position, grappleSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, grappleTarget.position) < 1f)
        {
            isGrappling = false;
            // Aquí puedes agregar la lógica para cuando el jugador llega al objetivo del gancho.
             Debug.Log(IsGrappling);
    
        }
    
      
    }

    public void StartGrapple(Transform target)
    {
        grappleTarget = target;
        isGrappling = true;
        // Aquí puedes agregar la lógica para iniciar el gancho, como mover al jugador hacia el objetivo.
    }

}
