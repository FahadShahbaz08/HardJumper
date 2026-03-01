using UnityEngine;

public class HighJumpPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.ActivateHighJump();
            Destroy(gameObject);
        }
    }
}