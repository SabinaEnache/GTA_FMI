using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject carPrefab; // Prefab-ul mașinii de poliție
    public Vector3[] spawnCoordinates; // Coordonatele unde vor fi instanțiate mașinile
    public int policeGonSpawn = 0; // Variabila de control pentru spawn
    private bool hasSpawned = false; // Previne spawnarea multiplă
    public GameObject carCamera;

    void Update()
    {
        // Verifică dacă policeGonSpawn este 1 și nu s-a efectuat deja spawn-ul
        if (policeGonSpawn == 1 && !hasSpawned)
        {
            SpawnPoliceCars();
            hasSpawned = true; // Previi instanțierea multiplă
        }
    }

    void SpawnPoliceCars()
    {
        foreach (Vector3 spawnCoord in spawnCoordinates)
        {
            Vector3 spawnDirection = carCamera.transform.forward;
            Debug.Log($"Încerc să spawnez o mașină la coordonatele: {spawnCoord}");
            Quaternion spawnRotation = Quaternion.LookRotation(spawnDirection) * Quaternion.Euler(0, -90, 0);

            // Instanțiază o mașină la coordonatele specificate
            GameObject carInstance = Instantiate(carPrefab, spawnCoord, spawnRotation);

            // Verifică dacă instanțierea a fost reușită
            if (carInstance != null)
            {
                Debug.Log($"Mașina a fost instanțiată la: {spawnCoord}");

                // Setează target-ul drept acest player
                AIDriving aiDriving = carInstance.GetComponent<AIDriving>();
                if (aiDriving != null)
                {
                    aiDriving.target = this.transform; // Setează target-ul ca fiind player-ul
                    Debug.Log("Target-ul a fost setat către player.");
                }
                else
                {
                    Debug.LogWarning("Nu s-a găsit componenta AIDriving pe prefab!");
                }
            }
            else
            {
                Debug.LogError("Instanțierea a eșuat!");
            }
        }
    }

}