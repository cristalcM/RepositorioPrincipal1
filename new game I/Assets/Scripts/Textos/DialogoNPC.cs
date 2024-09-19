using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogoNPC : MonoBehaviour
{
    [SerializeField] private GameObject DialogoMark;  // Marca que aparece cuando el jugador est� cerca
    [SerializeField] private GameObject DialogoPanel;  // Panel de di�logo
    [SerializeField] private TMP_Text DialogoText;  // Texto que se muestra en el panel de di�logo

    [SerializeField, TextArea(4, 6)] private string[] DialogueLinesNPC;  // L�neas del gato
    [SerializeField, TextArea(4, 6)] private string[] DialogueLinesJugador;  // L�neas del jugador

    private float typinigTime = 0.05f;  // Velocidad de escritura
    private bool IsplayerInRange;
    private bool DidDialogueStart;
    private int LineIndex;
    private bool isTalkingToNPC;  // Controla si est� hablando con el gato

    // Este m�todo es para activar el di�logo del gato
    public void IniciarDialogoConGato()
    {
        isTalkingToNPC = true;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Empieza el di�logo si el jugador est� en rango y presiona E
        if (IsplayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DidDialogueStart)
            {
                StartDialogue();
            }
            else if (DialogoText.text == GetCurrentDialogueLine())
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogoText.text = GetCurrentDialogueLine();
            }
        }
    }

    // Inicia el di�logo desde el principio
    private void StartDialogue()
    {
        DidDialogueStart = true;
        DialogoPanel.SetActive(true);
        DialogoMark.SetActive(false);
        LineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
        Debug.Log("Di�logo iniciado.");
    }

    // Muestra la siguiente l�nea del di�logo
    private void NextDialogueLine()
    {
        LineIndex++;
        if (LineIndex < GetDialogueLines().Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            DidDialogueStart = false;
            DialogoPanel.SetActive(false);
            DialogoMark.SetActive(true);
            Time.timeScale = 1f;
            Debug.Log("Di�logo terminado.");
        }
    }

    // Muestra las l�neas de di�logo car�cter por car�cter
    private IEnumerator ShowLine()
    {
        DialogoText.text = string.Empty;

        foreach (char ch in GetCurrentDialogueLine())
        {
            DialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typinigTime);
        }

        Debug.Log($"L�nea mostrada: {GetCurrentDialogueLine()}");
    }

    // Retorna la l�nea actual de di�logo dependiendo del NPC
    private string GetCurrentDialogueLine()
    {
        return GetDialogueLines()[LineIndex];
    }

    // Retorna las l�neas de di�logo correctas dependiendo del NPC (jugador o gato)
    private string[] GetDialogueLines()
    {
        if (isTalkingToNPC)
        {
            // Alternamos entre l�neas del jugador y el gato
            return LineIndex % 2 == 0 ? DialogueLinesJugador : DialogueLinesNPC;
        }

        return DialogueLinesJugador;  // Default al jugador si no es el gato
    }

    // Detecta si el jugador est� cerca para iniciar el di�logo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsplayerInRange = true;
            DialogoMark.SetActive(true);
            Debug.Log("Jugador est� en rango para interactuar.");
        }
    }

    // Detecta si el jugador se aleja
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsplayerInRange = false;
            DialogoMark.SetActive(false);
            Debug.Log("Jugador se ha alejado del rango.");
        }
    }
}
