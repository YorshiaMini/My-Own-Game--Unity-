using UnityEngine;
using UnityEngine.UI;

public class FreezeOverlayController : MonoBehaviour
{
    
    private PlayerTimeFreeze playerTimeFreeze;
    [SerializeField] private Image freezeOverlay;



    float normalAlpha = 0f;
    float freezeAlpha = 0.1f;
    float fadeSpeed = 2f;
    float targetAlpha;
  

    Color currentColor;



    void Awake()
    {
        playerTimeFreeze = GetComponent<PlayerTimeFreeze>();
        targetAlpha = normalAlpha;
    }

  
    void Update()
    {
        if (playerTimeFreeze.IsFrozen)
        {
            targetAlpha = freezeAlpha;
        }
        else
        {
            targetAlpha = normalAlpha;
        }

        CambioOverlay();
    }


    void CambioOverlay()
    {
        currentColor = freezeOverlay.color;
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeed * Time.unscaledDeltaTime);
        freezeOverlay.color = currentColor;
    }
    
    
}
