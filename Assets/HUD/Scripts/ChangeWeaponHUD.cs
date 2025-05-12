using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponHUD : MonoBehaviour
{

    [SerializeField] private Item item;

    private void OnEnable()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.image;
        }
    }
}
