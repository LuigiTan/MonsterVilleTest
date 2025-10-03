using UnityEngine;
using System.Collections;

public class SoilPlot : MonoBehaviour
{
    [Header("Referencias de fases")]
    public GameObject justPlanted;
    public GameObject phase1;
    public GameObject phase2;
    public GameObject phase3;

    [Header("Configuracion principal")]
    public float timePerPhase = 10f; // Tiempo para avanzar cada fase
    public int harvestValue = 5; // Dinero que da la cosecha

    private bool isPlanted = false; // Booleano para ver si ya hay algo plantado
    private int currentPhase = 0;   // 0 es nada, diponible. 1-3 es cuando hay algo
    

    private void Start()
    {
        ResetPlot();
    }

    private void ResetPlot()
    {
        justPlanted.SetActive(false);
        phase1.SetActive(false);
        phase2.SetActive(false);
        phase3.SetActive(false);
        isPlanted = false;
        currentPhase = 0;
    }

    public void TryPlant(PlayerInventory player)
    {
        if (!isPlanted && player.UseSeed())
        {
            isPlanted = true;
            StartCoroutine(GrowthCycle());
        }
    }

    private IEnumerator GrowthCycle()
    {
        // Fase Just Planted
        justPlanted.SetActive(true);

        yield return new WaitForSeconds(timePerPhase);

        // Fase 1
        justPlanted.SetActive(false);
        phase1.SetActive(true);
        currentPhase = 1;

        yield return new WaitForSeconds(timePerPhase);

        // Fase 2
        phase1.SetActive(false);
        phase2.SetActive(true);
        currentPhase = 2;

        yield return new WaitForSeconds(timePerPhase);

        // Fase 3
        phase2.SetActive(false);
        phase3.SetActive(true);
        currentPhase = 3;
    }

    public void TryHarvest(PlayerInventory player)
    {
        if (isPlanted && currentPhase == 3)
        {
            player.AddMoney(harvestValue);
            Debug.Log("Planta cosechada, +" + harvestValue + " dinero");
            ResetPlot();
        }
    }
}