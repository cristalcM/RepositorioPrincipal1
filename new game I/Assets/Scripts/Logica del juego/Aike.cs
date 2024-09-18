using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aike : MonoBehaviour
{
    public bool necesitaAyuda = true;
    public GameObject sombrilla;
    public GameObject tazaPrefab;
    private bool jugadorEnRango = false;  // Para detectar si el jugador está cerca

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona E
        {
            DarSombrilla();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de Aike.");
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador fuera del rango de Aike.");
            jugadorEnRango = false;
        }
    }

    public void DarSombrilla()
    {
        if (necesitaAyuda)
        {
            Debug.Log("Gracias por traerme la sombrilla.");
            necesitaAyuda = false;
            DarRecompensa();
        }
        else
        {
            Debug.Log("Ya no necesito ayuda.");
        }
    }

    private void DarRecompensa()
    {
        Player jugador = FindFirstObjectByType<Player>();
        jugador.RecibirTaza();
        Debug.Log("Aike te ha dado una taza.");
        Instantiate(tazaPrefab, transform.position, Quaternion.identity);
    }
}
