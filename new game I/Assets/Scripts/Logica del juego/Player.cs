using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gato;
    private bool tieneComida = false;
    public  bool tieneTaza = false;  // Indica si el jugador tiene la taza

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Tecla para interactuar
        {
            InteractuarConGato();
        }
    }

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

    public void RecibirTaza()
    {
        tieneTaza = true;
        Debug.Log("Ahora puedes llevar más comida de una sola vez al gato.");
    }

}
