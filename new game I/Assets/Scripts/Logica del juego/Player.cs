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
    private bool puedeRecibirTaza = false;
    private bool puedeRecibirComida = false;
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
            
        }
    }
    //------------------------------
    //Metodos para buscar comida
    //------------------------------


    public void BuscarComida()
    {
        if (puedeRecibirComida)
        {
            tieneComida = true;
            Debug.Log("Dale la comida a bigotes");
            puedeRecibirComida = false;  // Despu?s de recibirla, no puede recibirla de nuevo
        }
        else
        {
            Debug.Log("Necesitas ir por comida,");
        }
    }
    // M?todo para verificar si el jugador tiene 
    public bool TieneComida()
    {
        return tieneComida;
    }
    // M?todo que activa la posibilidad de recibir 
    public void PermitirRecibirComida()
    {
        Debug.Log("Has encontrado comida para el gato.");
        tieneComida = true;
        puedeRecibirComida = true;
        Debug.Log("Ahora puedes recoger comida.");
    }


    //--------------------------------------
    //Metodos para aike
    //----------------------------------
    void InteractuarConAike()
    {
        Aike aikeScript = aike.GetComponent<Aike>();

        if (aikeScript.necesitaAyuda && tieneSombrilla)
        {
            aikeScript.DarSombrilla(tieneSombrilla);
            tieneSombrilla = false;
        }
        else if (!tieneSombrilla)
        {
            Debug.Log("Necesito encontrar la sombrilla primero.");


        }
    }
    //------------------------------
    //Metodos para buscar Sombrilla
    //------------------------------
    public void BuscarSombrilla()
    {

        tieneSombrilla = true;
        Debug.Log("Dale la sombrilla a Aike");

    }
    // Método para verificar si el jugador tiene 
    public bool TieneSombra()
    {
        return tieneSombrilla;
    }



   
    //---------------------------------------------
    //METODOS PARA RECIBIR Y RECOLECTAR LA TAZA
    //---------------------------------------------
    // Método que solo Aike debe invocar para darle la taza al jugador
    public void RecibirTaza()
    {
       
        if (puedeRecibirTaza)  // Solo puede recibir la taza si está permitido
        {
            tieneTaza = true;  // El jugador obtiene la taza
            Debug.Log("Has recibido una taza. Ahora puedes llevar más comida al gato de una sola vez.");
            puedeRecibirTaza = false;  // Después de recibirla, no puede recibirla de nuevo
        }
        else
        {
            Debug.Log("No puedes recibir la taza sin antes interactuar correctamente.");
        }
    }

    // Método para verificar si el jugador tiene la taza
    public bool TieneTaza()
    {
        return tieneTaza;
    }
    // Método que activa la posibilidad de recibir la taza (llamado solo por la taza física)
    public void PermitirRecibirTaza()
    {
        puedeRecibirTaza = true;
        Debug.Log("Ahora puedes recoger la taza.");
    }

    //________________
    // Coliciones
    //________________

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



