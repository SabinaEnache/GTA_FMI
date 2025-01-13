using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        NPCController npc = other.GetComponent<NPCController>();
        if (npc != null)
        {
            npc.TakeDamage(damageAmount);
        }
    }
}
