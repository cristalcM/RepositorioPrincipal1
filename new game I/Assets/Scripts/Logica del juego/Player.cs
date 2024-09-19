using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //vareables para gato
    public GameObject gato;
    private bool tieneComida = false;
    public  bool tieneTaza = false;  // Indica si el jugador tiene la taza
    private bool gatoEnRango = false;
    //vareables para Aike
    public GameObject aike;
    public GameObject sombrillaPrefab;
    private bool tieneSombrilla = false;
    private bool AikeEnRango = false;

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && gatoEnRango) // Tecla para interactuar
        {
            InteractuarConGato();
            
        }
        if (Input.GetKeyDown(KeyCode.E) && AikeEnRango) // Tecla para interactuar
        {
            InteractuarConAike();

        }
    }

    //--------------------------------------
    //Metodos para gato
    //----------------------------------

    void InteractuarConGato()
    {
        Gato gatoScript = gato.GetComponent<Gato>();

        if (gatoScript.tieneHambre && tieneComida)
        {
            gatoScript.DarComida(tieneTaza);  // Pasamos si el jugador tiene la taza
            tieneComida = false;  // Resetear después de alimentar
        }
        else if (!tieneComida)
        {
            Debug.Log("Necesito encontrar comida primero.");
            BuscarComida();
        }
    }

    void BuscarComida()
    {
        Debug.Log("Has encontrado comida para el gato.");
        tieneComida = true;
    }

    // Método que solo Aike debe invocar para darle la taza al jugador
    public void RecibirTaza()
    {


        tieneTaza = true;
        Debug.Log("Ahora puedes llevar más comida de una sola vez al gato.");
    }

    // Método para verificar si el jugador tiene la taza
    public bool TieneTaza()
    {
        return tieneTaza;
    }

    

    //--------------------------------------
    //Metodos para aike
    //----------------------------------
    void InteractuarConAike()
    {
        Aike aikeScript = aike.GetComponent<Aike>();

        if (aikeScript.necesitaAyuda && tieneSombrilla)
        {
            aikeScript.DarSombrilla();
            tieneSombrilla = false;
        }
        else if (!tieneSombrilla)
        {
            Debug.Log("Necesito encontrar la sombrilla primero.");
            // Lógica para buscar y recoger la sombrilla
            BuscarSombrilla();
        }
    }

    void BuscarSombrilla()
    {
        Debug.Log("Has encontrado la sombrilla de Aike.");
        tieneSombrilla = true;
        // Puedes agregar animaciones o efectos aquí
        Instantiate(sombrillaPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("gato"))
        {
            Debug.Log("aike fuera del rango del jugador.");
            gatoEnRango = true;
        }
        if (collision.CompareTag("Aike"))
        {
            Debug.Log("Aike fuera del rango del jugador.");
            AikeEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("gato"))
        {
            Debug.Log("aike fuera del rango del jugador.");
            gatoEnRango = false;
        }
        if (collision.CompareTag("Aike"))
        {
            Debug.Log("Aike fuera del rango del jugador.");
            AikeEnRango = false;
        }
    }




}



