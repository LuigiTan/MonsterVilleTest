using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform player;
    public Vector3 Offset;
    private void Update()
    {
        transform.position = player.position + Offset;
    }
}
