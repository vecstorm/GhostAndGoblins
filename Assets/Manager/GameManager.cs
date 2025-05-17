using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static InputActionAsset inputActionsMapping;

    [SerializeField]
    InputActionAsset IA;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //que no es destrueixi en canvi d'escena
        }
        else
        {
            Destroy(gameObject);
        }

        inputActionsMapping = IA;
        inputActionsMapping.Enable();//posa enable el inputAction
    }
    

}
