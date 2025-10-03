using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public int SeedAmount = 1;


    public void SeedPickedUp()
    {
        Destroy(gameObject);
    }
}

