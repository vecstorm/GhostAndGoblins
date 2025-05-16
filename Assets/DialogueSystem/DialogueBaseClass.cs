using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

            for (int i = 0; i < input.Length; i++) // Fem un bucle bàsic
            {
                textHolder.text += input[i]; // Posa el text que posis al text holder
                yield return new WaitForSeconds(0.1f); // Temps d'espera entre lletres
            }

            // yield return new WaitForSeconds(delayBetweenLines); // Per si ho vols fer amb temps en comptes d'haber d'apretar click esquerre
            yield return new WaitUntil(() => Input.GetButtonDown("Fire1")); // skip cuan click esquerre

            finished = true;

        }
    }
}
