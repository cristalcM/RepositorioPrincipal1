using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogoNPC : MonoBehaviour
{
    public GameObject DialogoPanel;
    public TMP_Text DialogoTexto;
    public float typingSpeed = 0.05f;
   

    private string[] DialogoLineas; //Almacena las lineas del dialogo.
    private int indiceLineas; //LLeva el segimiento de la linea.
    private Coroutine currentCourutine;
    private bool dialogoActivo;
    private bool DidDialogueStart;


    //---------------------------------------
    //Metodo para iniciat un dialogo
    //---------------------------------------
    public void MostrarDialogo(string[] lineas)
    {
        DialogoLineas = lineas;
        indiceLineas = 0;
        DialogoPanel.SetActive(true);
        dialogoActivo = true;
        Time.timeScale = 0f;



        if (currentCourutine != null)
        {
          
            StopCoroutine(currentCourutine);
        }

        currentCourutine = StartCoroutine(MostrarTextoLetraPorLetra(DialogoLineas[indiceLineas]));
    }

   
    public void SiguienteLinea()
    {
        if (DialogoTexto.text == DialogoLineas[indiceLineas])
        {
            indiceLineas++;
            Debug.Log("Avanzando a la línea " + indiceLineas);
            if (indiceLineas < DialogoLineas.Length)
            {
                //muestra la sigiente linea
                if (currentCourutine != null)
                {
                    StopCoroutine (currentCourutine);
                }
                currentCourutine = StartCoroutine(MostrarTextoLetraPorLetra(DialogoLineas[indiceLineas]));
               
            }
            else
            {
                OcultarDialogo();
            }
        }
        else
        {
            StopAllCoroutines();
            DialogoTexto.text = DialogoLineas[indiceLineas];
        }
    }

    public void OcultarDialogo()
    {
        DialogoPanel.SetActive(false);
        dialogoActivo = false;
        Time.timeScale = 1f;
        DialogoTexto.text = "";
    }

    // Coroutine para mostrar el texto letra por letra
    private IEnumerator MostrarTextoLetraPorLetra(string mensaje)
    {
        DialogoTexto.text = "";  // Limpiar el texto antes de empezar

        foreach (char letra in mensaje.ToCharArray())
        {
            DialogoTexto.text += letra;  // Agregar cada letra al texto
            yield return new WaitForSecondsRealtime(typingSpeed);  // Esperar un tiempo antes de la siguiente letra
        }
    }

    private void Update()
    {
       
       // Si el diálogo está activo y el jugador presiona la tecla E, avanzar al siguiente texto
        if (dialogoActivo && Input.GetKeyDown(KeyCode.W))
        {
            SiguienteLinea();
        }
    }
}






