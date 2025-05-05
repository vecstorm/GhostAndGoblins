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
    public Sprite antorchaSprite;
    public Sprite hachaSprite;
    public Sprite escudoSprite;

    private string currentWeapon = "";

    enum ARMAS { LANZA, HACHA, DAGA, ANTORCHA}

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
            case "Antorcha":
                heldItemImage.sprite = antorchaSprite;
                break;
            case "Hacha":
                heldItemImage.sprite = hachaSprite;
                break;
            case "Escudo":
                heldItemImage.sprite = escudoSprite;
                break;
            default:
                heldItemImage.sprite = defaultSprite;
                break;
        }
    }
}
