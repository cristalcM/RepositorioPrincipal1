using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Gato : MonoBehaviour
{
    public bool tieneHambre = true;
    public GameObject monedaPrefab;
    public float tiempoParaHambre = 60f;
    private int comidaRecibida = 0;
    private bool jugadorEnRango = false;  // Para detectar si el jugador est� cerca
    private Player Player;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� en rango y presiona E
        {
            Player  = FindFirstObjectByType<Player>();
            /* DarComida(Player.tieneTaza());*/  // Llama a DarComida si el jugador est� cerca
            Player.RecibirTaza();
        }
    }

    // Detecta cuando el jugador entra en la zona de interacci�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))  // Aseg�rate de que el jugador tenga la etiqueta "Jugador"
        {
            Debug.Log("Jugador cerca del gato.");
            jugadorEnRango = true;
        }
    }

    // Detecta cuando el jugador sale de la zona de interacci�n
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Debug.Log("Jugador fuera del rango del gato.");
            jugadorEnRango = false;
        }
    }

    public void DarComida(bool jugadorTieneTaza)
    {
        if (tieneHambre)
        {
            if (jugadorTieneTaza)
            {
                Debug.Log("El gato est� siendo alimentado en un solo viaje.");
                LlenarHambre();
            }
            else
            {
                comidaRecibida++;
                Debug.Log("El gato ha recibido algo de comida.");

                if (comidaRecibida >= 2)
                {
                    LlenarHambre();
                }
                else
                {
                    Debug.Log("El gato sigue con hambre. Necesitas traer m�s comida.");
                }
            }
        }
        else
        {
            Debug.Log("El gato ya est� satisfecho.");
        }
    }

    private void LlenarHambre()
    {
        Debug.Log("El gato est� satisfecho.");
        tieneHambre = false;
        comidaRecibida = 0;
        DarRecompensa();
        StartCoroutine(TemporizadorHambre());
    }

    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
        Instantiate(monedaPrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator TemporizadorHambre()
    {
        yield return new WaitForSeconds(tiempoParaHambre);
        tieneHambre = true;
        Debug.Log("El gato tiene hambre nuevamente.");
    }
}
