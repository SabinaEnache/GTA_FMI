using UnityEngine;

[ExecuteInEditMode]
public class waypoint : MonoBehaviour
{
    [Header("Waypoint Settings")]
    public waypoint previousWaypont; // Referință către waypoint-ul anterior
    public waypoint nextWaypoint;     // Referință către waypoint-ul următor

    [Range(0f, 10f)] // Slider pentru reglarea lățimii
    public float width = 1f;          // Lățimea waypoint-ului (modificabilă în inspector)

    private void OnDrawGizmos()
    {
        // Desenează un mic punct pentru acest waypoint
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

        // Desenează linii către previous și next waypoint-uri, dacă sunt setate
        if (previousWaypont != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, previousWaypont.transform.position);
        }

        if (nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
        }
    }
}