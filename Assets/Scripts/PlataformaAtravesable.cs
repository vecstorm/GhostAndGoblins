using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAtravesable : MonoBehaviour
{

    private GameObject player;
    private CapsuleCollider2D csPlayer;
    private BoxCollider2D csPlata;
    private Bounds csPlataBounds;
    private Vector2 csPlayerSize;
    private float topPlata, piePlayer;



    // Start is called before the first frame update
    void Start()
    {
    player = GameObject.FindGameObjectWithTag("Player");
    csPlayer = player.GetComponent<CapsuleCollider2D>();
    csPlata = GetComponent<BoxCollider2D>();
    csPlataBounds = csPlata.bounds;
    csPlayerSize = csPlayer.size;
    topPlata = csPlataBounds.center.y + csPlataBounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        piePlayer = player.transform.position.y - csPlayer.size.y / 2;
        if(piePlayer > topPlata){
            csPlata.isTrigger = false;
            gameObject.tag = "TerrenoAtravesable";
            gameObject.layer = LayerMask.NameToLayer("Floor");
        }

        if(!csPlata.isTrigger && (piePlayer < topPlata - 0.1f)){
            csPlata.isTrigger = true;
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
