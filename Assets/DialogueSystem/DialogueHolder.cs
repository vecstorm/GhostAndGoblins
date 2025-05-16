using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        /*[SerializeField] GameObject hud;
        [SerializeField] GameObject enemigos;
        [SerializeField] GameObject zombis;
        [SerializeField] GameObject musica;*/
        [SerializeField] GameObject cmm;

        private void Awake()
        {
            /*hud.SetActive(false);
            enemigos.SetActive(false);
            zombis.SetActive(false);
            musica.SetActive(false);*/
            cmm.SetActive(true);

            StartCoroutine(DialogueSequence());
        }

        private IEnumerator DialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true); // Agafa l'objecte fill i ho activa
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }

            gameObject.SetActive(false); // desactiva el sistema de diàlegs
            /*hud.SetActive(true); // activa al hud
            enemigos.SetActive(true); // activa al player
            zombis.SetActive(true); // activa els spawners dels zombies
            musica.SetActive(true); // activa la musica*/
            cmm.SetActive(false); // desactiva la musica del diàlegs

        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

// int currentSceneIndex = sceneManager.GetActiveScene().BuildIndex;
// sceneManager.LoadScene(currentSceneIndex + 1);