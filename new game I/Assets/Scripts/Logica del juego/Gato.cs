using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gato : MonoBehaviour
{
    public bool tieneHambre = true;
    public GameObject monedaPrefab;
    public float tiempoParaHambre = 60f;
    private int comidaRecibida = 0;
    private bool jugadorEnRango = false;  // Para detectar si el jugador está cerca
    private Player Player;

    private void Update()
    {
        
            if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona E
            {
                Player = FindFirstObjectByType<Player>();
                if (Player != null && Player.TieneComida())  // Solo dar comida si el jugador tiene comida
                {
                    DarComida(Player.TieneTaza());  // Llama a DarComida si el jugador está cerca y tiene comida
                    Player.RecibirTaza();  // Después de dar comida, el jugador pierde su comida
                }
                else
                {
                    Debug.Log("No tienes comida para el gato.");
                }
            }
    }

    // Detecta cuando el jugador entra en la zona de interacción
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Asegúrate de que el jugador tenga la etiqueta "Jugador"
        {
            Debug.Log("Jugador cerca del gato.");
            jugadorEnRango = true;
        }
    }

    // Detecta cuando el jugador sale de la zona de interacción
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador fuera del rango del gato.");
            jugadorEnRango = false;
        }
    }


    //___________________________
    //Metodo para darle comida al gato.
    //_________________________________
    public void DarComida(bool jugadorTieneTaza)
    {
        if (tieneHambre)
        {
            if (jugadorTieneTaza)
            {
                Debug.Log("El gato está siendo alimentado en un solo viaje.");
                LlenarHambre();
            }
            else
            {
                comidaRecibida++;
                Debug.Log("El gato ha recibido algo de comida.");

                if (comidaRecibida >= 3)
                {
                    LlenarHambre();
                }
                else
                {
                    Debug.Log("El gato sigue con hambre. Necesitas traer más comida.");
                }
            }
        }
        else
        {
            Debug.Log("El gato ya está satisfecho.");
        }
    }


    private void LlenarHambre()
    {
        Debug.Log("El gato está satisfecho.");
        tieneHambre = false;
        comidaRecibida = 0;
        DarRecompensa();
        StartCoroutine(TemporizadorHambre());
    }

    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
        Instantiate(monedaPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
    }

    private IEnumerator TemporizadorHambre()
    {
        yield return new WaitForSeconds(tiempoParaHambre);
        tieneHambre = true;
        Debug.Log("El gato tiene hambre nuevamente.");
    }
}
