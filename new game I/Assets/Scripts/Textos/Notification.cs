using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject NotificationPanel;  // Panel que contiene el texto de la notificaci�n
    public TMP_Text NotificationTexto;  // Campo de texto que mostrar� las notificaciones

    private string[] NotificationLineas;  // Almacena las l�neas de notificaci�n
    private int indiceLineas;  // Lleva el seguimiento de la l�nea actual
    private Coroutine currentCourutine;  // Corrutina para cambiar el texto
   

    void Start()
    {
        NotificationPanel.SetActive(false);  // Inicia con el panel oculto
    }

    public void MostrarNotification(string[] lineas)
    {
        NotificationLineas = lineas;
        indiceLineas = 0;
        NotificationPanel.SetActive(true);  // Mostrar el panel
        

        // Si ya hay una corrutina en ejecuci�n, detenerla antes de iniciar una nueva
        if (currentCourutine != null)
        {
            StopCoroutine(currentCourutine);
        }

        // Iniciar la corrutina para cambiar las notificaciones
        currentCourutine = StartCoroutine(CambiarTexto());
    }

    // Corrutina que cambia el texto cada cierto tiempo
    private IEnumerator CambiarTexto()
    {
        while (indiceLineas < NotificationLineas.Length)
        {
            NotificationTexto.text = NotificationLineas[indiceLineas];  // Actualizar el texto
            indiceLineas++;
        }

        yield return new WaitForSeconds(20f);  // Esperar 2 segundos antes de cambiar al siguiente
        NotificationPanel.SetActive(false);
    }
}


