using UnityEngine;

public class CenterOfMassVisualizer : MonoBehaviour
{
    public Rigidbody rb; // Referință către Rigidbody
    public Color gizmoColor = Color.red; // Culoarea Gizmo-ului
    public float gizmoSize = 0.2f; // Dimensiunea Gizmo-ului

    void OnDrawGizmos()
    {
        if (rb != null)
        {
            // Setează culoarea Gizmo-ului
            Gizmos.color = gizmoColor;
            
            // Desenează un sferă mică la poziția centrului de masă
            Gizmos.DrawSphere(rb.worldCenterOfMass, gizmoSize);
        }
    }
}