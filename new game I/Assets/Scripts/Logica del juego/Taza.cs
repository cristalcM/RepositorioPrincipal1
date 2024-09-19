using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Taza : MonoBehaviour
{
    private bool jugadorEnRango = false;  // Verificar si el jugador está cerca

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está cerca y presiona E
        {
            RecogerTaza();
        }
    }

    // Detectar si el jugador está cerca de la taza
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de la taza.");
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja de la taza
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se alejó de la taza.");
            jugadorEnRango = false;
        }
    }

    // Método para recoger la taza
    private void RecogerTaza()
    {
        Player jugador = FindFirstObjectByType<Player>();  // Encontrar al jugador
        jugador.PermitirRecibirTaza();  // Activar la posibilidad de recoger la taza
        jugador.RecibirTaza();  // El jugador recibe la taza después de interactuar
        Debug.Log("Has recogido la taza.");
        Destroy(gameObject);  // Destruir el objeto físico de la taza
    }
}
