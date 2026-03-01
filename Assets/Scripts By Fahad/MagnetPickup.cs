using UnityEngine;

public class MagnetPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.ActivateMagnet(); // DIRECT CALL
            Destroy(gameObject);
        }
    }
}