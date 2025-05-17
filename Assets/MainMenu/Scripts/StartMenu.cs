using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Play()
    {
        //obte l'index de la escena actual (la del menu)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); //carrega la seguent escena
    }

    public void Quit()
    {
        //Si estem executant el joc dins de l'editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Aturem el mode de joc
#else
        Application.Quit();//si es una versio compilada del joc tanquem l'aplicacio
#endif
    }
}

//es podria fer d'una altre manera, pero a windows no esta la opcio d'utlitzar simplement unityEditor.
