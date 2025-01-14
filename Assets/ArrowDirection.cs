using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public GameObject a; // Obiectul țintă care are Box Collider
    public Transform target; // Destinația spre care săgeata trebuie să arate
    public Transform player; // Player-ul, pentru a poziționa săgeata deasupra lui

    void Update()
    {
        if (target != null)
        {
            // Calculăm direcția dintre player și destinație
            Vector3 direction = (target.position - player.position).normalized;

            // Setăm rotația săgeții cu compensare
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y + 90, 0); // Offset pe Y
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificăm dacă săgeata a intrat în coliziune cu obiectul a
        if (other.gameObject == a)
        {
            // Dezactivăm săgeata
            gameObject.SetActive(false);
        }
    }
}