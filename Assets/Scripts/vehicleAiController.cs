using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleAiController : MonoBehaviour
{
    private carModifier modifier;
    private WheelCollider[] wheels; // Referință la WheelColliders din carModifier

    public float totalPower;
    public float vertical, horizontal;

    private float radius = 8, distance;
    public carNode currentNode;

    private Vector3 velocity, Destination, lastPosition;

    void Start()
    {
        // Obține componenta carModifier și asigură-te că roțile sunt setate
        modifier = GetComponent<carModifier>();
        if (modifier == null || modifier.wheelColliders.Length == 0)
        {
            Debug.LogError("Nu există WheelColliders configurate pentru vehiculul AI!");
            return;
        }
        wheels = modifier.wheelColliders; // Obține referințele la WheelColliders
    }

    void FixedUpdate()
    {
        try
        {
            checkDistance();
            steerVehicle();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Eroare în FixedUpdate: {ex.Message}");
        }
    }

    void checkDistance()
    {
        if (Vector3.Distance(transform.position, currentNode.transform.position) <= 3)
        {
            reachedDestination();
        }
    }

    private void reachedDestination()
    {
        if (currentNode.nextWaypoint == null)
        {
            currentNode = currentNode.previousWaypont;
            return;
        }
        if (currentNode.previousWaypont == null)
        {
            currentNode = currentNode.nextWaypoint;
            return;
        }

        if (currentNode.link != null && Random.Range(0, 100) <= 20)
            currentNode = currentNode.link;
        else
            currentNode = currentNode.nextWaypoint;
    }

    private void steerVehicle()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 2;
        horizontal = newSteer;

        foreach (var item in wheels)
        {
            item.motorTorque = totalPower; // Aplica puterea motorului
        }

        if (horizontal > 0)
        {
            wheels[2].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheels[3].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            wheels[2].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheels[3].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        }
        else
        {
            wheels[2].steerAngle = 0;
            wheels[3].steerAngle = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (currentNode != null)
            Gizmos.DrawSphere(currentNode.transform.position, 0.5f);
    }
}
