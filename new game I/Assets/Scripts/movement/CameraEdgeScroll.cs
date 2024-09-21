using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeScroll : MonoBehaviour
{

    protected float scrollSpeed = 7f; // Velocidad de movimiento de la c�mara
    protected float edgeSize = 10f;    // Tama�o del �rea en las esquinas de la pantalla donde se activa el movimiento

    private Camera cam;

    // L�mites de la c�mara en el mundo (ajusta seg�n tu escenario)
    public Vector2 minLimit;  // L�mite inferior de la c�mara (x, y)
    public Vector2 maxLimit;  // L�mite superior de la c�mara (x, y)


    //---------------------------------------
    // Obtener la c�mara principal
    //-----------------------------------
    void Start()
    {
        cam = Camera.main; 
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Obtener la posici�n del rat�n en la pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Mover la c�mara hacia arriba si el rat�n est� en el borde superior
        if (mousePosition.y >= Screen.height - edgeSize)
        {
            movement.y += scrollSpeed * Time.deltaTime;
        }

        // Mover la c�mara hacia abajo si el rat�n est� en el borde inferior
        if (mousePosition.y <= edgeSize)
        {
            movement.y -= scrollSpeed * Time.deltaTime;
        }

        // Mover la c�mara hacia la derecha si el rat�n est� en el borde derecho
        if (mousePosition.x >= Screen.width - edgeSize)
        {
            movement.x += scrollSpeed * Time.deltaTime;
        }

        // Mover la c�mara hacia la izquierda si el rat�n est� en el borde izquierdo
        if (mousePosition.x <= edgeSize)
        {
            movement.x -= scrollSpeed * Time.deltaTime;
        }


        // Obtener la nueva posici�n de la c�mara si aplicamos el movimiento
        Vector3 newPosition = cam.transform.position + movement;

        // Limitar el movimiento dentro de los l�mites establecidos
        newPosition.x = Mathf.Clamp(newPosition.x, minLimit.x, maxLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minLimit.y, maxLimit.y);

        // Aplicar la nueva posici�n a la c�mara
        cam.transform.position = newPosition;
    }

}
