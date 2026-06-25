using UnityEngine;
using UnityEngine.UI;

public class FreezeOverlayController : MonoBehaviour
{
    
    private PlayerTimeFreeze playerTimeFreeze;
    [SerializeField] private Image freezeOverlay;
    [SerializeField] private Image freezeBarFill;
    [SerializeField] private Image freezeBarBackground;




    float normalAlpha = 0f;
    float freezeAlpha = 0.1f;
    float fadeSpeed = 2f;
    float targetAlpha;
  

    Color currentColor;


 
    void Awake()
    {
        playerTimeFreeze = GetComponent<PlayerTimeFreeze>();
        targetAlpha = normalAlpha;
        freezeBarBackground.gameObject.SetActive(false);

    
    }

  
    void Update()
    {
        if (playerTimeFreeze.IsFrozen)
        {
            targetAlpha = freezeAlpha;
            freezeBarFill.fillAmount = playerTimeFreeze.FreezeCounter / playerTimeFreeze.FreezeDuration;    
            freezeBarBackground.gameObject.SetActive(true);
        }
        else
        {
            targetAlpha = normalAlpha;
            freezeBarBackground.gameObject.SetActive(false);
        
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
