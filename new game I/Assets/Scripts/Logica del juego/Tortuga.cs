using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tortuga : MonoBehaviour
{
    public Animator doroteoAnimator;   // Asigna el Animator de Doroteo
    public GameObject botas;           // Botas que recibirá el jugador
    public float distanciaInteraccion = 2f;  // Distancia mínima para interactuar

    private bool estaVolteada = true;  // Doroteo empieza volteada
    private bool jugadorEnRango = false;  // Verifica si el jugador está cerca
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
            // Si Doroteo está volteado, inicia la animación de "desvoltear"
            VoltearDoroteo();
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

        // Después de que se desvoltea, entrega las botas si no lo ha hecho aún
        if (!yaEntregoBotas)
        {
           DarRecompensa();
        }
    }


    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
      
        Instantiate(botas, transform.position + new Vector3(0, -2, 0), Quaternion.identity);

        //Mostrar el diálogo de agradecimiento
            Debug.Log("Doroteo: ¡Gracias por ayudarme! Aquí tienes unas botas.");
    }
    

    void MostrarDialogoSegundaInteraccion()
    {
        Debug.Log("Doroteo: Grah... (Gracias nuevamente).");
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
}
