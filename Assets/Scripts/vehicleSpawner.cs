using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Necesită pentru operațiuni LINQ

public class vehicleSpawner : MonoBehaviour
{
    public carNode[] waypoints;

    public GameObject[] vehicle;
    public int totalPopulationMax = 20;
    public float maxDistance = 100f;
    public int totalAi = 0;
    public List<GameObject> spawnedAi;

    // Lista waypoint-urilor excluse
    private HashSet<int> excludedWaypoints = new HashSet<int> { 2, 3, 6, 7, 9, 10, 11, 12, 14, 15, 16,21,22,23,25,26,30,31,33,36,37,38,39,  43,44,46,47,48,51,52,53,54,55,56 };

    void Awake()
    {
        loadArray();
        StartCoroutine(spawner());
        managePopulation();
    }

    void loadArray()
    {
        // Obține toate waypoint-urile
        carNode[] allWaypoints = FindObjectsOfType<carNode>();

        // Filtrează waypoint-urile excluse
        waypoints = allWaypoints
            .Where((waypoint, index) => !excludedWaypoints.Contains(index))
            .ToArray();
    }

    void spawnPrefab(int index)
    {
        GameObject i = Instantiate(vehicle[Random.Range(0, vehicle.Length)]);
        i.GetComponent<vehicleAiController>().currentNode = waypoints[index].GetComponent<carNode>();
        i.transform.position = waypoints[index].transform.position;
        i.transform.Rotate(0, waypoints[index].transform.eulerAngles.y, 0);
        spawnedAi.Add(i);
    }

    public IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            managePopulation();
        }
    }

    void managePopulation()
    {
        for (int i = 0; i < waypoints.Length; i += 3)
        {
            if (spawnedAi.Count <= totalPopulationMax)
                populationLoop(i);
        }

        for (int i = 0; i < spawnedAi.Count; i++)
        {
            if (Vector3.Distance(transform.position, spawnedAi[i].transform.position) >= maxDistance)
            {
                Destroy(spawnedAi[i]);
                spawnedAi.Remove(spawnedAi[i]);
            }
        }
        totalAi = spawnedAi.Count;
    }

    void populationLoop(int index)
    {
        if (Vector3.Distance(transform.position, waypoints[index].transform.position) <= maxDistance)
        {
            spawnPrefab(index);
        }
    }
}
