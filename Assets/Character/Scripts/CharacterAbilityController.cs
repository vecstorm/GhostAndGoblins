
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityController : MonoBehaviour
{

    [SerializeField] Weapon weapon;

    [SerializeField]Transform shootSpawnPoint;
    InputAction shoot;
    [SerializeField]float coolDownShoot;
    int cantidadDisparos=0;

    public InputActionAsset inputActionMapping;
    private Animator animator;
    audioManagerScript audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
    }

    void Start()
    {
        MovimientoJugador c = GetComponent<MovimientoJugador>(); // Obte el component que controla el moviment del jugador
        shoot = c.inputActionsMapping.FindActionMap("Attack").FindAction("Shoot"); // El input action de atacar
        animator = GetComponent<Animator>(); //obte el component del Animator

    }

    void Update()
    {
        //si el boto de disparar ha sigut presionat
        if (shoot.triggered)
        {
            Disparar(); //executa el dispar
            animator.SetBool("IsShooting", true); //Activa la animacio de disparar
        }
        else
        {
            animator.SetBool("IsShooting", false);//Desactiva la animacio de disparar
        }
    }

    void Disparar()
    {
        //reprodueix el so de disparar
        audioManager.PlaySFX(audioManager.disparoPlayer);

        //Instanciem el projectil desde el punt de dispar
        Instantiate(weapon.GetProjectilePrefab(), shootSpawnPoint.position, shootSpawnPoint.rotation);
        cantidadDisparos++; //incrementa la quantitat de dispars
    }

    //metode que obte la quantitat de dispars realitzats
    public int GetCantidadDisparos()
    {
        return cantidadDisparos;
    }

    //metode per cambiar l'arma actual a la nova
    public void ChangeWeapon(ItemContainer newWeaponItemContainer)
    {
        Weapon newWeapon = (Weapon)newWeaponItemContainer.getItem(); // obte l'arma del contenidos de item

        //si s'obte correctament, la reemplaça
        if (newWeapon != null)
        {

            weapon = newWeapon;

        }
    }



}
