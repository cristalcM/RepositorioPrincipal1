using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    private bool jugadorEnRango = false;  // Verificar si el jugador est� cerca

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� cerca y presiona E
        {
            RecogerTaza();
        }
    }

    // Detectar si el jugador est� cerca de la moneda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de la Moneda.");
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja de la moneda
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se alej� de la Moneda.");
            jugadorEnRango = false;
        }
    }

    // M�todo para recoger la taza
    private void RecogerTaza()
    {
        Inventario inventario = FindFirstObjectByType<Inventario>();  // Encontrar al jugador
        inventario.A�adirMoneda();  // Activar la posibilidad de recoger la taza
        Debug.Log("Has recogido la Moneda.");
        Destroy(gameObject);  // Destruir el objeto f�sico de la taza
    }
}
