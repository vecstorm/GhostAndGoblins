using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPoint;
    private GameObject currentCharacter;
    public static SpawnerPlayer Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    private void Start()
    {
        Debug.Log("SpawnerPlayer activado, instanciando personaje...");
        if (currentCharacter == null)
        {
            SpawnCharacter();
        }
        
    }
    public void SpawnCharacter()
    {
        Debug.Log("Instanciando personaje...");
        if (currentCharacter != null)
        {
            Destroy(currentCharacter); // Eliminamos al anterior
        }

        currentCharacter = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        if (currentCharacter != null)
        {
            Debug.Log("Personaje instanciado correctamente.");
        }
        else
        {
            Debug.LogError("Error al instanciar el personaje.");
        }
    }
}
