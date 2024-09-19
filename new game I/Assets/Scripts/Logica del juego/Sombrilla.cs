using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrilla : MonoBehaviour
{
    private bool jugadorEnRango = false;  // Verificar si el jugador est� cerca

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� cerca y presiona E
        {
            RecogerSombra();
        }
    }

    // Detectar si el jugador est� cerca de la moneda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de la sombrilla.");
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja de la somnrilla
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se alej� de la sombrilla.");
            jugadorEnRango = false;
        }
    }

    // M�todo para recoger la sombrilla
    private void RecogerSombra()
    {
        Player player = FindFirstObjectByType<Player>();  // Encontrar al jugador
        player.BuscarSombrilla();  // Activar la posibilidad de recoger la sombra
        Debug.Log("Has Encontrado.");
        Destroy(gameObject);  // Destruir el objeto f�sico de la taza
    }
}
