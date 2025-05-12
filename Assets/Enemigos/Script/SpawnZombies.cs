using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    [SerializeField] private GameObject zombiPrefab; // Prefab del zombi
    [SerializeField] private Transform[] spawnPoints; // Puntos donde pueden aparecer los zombis
    [SerializeField] private float spawnRate = 1f; // Tiempo entre cada aparición

    private void Start()
    {
        InvokeRepeating("SpawnZombi", 2f, spawnRate); // Genera zombis cada cierto tiempo
    }

    private void SpawnZombi()
    {
        if (spawnPoints.Length == 0 || zombiPrefab == null) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Verificar si el punto de spawn está dentro de la cámara antes de generar el zombi
        if (IsVisibleFromCamera(spawnPoint.position))
        {
            Instantiate(zombiPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    private bool IsVisibleFromCamera(Vector3 position)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);
        return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;
    }

}
