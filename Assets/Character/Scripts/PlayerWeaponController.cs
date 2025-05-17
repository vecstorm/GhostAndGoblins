using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    //Instancia estatica que permet l'acces global al controlador d'arma
    public static PlayerWeaponController Instance;

    public Image heldItemImage;  // Imatge en el canva

    //Sprites de les armes
    public Sprite defaultSprite;
    public Sprite lanzaSprite;
    public Sprite dagaSprite;


    private string currentWeapon = "";

    enum ARMAS { LANZA, DAGA}

    ArrayList weapons = new ArrayList();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //Quan el jugador entra en colisio amb una arma
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {

            weapons.Add(weapons[1]);
            weapons.Remove(weapons[0]);
        }
    }

    //equipa l'arma 
    public void EquipWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        UpdateWeaponUI();
    }

    //Actualitza la imatge de l'arma per el nom
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
