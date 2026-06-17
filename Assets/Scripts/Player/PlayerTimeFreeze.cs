using UnityEngine;

public class PlayerTimeFreeze : MonoBehaviour
{

    private PlayerInputHandler inputHandler;


    [SerializeField] float freezeDuration = 4f;
    [SerializeField] float freezeCooldown = 6f;

    private float freezeCounter;
    private float cooldownCounter; //-=Time.unscaledDeltaTime; // Contador para el tiempo de enfriamiento

    private bool isFrozen;
    public bool IsFrozen => isFrozen;
    private bool canFreeze = true;


    
    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();

        //iniciamos los contadores y estados
        isFrozen = false;
        canFreeze = true;
        freezeCounter = 0f;
        cooldownCounter = 0f;

        //Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (inputHandler.FreezePressed && canFreeze)
        {
            ActivateTimeFreeze();
    
        }

       
       
        if (isFrozen)
        {
            freezeCounter -= Time.unscaledDeltaTime; // Decrementa el contador de duración de congelación
            if (freezeCounter <= 0f)
            {
                DeactivateTimeFreeze();
            }
        }

       if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;

            if (cooldownCounter <= 0)
            {
                cooldownCounter = 0;
                canFreeze = true;
                Debug.Log("Time Freeze Ready");
            }
        }

       

    }


    void ActivateTimeFreeze()
    {
        isFrozen = true;
        canFreeze = false;
        freezeCounter = freezeDuration;
        Time.timeScale = 0.1f; // Ralentiza el tiempo
        Debug.Log("Time Freeze Activated");

        //Cursor

        Cursor.visible = true; // Hace visible el cursor
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        
    }

    void DeactivateTimeFreeze()
    {
        isFrozen = false;
        cooldownCounter = freezeCooldown; // Inicia el contador de enfriamiento
        Time.timeScale = 1f; // Restaura el tiempo normal
        Debug.Log("Time Freeze Deactivated");

        //Cursor
        Cursor.visible = false; // Oculta el cursor
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
    }
}
