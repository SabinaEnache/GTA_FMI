using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    public float damageAmount = 20f; // Damage-ul aplicat
    private bool canDealDamage = false; // Controlează dacă pumnul poate aplica damage

    void OnTriggerEnter(Collider other)
    {
        if (canDealDamage)
        {
            NPCController npc = other.GetComponent<NPCController>();
            if (npc != null)
            {
                npc.TakeDamage(damageAmount);
            }
        }
    }

    public void EnableDamage()
    {
        canDealDamage = true;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
    }
}
