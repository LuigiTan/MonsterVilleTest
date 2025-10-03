using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInventory : MonoBehaviour
{
    public int seeds = 3; // Semillas iniciales
    public int money = 0; // Dinero inicial
    ///private MaiActions actions; //Fucking hate action maps
    private SoilPlot currentPlot;// Referencia al plot en el que estamos parados

    private SeedManager seedManager;
  

    // ---------------- INVENTARIO ----------------
    // Gastar semillas
    public bool UseSeed()
    {
        if (seeds > 0)
        {
            seeds--;
            Debug.Log("Plantaste una semilla. Semillas restantes: " + seeds);
            return true;
        }
        else
        {
            Debug.Log("No tienes semillas");
            return false;
        }
    }

    // Hablarle a esto para ganar dinero
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Dinero actual: " + money);
    }

    // ---------------- INPUT CALLBACKS ----------------

    // Llamado automáticamente por PlayerInput en modo Send Messages
    public void OnPlant(InputValue value)
    {
        Debug.Log("Se presiono E");
        // Para botones: value.isPressed será true cuando se presiona
        if (!value.isPressed) return;

        if (currentPlot != null)
            currentPlot.TryPlant(this);
    }

    public void OnHarvest(InputValue value)
    {
        Debug.Log("Se presiono Q");
        Debug.Log(value.ToString());
        if (!value.isPressed) return;

        if (currentPlot != null)
            currentPlot.TryHarvest(this);
    }


    //-------------------OLD----------------------------
    /*
    public void OnPlant(InputAction.CallbackContext context) 
    { 
        if (context.performed && currentPlot != null) 
        { 
            currentPlot.TryPlant(this); 
        } 
    }
    public void OnHarvest(InputAction.CallbackContext context)
    {
        if (context.performed && currentPlot != null)
        {
            currentPlot.TryHarvest(this);
        }
    }
    */
    // ---------------- DETECCIÓN DE TIERRA ----------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soil"))
        {
            currentPlot = other.GetComponent<SoilPlot>();
        }
        else if (other.CompareTag("Seed"))
        {
            seedManager = other.GetComponent<SeedManager>();

            seeds = seeds + seedManager.SeedAmount;

            seedManager.SeedPickedUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Soil") && currentPlot == other.GetComponent<SoilPlot>())
        {
            currentPlot = null;
        }
    }

}