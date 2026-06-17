using UnityEngine;
using Cinemachine;
public class PlayerCameraController : MonoBehaviour
{
    
    float normalFov = 45f;
    float freezeFov = 85f;
    float targetFov;

    float fovTransitionSpeed = 2f;

    [SerializeField]  CinemachineVirtualCamera myCamera;

    private PlayerTimeFreeze timeFreeze;



    void Awake()
    {
        timeFreeze = GetComponent<PlayerTimeFreeze >();
	    targetFov = normalFov;

    }

    
    void Update()
    {
        if (timeFreeze.IsFrozen)
        {
            targetFov = freezeFov;
        }
        else
        {
            targetFov = normalFov;
        }
        CambioCamara();
    }

    void CambioCamara()
    {
        myCamera.m_Lens.FieldOfView = Mathf.Lerp
        (myCamera.m_Lens.FieldOfView, targetFov, fovTransitionSpeed * Time.unscaledDeltaTime);
    }

}
