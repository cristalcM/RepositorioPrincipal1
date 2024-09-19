using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croquetas : MonoBehaviour
{
    private bool jugadorEnRango = false;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está cerca y presiona E
        {
            RecogerCroq();
        }
    }



    // Detectar si el jugador está cerca de la croquetas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de la croquetas.");
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja de la croquetas
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se alejó de la croquetas.");
            jugadorEnRango = false;
        }
    }


    // Método para recoger las croquetas
    private void RecogerCroq()
    {
        Inventario inventario = FindFirstObjectByType<Inventario>();  // Encontrar al jugador
        Player player = FindFirstObjectByType<Player>();  // Encontrar al jugador
        inventario.RecogerCroq();  // Activar la posibilidad de recoger la taza
        player.BuscarComida();
        player.PermitirRecibirComida();
        Debug.Log("Has recogido la Moneda.");
        Destroy(gameObject);  // Destruir el objeto físico de la taza
    }
}
