using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5f; // Viteza de mișcare

    void FixedUpdate()
    {
        // Obține input-ul de la utilizator pentru direcțiile față/spate și stânga/dreapta
        float moveForward = Input.GetAxis("Vertical");   // Față/Spate (W/S sau ↑/↓)
        float moveSideways = Input.GetAxis("Horizontal"); // Stânga/Dreapta (A/D sau ←/→)

        // Creează un vector de mișcare pe baza input-ului
        Vector3 movement = transform.forward * moveForward + transform.right * moveSideways;

        // Aplică mișcarea obiectului
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
