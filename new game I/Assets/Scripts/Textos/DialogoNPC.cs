using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogoNPC : MonoBehaviour
{
    [SerializeField] private GameObject DialogoMark;  // Marca que aparece cuando el jugador está cerca
    [SerializeField] private GameObject DialogoPanel;  // Panel de diálogo
    [SerializeField] private TMP_Text DialogoText;  // Texto que se muestra en el panel de diálogo

    [SerializeField, TextArea(4, 6)] private string[] DialogueLinesNPC;  // Líneas del gato
    [SerializeField, TextArea(4, 6)] private string[] DialogueLinesJugador;  // Líneas del jugador

    private float typinigTime = 0.05f;  // Velocidad de escritura
    private bool IsplayerInRange;
    private bool DidDialogueStart;
    private int LineIndex;
    private bool isTalkingToNPC;  // Controla si está hablando con el gato

    // Este método es para activar el diálogo del gato
    public void IniciarDialogoConGato()
    {
        isTalkingToNPC = true;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Empieza el diálogo si el jugador está en rango y presiona E
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

    // Inicia el diálogo desde el principio
    private void StartDialogue()
    {
        DidDialogueStart = true;
        DialogoPanel.SetActive(true);
        DialogoMark.SetActive(false);
        LineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
        Debug.Log("Diálogo iniciado.");
    }

    // Muestra la siguiente línea del diálogo
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
            Debug.Log("Diálogo terminado.");
        }
    }

    // Muestra las líneas de diálogo carácter por carácter
    private IEnumerator ShowLine()
    {
        DialogoText.text = string.Empty;

        foreach (char ch in GetCurrentDialogueLine())
        {
            DialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typinigTime);
        }

        Debug.Log($"Línea mostrada: {GetCurrentDialogueLine()}");
    }

    // Retorna la línea actual de diálogo dependiendo del NPC
    private string GetCurrentDialogueLine()
    {
        return GetDialogueLines()[LineIndex];
    }

    // Retorna las líneas de diálogo correctas dependiendo del NPC (jugador o gato)
    private string[] GetDialogueLines()
    {
        if (isTalkingToNPC)
        {
            // Alternamos entre líneas del jugador y el gato
            return LineIndex % 2 == 0 ? DialogueLinesJugador : DialogueLinesNPC;
        }

        return DialogueLinesJugador;  // Default al jugador si no es el gato
    }

    // Detecta si el jugador está cerca para iniciar el diálogo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsplayerInRange = true;
            DialogoMark.SetActive(true);
            Debug.Log("Jugador está en rango para interactuar.");
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
