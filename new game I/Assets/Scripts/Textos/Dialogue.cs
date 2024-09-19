using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue: MonoBehaviour
{
    [SerializeField] private GameObject DialogoMark;
    [SerializeField] private GameObject DialogoPanel;
    [SerializeField] private TMP_Text DialogoText;

    [SerializeField, TextArea(4, 6)] private string[] DialogueLines;

    private float typinigTime = 0.05f;

    private bool IsplayerInRange;
    private bool DidDialogueStart;
    private int LineIndex;

    // Update is called once per frame
    void Update()
    {//Empieza el dialogto
        //if (IsplayerInRange && Input.GetKeyDown(KeyCode.E))
        //{   //Solo si no esta inicializado
        //    if (!DidDialogueStart)
        //    {
        //        StartDialogue();
        //    }
        //    //Seigue al otro dialogo
        //    else if (DialogoText.text == DialogueLines[LineIndex])
        //    {
        //        NextDialogueLine();
        //    }
        //    //finaliza raipo el dialogo
        //    else
        //    {
        //        StopAllCoroutines();
        //        DialogoText.text = DialogueLines[LineIndex];
        //    }
        //}
    }



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
        if (LineIndex < DialogueLines.Length)
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

        foreach (char ch in DialogueLines[LineIndex])
        {
            DialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typinigTime);
        }
    }

    //Detecta si esta cerca 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsplayerInRange = true;
            DialogoMark.SetActive(true);
        }

    }

    //Detecta si esta lejos
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsplayerInRange = false;
            DialogoMark.SetActive(false);
        }
    }

    //MOSTRAR Y CERRAR EL DIALOGO CON NPC
    public void MostrarTexto(string texto)
    {
        DialogoPanel.SetActive(true);
        DialogoText.text = texto;
    }

    public void CerrarDialogo()
    {
        DialogoPanel.SetActive(false);
    }
}
