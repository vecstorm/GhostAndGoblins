using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; private set; }

        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delayBetweenLines)
        {
            textHolder.color = textColor; // Set de la font
            textHolder.font = textFont; // Set del color
            textHolder.text = ""; // Per esborra el text previ en cas que hi hagi

            finished = false;
            bool skipLine = false;


            int i = 0;

            while (i < input.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    skipLine = true;
                    break; // Sortim del bucle per mostrar tot el text
                }

                textHolder.text += input[i];
                i++;
                yield return new WaitForSeconds(0.05f);
            }

            // Si es va prémer espai durant l'escriptura, mostrar el text complet
            if (skipLine)
            {
                textHolder.text = input;
            }

            //ESPERA a que deixin anar el botó, i un petit delay perquè no es salti de cop
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
            yield return new WaitForSeconds(0.1f);


            // Ara esperem una nova pulsació d'espai per passar al següent diàleg
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            finished = true;

        }

    }
}
