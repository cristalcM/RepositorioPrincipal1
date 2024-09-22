using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botas : MonoBehaviour
{
   
        private bool jugadorEnRango = false;  // Verificar si el jugador est� cerca
       

        private void Update()
        {
            if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� cerca y presiona E
            {
                RecogerTaza();
            }
        }

        // Detectar si el jugador est� cerca Las botas.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Jugador cerca de las botas.");
                jugadorEnRango = true;
            }
        }

        // Detectar si el jugador se aleja de la moneda
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Jugador se alej� de las botass.");
                jugadorEnRango = false;
            }
        }

        // M�todo para recoger la taza
        private void RecogerTaza()
        {

            Debug.Log("Has recogido Unos tenis.");

        MainPlayer player = FindFirstObjectByType<MainPlayer>();  // Encontrar al jugador
        player.PowerOp();  // Activar la posibilidad de recoger la taza
        Destroy(gameObject);  // Destruir el objeto f�sico de la taza
        }
    }
