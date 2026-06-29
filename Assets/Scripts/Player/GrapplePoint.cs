using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }


    public void SetNormal()
    {
        Debug.Log("SetNormal");
        meshRenderer.material.color = normalColor;
    }

    public void SetValid()
    {
        Debug.Log("SetValid");
        meshRenderer.material.color = validColor;
    }

    public void SetInvalid()
    {
        Debug.Log("SetInvalid");
        meshRenderer.material.color = invalidColor;
    }

    public void SetSelected()
    {
        Debug.Log("SetSelected");
        meshRenderer.material.color = Color.blue;
    }
}
