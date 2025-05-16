using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header("- Opcións del Text -")]
        [SerializeField] private string input; // El text que vols
        [SerializeField] private Color textColor; // El text que vols
        [SerializeField] private Font textFont; // El text que vols

        [Header("- Opcións del temps -")]
        [SerializeField] private float delayBetweenLines;

        [Header("- Imatge Personatge -")]
        [SerializeField] private Sprite characterSprite; // Referencia al Sprite
        [SerializeField] private Image imageHolder; // Referencia a l'imatge

        private Text textHolder; // Suport per aguantar el text

        void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delayBetweenLines)); // Comença la corrutina

        }
    }
}
