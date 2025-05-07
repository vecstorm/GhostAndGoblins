using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Instance;

    public Image heldItemImage;  // Imagen en el Canvas
    public Sprite defaultSprite;
    public Sprite lanzaSprite;
    public Sprite dagaSprite;


    private string currentWeapon = "";

    enum ARMAS { LANZA, DAGA}

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void EquipWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        switch (currentWeapon)
        {
            case "Lanza":
                heldItemImage.sprite = lanzaSprite;
                break;
            case "Daga":
                heldItemImage.sprite = dagaSprite;
                break;
            default:
                heldItemImage.sprite = defaultSprite;
                break;
        }
    }
}
