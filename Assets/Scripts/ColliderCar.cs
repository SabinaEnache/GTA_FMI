using UnityEngine;

public class ColliderCar : MonoBehaviour
{
    void Start()
    {
        // Adaugă un Box Collider dacă nu există deja
        if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        // Activează Box Collider-ul
        GetComponent<BoxCollider>().enabled = true;
    }
}
