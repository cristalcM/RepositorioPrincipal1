using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject NotificationPanel;  // Panel que contiene el texto de la notificación
    public TMP_Text NotificationTexto;  // Campo de texto que mostrará las notificaciones

    private string[] NotificationLineas;  // Almacena las líneas de notificación
    private int indiceLineas;  // Lleva el seguimiento de la línea actual
    private Coroutine currentCourutine;  // Corrutina para cambiar el texto
    private bool NotificationActivo;  // Indica si las notificaciones están activas

    void Start()
    {
        NotificationPanel.SetActive(false);  // Inicia con el panel oculto
    }

    public void MostrarNotification(string[] lineas)
    {
        NotificationLineas = lineas;
        indiceLineas = 0;
        NotificationPanel.SetActive(true);  // Mostrar el panel
        NotificationActivo = true;

        // Si ya hay una corrutina en ejecución, detenerla antes de iniciar una nueva
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

            yield return new WaitForSeconds(2f);  // Esperar 2 segundos antes de cambiar al siguiente
        }

        // Ocultar el panel cuando se hayan mostrado todas las notificaciones
        NotificationActivo = false;
        NotificationPanel.SetActive(false);
    }
}


//public class Notification : MonoBehaviour
//{

//    public GameObject NotificationPanel;
//    public TMP_Text NotificationTexto;


//    private string[] NotificationLineas; //Almacena las Notificaciones.
//    private int indiceLineas; //LLeva el segimiento de las notificaiones.
//    private Coroutine currentCourutine;
//    private bool NotificationActivo;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }


//    public void MostrarNotification(string[] lineas)
//    {
//        NotificationLineas = lineas;
//        indiceLineas = 0;
//        NotificationPanel.SetActive(true);
//        NotificationActivo = true;

//        NotificationTexto.text = "aquí aparece que notifica";
//    }
//}
