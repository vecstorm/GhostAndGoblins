using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAtravesable : MonoBehaviour
{

    //Aquest script forma part d'un collider a la part superior de les escales que es diu "TerrenoAtravesable"

    private GameObject player;
    private CapsuleCollider2D csPlayer;// Collider del jugador
    private BoxCollider2D csPlata;// Collider de la plataforma
    private Bounds csPlataBounds;// Límits del collider de la plataforma
    private Vector2 csPlayerSize; // Mida del collider del jugador
    private float topPlata, piePlayer;// Altura superior de la plataforma i "peus" del jugador




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");// Busca el jugador per tag

        if (player == null)
        {
            Debug.LogError("PlataformaAtravesable: No se encontr� un objeto con la etiqueta 'Player'.");
            return;
        }


        // Assigna els colliders del jugador i la plataforma
        csPlayer = player.GetComponent<CapsuleCollider2D>();
        csPlata = GetComponent<BoxCollider2D>();

        if (csPlayer == null || csPlata == null)
        {
            Debug.LogError("PlataformaAtravesable: No se pudo obtener los colliders.");
            return;
        }


        // Guarda límits i mida per càlculs
        csPlataBounds = csPlata.bounds;
        csPlayerSize = csPlayer.size;

        // Calcula la part superior de la plataforma
        topPlata = csPlataBounds.center.y + csPlataBounds.extents.y;
    }

    void Update()
    {
        if (player != null)
        {
             // Calcula la posició de "peus" del jugador (part inferior)
            piePlayer = player.transform.position.y - csPlayer.size.y / 2;


            if (piePlayer > topPlata)
            {
                // La plataforma és sòlida (no es pot travessar)
                csPlata.isTrigger = false;
                gameObject.tag = "TerrenoAtravesable";
                gameObject.layer = LayerMask.NameToLayer("Floor");
            }

            // Si el jugador ha baixat per sota de la plataforma
            if (!csPlata.isTrigger && (piePlayer < topPlata - 0.1f))
            {
                // La plataforma es converteix en trigger (es pot travessar)
                csPlata.isTrigger = true;
                gameObject.tag = "Untagged";
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}
