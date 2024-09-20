using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogosconNPC : MonoBehaviour
{


    //________________________
    //Atributos
    //-----------------------

    [SerializeField] private GameObject DialogoMark;
    [SerializeField] private GameObject DialogoPanel;
    [SerializeField] private TMP_Text DialogoText;
    //el tiempo en que se escriben las palabras letra por letra.
    private float typinigTime = 0.05f;
    private bool DidDialogueStart;
    private int LineIndex;

    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoSincomida =
    {
        "Gato: ¡MIAUURR!",
        "Jugador: ¿Te encuentras bien, amiguito?",
        "Gato: MRAUU",
        "Jugador: Mmm… pareces tener hambre, déjame buscarte algo de comer."
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoConcomida =
    {
    // Después de dar la primera comida
        "Jugador: Aquí tienes.",
        "Gato: Mrauu",
        "Jugador: Parece que aún tienes hambre, supongo que tendré que traerte un poco más.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoFinal =
   {
     // Después de la segunda comida
        "Jugador: Ahora sí, provecho Bigotes.",
        "Gato: Miau",
        "*El gato le da una moneda en agradecimiento.*",
        "Jugador: Gracias amigo, regresaré a visitarte más tarde por si vuelves a tener hambre.",
        "Gato: ¡Miau!"
    };
    //Inicia el dialogo desde el principio
    private void StartDialogue()
    {
        DidDialogueStart = true;
        DialogoPanel.SetActive(true);
        DialogoMark.SetActive(false);
        LineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    //Mestra el siguiente dialogo
    private void NextDialogueLine()
    {
        LineIndex++;
        if (LineIndex < gatoDialogoSincomida.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            DidDialogueStart = false;
            DialogoPanel.SetActive(false);
            DialogoMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    //Muestra los caracteres uno por uno
    private IEnumerator ShowLine()
    {
        DialogoText.text = string.Empty;

        foreach (char ch in gatoDialogoSincomida[LineIndex])
        {
            DialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typinigTime);
        }
    }
}
