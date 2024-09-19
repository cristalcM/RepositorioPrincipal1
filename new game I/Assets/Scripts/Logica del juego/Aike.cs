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
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E)) // Si el jugador está en rango y presiona E
        {
            Player player = FindFirstObjectByType<Player>();
            DarSombrilla(player.TieneSombra());  // Llama a DarComida si el jugador está cerca
            //player.PermitirRecibirSombra();
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

    public void DarSombrilla(bool JugadorTieneSombra)
    {

        Debug.Log("Texto de conversaion");
        if (necesitaAyuda)
        {
            if ( JugadorTieneSombra)
            {
                Debug.Log("Gracias por traerme la sombrilla.");
                necesitaAyuda = false;
                CrearTaza();  // Crear taza en el mundo();
            }
            else
            {
                Debug.Log("Necesito una sombrilla.");
            }
          
        }
        else
        {
            Debug.Log("Ya no necesito ayuda.");
        }
    }

   
        private void CrearTaza()
        {
            Debug.Log("Aike ha dejado una taza.");
            Instantiate(tazaPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);  // Aparece la taza
        }
    
}
