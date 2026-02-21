using UnityEngine;

public class MagnetPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().SendMessage("ActivateMagnet");
            Destroy(gameObject);
        }
    }
}