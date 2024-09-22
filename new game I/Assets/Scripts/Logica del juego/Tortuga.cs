using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;

public class Tortuga : MonoBehaviour
{
    public Animator doroteoAnimator;   // Asigna el Animator de Doroteo
    public GameObject botas;           // Botas que recibir� el jugador
    public float distanciaInteraccion = 2f;  // Distancia m�nima para interactuar

    private bool estaVolteada = true;  // Doroteo empieza volteada
    private bool jugadorEnRango = false;  // Verifica si el jugador est� cerca
    private bool yaEntregoBotas = false;  // Asegura que solo entregue las botas una vez


    //Otras clases.
    public DialogoNPC Dialogo;
    public Notification notification;

    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador presiona E cerca de Doroteo
        {
            InteractuarConDoroteo();
        }
    }

    void InteractuarConDoroteo()
    {
       
        if (estaVolteada)
        {
            Dialogo.MostrarDialogo(DoroteoDialogoSinAyuda);
            // Si Doroteo est� volteado, inicia la animaci�n de "desvoltear"
            VoltearDoroteo();
           
        }
        else if (yaEntregoBotas == false)
        {
            DialogodeAgradecimiento();
        }

        else
        {
            // Si ya est� desvolteado, muestra el di�logo de la segunda interacci�n
            MostrarDialogoSegundaInteraccion();
        }
    }

    public void VoltearDoroteo()
    {
        estaVolteada = false;

        // Activa la animaci�n de desvoltear
        doroteoAnimator.SetTrigger("isDesvolteando");

        
    }


    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
        yaEntregoBotas = true;
      
        Instantiate(botas, transform.position + new Vector3(0, -2, 0), Quaternion.identity);

        //Mostrar el di�logo de agradecimiento
            Debug.Log("Doroteo: �Gracias por ayudarme! Aqu� tienes unas botas.");

    }


    void MostrarDialogoSegundaInteraccion()
    {
        Debug.Log("Doroteo: Grah... (Gracias nuevamente).");
        Dialogo.MostrarDialogo(DoroteoDialogofinal);
    }

    void DialogodeAgradecimiento()
    {
        // Despu�s de que se desvoltea, entrega las botas si no lo ha hecho a�n
        if (!yaEntregoBotas)
        {
            DarRecompensa();
        }
        Dialogo.MostrarDialogo(DoroteoDialogoConAyuda);
    }

    // Detecta si el jugador est� en rango
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("Jugador cerca de Doroteo.");
        }
    }

    // Detecta cuando el jugador se aleja
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador se alej� de Doroteo.");
        }
    }


    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________
    [SerializeField, TextArea(4, 6)]
    private string[] DoroteoDialogoSinAyuda =
     {
        "Doroteo: GRRUUH",
        "Jugador: d�jame echarte una mano."
    };
    [SerializeField, TextArea(4, 6)]
    private string[] DoroteoDialogoConAyuda =
    {
       "*le da las botas*",
       "Jugador: �Wow! Estas botas me ser�n muy �tiles. �Gracias, Doroteo!",
        "Doroteo: mmmmm (nuevamente gracias)",
        "Jugado: no hay de que amiguito."

    };

    private string[] DoroteoDialogofinal =
   {
        "Doroteo: Grah... (Gracias)",
        "Jugador: No hay de qu� amiguito.",
    };
}
