using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{

    public GameObject NotificationPanel;
    public TMP_Text NotificationTexto;


    private string[] NotificationLineas; //Almacena las Notificaciones.
    private int indiceLineas; //LLeva el segimiento de las notificaiones.
    private Coroutine currentCourutine;
    private bool NotificationActivo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     
    public void MostrarNotification(string[] lineas)
    {
        NotificationLineas = lineas;
        indiceLineas = 0;
        NotificationPanel.SetActive(true);
        NotificationActivo = true;

        NotificationTexto.text = "aquí aparece que notifica";
    }
}
