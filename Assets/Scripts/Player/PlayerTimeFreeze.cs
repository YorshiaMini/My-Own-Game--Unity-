using UnityEngine;

public class PlayerTimeFreeze : MonoBehaviour
{

    private PlayerInputHandler inputHandler;


    [SerializeField] float freezeDuration = 4f;
    [SerializeField] float freezeCooldown = 6f;

    private float freezeCounter;
    private float cooldownCounter; //-=Time.unscaledDeltaTime; // Contador para el tiempo de enfriamiento

    private bool isFrozen;
    private bool canFreeze = true;


    
    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();

        //iniciamos los contadores y estados
        isFrozen = false;
        canFreeze = true;
        freezeCounter = 0f;
        cooldownCounter = 0f;
    }

    
    void Update()
    {
        if (inputHandler.FreezePressed && canFreeze)
        {
            ActivateTimeFreeze();
            freezeCounter -= Time.unscaledDeltaTime; // Inicia el contador de duración de congelación
        }
    }


    void ActivateTimeFreeze()
    {
        isFrozen = true;
        canFreeze = false;
        freezeCounter = freezeDuration;
        Time.timeScale = 0.1f; // Ralentiza el tiempo
    }

    void DeactivateTimeFreeze()
    {
        isFrozen = false;
        cooldownCounter = freezeCooldown; // Inicia el contador de enfriamiento
        Time.timeScale = 1f; // Restaura el tiempo normal
    }
}
