using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.ActivateShield(); // DIRECT CALL
            Destroy(gameObject);
        }
    }
}