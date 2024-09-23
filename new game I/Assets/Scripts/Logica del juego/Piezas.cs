using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Piezas : MonoBehaviour
{
    private bool jugadorEnRango = false;  // Verificar si el jugador está cerca
    public Notification notification;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está cerca y presiona E
        {
            RecogerPieza();
        }
    }

    // Detectar si el jugador está cerca de la moneda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja de la moneda
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            jugadorEnRango = false;
        }
    }

    // Método para recoger la taza
    private void RecogerPieza()
    {
        Inventario inventario = FindFirstObjectByType<Inventario>();  // Encontrar al jugador
        inventario.RecojerPieza();  // Activar la posibilidad de recoger la pieza
        Debug.Log("Has recogido la pieza.");
        Destroy(gameObject);  // Destruir el objeto físico de la pieza
    }
}
