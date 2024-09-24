using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{

    private bool jugadorEnRango = false;  // Verificar si el jugador está cerca

    public GameObject panelCreditos;

    private void Update()
    {
        if (jugadorEnRango  && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona E
        {
            mostarPanel();
        }
       if (panelCreditos && Input.GetKeyDown(KeyCode.C))
        {
           ocultarpanel();
        }
        
  
}

    void mostarPanel()
    {
        panelCreditos.gameObject.SetActive(true);
        
    }

    void ocultarpanel()
    {
        panelCreditos.gameObject.SetActive(false);
        
    }



// Detectar si el jugador está cerca de la moneda
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
            Debug.Log("Jugador se alejó de la Moneda.");
            jugadorEnRango = false;
        }
    }



}
