using System.Collections;
using System.Collections.Generic;
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
            // Si Doroteo est� volteado, inicia la animaci�n de "desvoltear"
            VoltearDoroteo();
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

        // Despu�s de que se desvoltea, entrega las botas si no lo ha hecho a�n
        if (!yaEntregoBotas)
        {
           DarRecompensa();
        }
    }


    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
      
        Instantiate(botas, transform.position + new Vector3(0, -2, 0), Quaternion.identity);

        //Mostrar el di�logo de agradecimiento
            Debug.Log("Doroteo: �Gracias por ayudarme! Aqu� tienes unas botas.");
    }
    

    void MostrarDialogoSegundaInteraccion()
    {
        Debug.Log("Doroteo: Grah... (Gracias nuevamente).");
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
}
