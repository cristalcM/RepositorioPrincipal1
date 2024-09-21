using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeScroll : MonoBehaviour
{

    protected float scrollSpeed = 7f; // Velocidad de movimiento de la cámara
    protected float edgeSize = 10f;    // Tamaño del área en las esquinas de la pantalla donde se activa el movimiento

    private Camera cam;

    // Límites de la cámara en el mundo (ajusta según tu escenario)
    public Vector2 minLimit;  // Límite inferior de la cámara (x, y)
    public Vector2 maxLimit;  // Límite superior de la cámara (x, y)


    //---------------------------------------
    // Obtener la cámara principal
    //-----------------------------------
    void Start()
    {
        cam = Camera.main; 
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Obtener la posición del ratón en la pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Mover la cámara hacia arriba si el ratón está en el borde superior
        if (mousePosition.y >= Screen.height - edgeSize)
        {
            movement.y += scrollSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia abajo si el ratón está en el borde inferior
        if (mousePosition.y <= edgeSize)
        {
            movement.y -= scrollSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia la derecha si el ratón está en el borde derecho
        if (mousePosition.x >= Screen.width - edgeSize)
        {
            movement.x += scrollSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia la izquierda si el ratón está en el borde izquierdo
        if (mousePosition.x <= edgeSize)
        {
            movement.x -= scrollSpeed * Time.deltaTime;
        }


        // Obtener la nueva posición de la cámara si aplicamos el movimiento
        Vector3 newPosition = cam.transform.position + movement;

        // Limitar el movimiento dentro de los límites establecidos
        newPosition.x = Mathf.Clamp(newPosition.x, minLimit.x, maxLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minLimit.y, maxLimit.y);

        // Aplicar la nueva posición a la cámara
        cam.transform.position = newPosition;
    }

}
