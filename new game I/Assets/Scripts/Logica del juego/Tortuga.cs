using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Tortuga : MonoBehaviour
{
    static string nombre;

    public Animator doroteoAnimator;   // Asigna el Animator de Doroteo
    public GameObject botas;           // Botas que recibirá el jugador
    public float distanciaInteraccion = 2f;  // Distancia mínima para interactuar

    private bool estaVolteada = true;  // Doroteo empieza volteada
    private bool jugadorEnRango = false;  // Verifica si el jugador está cerca
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

    //----------------------------------------
    // Buscar nombre y lo declara
    private void Start()
    {
        if (PlayerPrefs.HasKey("NamePLayer"))
        {
            string playerName = PlayerPrefs.GetString("NamePLayer");
            // Aquí puedes usar "playerName" en tus diálogos o donde sea necesario
            Debug.Log("Se busca nombre");
        }

        if (PlayerPrefs.HasKey("Career"))
        {
            string playerCareer = PlayerPrefs.GetString("Career");
            // Aquí puedes usar "playerCareer" en tus diálogos o donde sea necesario
            Debug.Log("Se busca carrera");
        }

        nombre = PlayerPrefs.GetString("NamePLayer");


    }
    //----------------------------------------

    void InteractuarConDoroteo()
    {
       
        if (estaVolteada)
        {
            Dialogo.MostrarDialogo(DoroteoDialogoSinAyuda);
            // Si Doroteo está volteado, inicia la animación de "desvoltear"
            VoltearDoroteo();
           
        }
        else if (yaEntregoBotas == false)
        {
            DialogodeAgradecimiento();
        }

        else
        {
            // Si ya está desvolteado, muestra el diálogo de la segunda interacción
            MostrarDialogoSegundaInteraccion();
        }
    }

    public void VoltearDoroteo()
    {
        estaVolteada = false;

        // Activa la animación de desvoltear
        doroteoAnimator.SetTrigger("isDesvolteando");

        
    }


    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
        yaEntregoBotas = true;
      
        Instantiate(botas, transform.position + new Vector3(0, -2, -5), Quaternion.identity);

        //Mostrar el diálogo de agradecimiento
            Debug.Log("Doroteo: ¡Gracias por ayudarme! Aquí tienes unas botas.");

    }


    void MostrarDialogoSegundaInteraccion()
    {
        Debug.Log("Doroteo: Grah... (Gracias nuevamente).");
        Dialogo.MostrarDialogo(DoroteoDialogofinal);
    }

    void DialogodeAgradecimiento()
    {
        // Después de que se desvoltea, entrega las botas si no lo ha hecho aún
        if (!yaEntregoBotas)
        {
            DarRecompensa();
        }
        Dialogo.MostrarDialogo(DoroteoDialogoConAyuda);
    }

    // Detecta si el jugador está en rango
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
            Debug.Log("Jugador se alejó de Doroteo.");
        }
    }


    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________
    [ TextArea(4, 6)]
    private string[] DoroteoDialogoSinAyuda =
     {
        "Doroteo: GRRUUH",
        "Jugador: déjame echarte una mano."
    };
    [ TextArea(4, 6)]
    private string[] DoroteoDialogoConAyuda =
    {
       "*le da las botas*",
       "Yo: ¡Wow! Estas botas me serán muy útiles. ¡Gracias, Doroteo!",
       "Doroteo: mmmmm (nuevamente gracias)",
       "Yo: no hay de que amiguito."

    };

    private string[] DoroteoDialogofinal =
   {
       "Doroteo: Grah... (Gracias)",
       "Yo: No hay de qué amiguito.",
   };
}
