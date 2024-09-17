using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AgarrarObjetos : MonoBehaviour
{
    public Transform mano; // Donde se colocará el objeto agarrado
    public float distanciaAgarrar = 2f; // Distancia máxima para agarrar un objeto
    private GameObject objetoAgarrado; // Referencia al objeto que está siendo agarrado

    void Update()
    {
        // Si el jugador presiona la tecla E intenta agarrar o soltar el objeto
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objetoAgarrado == null)
            {
                // Intentar agarrar un objeto
                Agarrar();
            }
            else
            {
                // Soltar el objeto actual
                Soltar();
            }
        }
    }

    void Agarrar()
    {
        // Raycast para detectar objetos en la dirección hacia adelante del personaje
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaAgarrar))
        {
            if (hit.collider.CompareTag("Agarrar"))
            {
                objetoAgarrado = hit.collider.gameObject;
                objetoAgarrado.GetComponent<Rigidbody>().isKinematic = true; // Evitar que el objeto sea afectado por físicas
                objetoAgarrado.transform.position = mano.position; // Mover el objeto a la mano
                objetoAgarrado.transform.parent = mano; // Hacer que el objeto siga la mano
            }
        }
    }

    void Soltar()
    {
        if (objetoAgarrado != null)
        {
            objetoAgarrado.GetComponent<Rigidbody>().isKinematic = false; // Volver a activar las físicas
            objetoAgarrado.transform.parent = null; // Quitar el objeto de la mano
            objetoAgarrado = null;
        }
    }
}