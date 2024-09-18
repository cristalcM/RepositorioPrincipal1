using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class movementplayer : MonoBehaviour
{
    private float velocidad = 1f;
    private Vector3 posicionDobjetivo;
   
    private bool hasNewClick = false; // NUevo click
    [SerializeField] float radioDeDeteccion = 1f; // Radio de detección para el CircleCast
    [SerializeField] LayerMask layerDeColision; 

    void Start()
    {
        // Inicializa la posición del objetivo como la posición actual del personaje
        posicionDobjetivo = this.transform.position;
    }

    void Update()
    {
        // Detectar nuevo clic
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posición del clic
            posicionDobjetivo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionDobjetivo.z = this.transform.position.z;

            // Registrar que hubo un nuevo clic, permitiendo el movimiento
            hasNewClick = true;
        }

        MoverHaciaObjetivo();
    }

    private void MoverHaciaObjetivo()
    {
        // Comprobar si el personaje está colisionando
        bool isColliding = Physics2D.OverlapCircle(transform.position, radioDeDeteccion, layerDeColision);
        

        // Mover solo si hubo un nuevo clic y no hay colisión
        if (hasNewClick)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionDobjetivo, velocidad * Time.deltaTime);

            // Si llega a la posición objetivo, dejar de mover
            if (Vector3.Distance(transform.position, posicionDobjetivo) == 0f)
            {
                hasNewClick = false; 
            }
        }

        // Si está colisionando, detener el movimiento hasta un nuevo clic
        if (isColliding)
        {
            hasNewClick = false; // Al colisionar, esperar un nuevo clic
            return;
        }

    }
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDeteccion);
        }
}
