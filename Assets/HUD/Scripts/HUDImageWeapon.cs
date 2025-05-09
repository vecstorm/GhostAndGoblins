using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDImageWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weapon;



    private void OnEnable()
    {
        if (weapon != null)
        {
            GetComponent<SpriteRenderer>().sprite = weapon.image;
        }
    }
    public void ChangeWeapon(ItemContainer newWeaponItemContainer)
    {
        Weapon newWeapon = (Weapon)newWeaponItemContainer.GetItem();

        if (newWeapon != null)
        {
            weapon = newWeapon;
        }
        // weapon1 = newWeapon;
    }
}
