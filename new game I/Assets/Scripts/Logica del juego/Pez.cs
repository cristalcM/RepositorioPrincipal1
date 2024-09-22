using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pez : MonoBehaviour
{
    private bool jugadorEnRango = false;  // Verificar si el jugador est� cerca

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� cerca y presiona E
        {
            RecogerPez();
        }
    }

    // Detectar si el jugador est� cerca delpez
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de el pez.");
            jugadorEnRango = true;
        }
    }

    // Detectar si el jugador se aleja del pez
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se alej� del pez.");
            jugadorEnRango = false;
        }
    }

    // M�todo para recoger el pez
    private void RecogerPez()
    {
        Gato gato = FindFirstObjectByType<Gato>();  // Encontrar al jugador
       gato.Inmune();
        Debug.Log("Has recogido la Comida.");
        Destroy(gameObject);  // Destruir el objeto f�sico del pez
    }
}
