using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{

    private Rigidbody rb;
    private bool isGrappling = false;
    public bool IsGrappling => isGrappling; // Propiedad pública para acceder al estado de grappling desde otras clases
    private Transform grappleTarget;
    [SerializeField] private float grappleSpeed = 12f;
    

    //Cuerda    
    [SerializeField] private LineRenderer lineRenderer;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = false;
    }


    void Update()
    {
        if(!isGrappling) { return; }

        lineRenderer.SetPosition(0,transform.position + new Vector3(0,1,0));
        lineRenderer.SetPosition(1,grappleTarget.position);

        transform.position = Vector3.MoveTowards(transform.position, grappleTarget.position, grappleSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, grappleTarget.position) < 2f)
        {
            isGrappling = false;
            lineRenderer.enabled = false;
            // Aquí puedes agregar la lógica para cuando el jugador llega al objetivo del gancho.
        }
    
      
    }

    public void StartGrapple(Transform target)
    {
        grappleTarget = target;
        isGrappling = true;
        lineRenderer.enabled = true;
        Debug.Log(lineRenderer.enabled);
        // Aquí puedes agregar la lógica para iniciar el gancho, como mover al jugador hacia el objetivo.
    }

}
